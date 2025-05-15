#tool "nuget:?package=ILMerge&version=2.12.803"
#addin "nuget:?package=Cake.Git&version=1.0.0"
#tool "nuget:?package=ILRepack.MSBuild.Task&version=2.0.13"
#tool "nuget:?package=protobuf-net&version=1.0.0.280"
#tool "nuget:?package=secure-file&version=1.0.31"
#tool "nuget:?package=Brutal.Dev.StrongNameSigner&version=2.7.1"
#tool "nuget:?package=NuGet.CommandLine&version=6.13.2"
#addin "nuget:?package=Cake.StrongNameSigner&version=0.2.0"
using Cake.Common.Diagnostics;
using Cake.Git;
using System.Text.RegularExpressions;
using Cake.Common.Tools.DotNet;

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var dbgSuffix = (configuration == "Debug" ? "-dbg" : "");

var buildDir = new DirectoryPath("./bin").Combine(configuration);
var buildDirNuget = buildDir.Combine("DiadocApi.Nuget");
var DiadocApiSolutionPath = "./DiadocApi.sln";
var binariesNet35Zip = buildDir.CombineWithFilePath("diadocsdk-csharp-net35-binaries.zip");
var binariesNet45Zip = buildDir.CombineWithFilePath("diadocsdk-csharp-net45-binaries.zip");
var binariesNet461Zip = buildDir.CombineWithFilePath("diadocsdk-csharp-net461-binaries.zip");
var binariesNetStandard2Zip = buildDir.CombineWithFilePath("diadocsdk-csharp-netstandard2.0-binaries.zip");
var needSigning = false;
var clearVersion = "";
var assemblyVersion = "";
var semanticVersionForNuget = "";
var semanticVersion = "";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Setup(context =>
{
	if (BuildSystem.IsRunningOnGitHubActions && BuildSystem.GitHubActions.Environment.PullRequest.IsPullRequest)
	{
		needSigning = false;
		return;
	}

	if (FileExists("./src/diadoc.snk"))
	{
		needSigning = true;
		return;
	}

	if (HasEnvironmentVariable("diadoc_signing_secret"))
	{
		var secureFileArguments = new ProcessSettings()
			.WithArguments(x =>	x
				.AppendSwitch("-decrypt", @"src\diadoc.snk.enc")
				.AppendSwitchSecret("-secret", EnvironmentVariable("diadoc_signing_secret")));
		var secureFilePath = context.Tools.Resolve("secure-file.exe");
		var exitCode = StartProcess(secureFilePath, secureFileArguments);
		if (exitCode != 0)
		{
			Warning($"secure-file exit with error {exitCode}");
			return;
		}
		needSigning = true;
		return;
	}
});

Task("Clean")
	.Does(() =>
	{
		CleanDirectory(buildDir);
	});

Task("GenerateVersionInfo")
	.Does(context =>
	{
		var tagVersion = GetVersionFromTag();
		clearVersion = ClearVersionTag(tagVersion) ?? "1.0.0";
		semanticVersionForNuget = GetSemanticVersionV1(clearVersion);
		semanticVersion = semanticVersionForNuget + dbgSuffix;

		var versionParts = clearVersion.Split('.');
		var majorVersion = 1;
		var minorVersion = 0;
		int.TryParse(versionParts[0], out majorVersion);
		if (versionParts.Length > 1)
			int.TryParse(versionParts[1], out minorVersion);
			
		assemblyVersion = $"{majorVersion}.{minorVersion}.0.0";

		if (!string.IsNullOrEmpty(clearVersion))
		{
			Information($"Version from tag: {clearVersion}");
			Information($"Assembly version: {assemblyVersion}");
			Information($"Nuget version: {semanticVersionForNuget}");
			Information($"Semantic version: {semanticVersion}");
		}
	});

Task("GenerateProtoFiles")
	.Does(() =>
	{
		if (!FileExists("./tools/protobuf-net.1.0.0.280/Tools/protobuf-net.dll"))
			CopyFileToDirectory("./tools/protobuf-net.1.0.0.280/lib/protobuf-net.dll", "./tools/protobuf-net.1.0.0.280/Tools");

		var files = GetFiles("./proto/**/*.proto");
		var filesWithError = files.AsParallel()
			.Select(x => GenerateProtoFile(x))
			.Where(x => x != null)
			.ToList();

		if (filesWithError.Count > 0)
			throw new Exception("There was several errors when generating proto classes");

		FilePath GenerateProtoFile(FilePath file)
		{
			var sourceProtoDir = new DirectoryPath("./proto/").MakeAbsolute(Context.Environment);
			var destinationProtoDir = new DirectoryPath("./src/Proto/").MakeAbsolute(Context.Environment);

			var outputFile = file.AppendExtension("cs");
			var relativeFile = sourceProtoDir.GetRelativePath(file);
			var destinationFile = destinationProtoDir.CombineWithFilePath(relativeFile).AppendExtension("cs");

			EnsureDirectoryExists(destinationFile.GetDirectory());

			var protogenArguments = new ProcessSettings
			{
				Arguments = $"-i:{file} -o:{destinationFile} -q",
				WorkingDirectory = sourceProtoDir
			};

			var exitCode = StartProcess("./tools/protobuf-net.1.0.0.280/Tools/protogen.exe", protogenArguments);
			if (exitCode != 0)
			{
				Error($"Error processing file {file} to {outputFile}, protogen exit code: {exitCode}");
				return file;
			}
			return null;
		}
	});

Task("Build")
	.IsDependentOn("GenerateProtoFiles")
	.Does(() =>
	{
		var settings = new DotNetBuildSettings
		{
			Configuration = configuration,
			ArgumentCustomization = (args) =>
			{
				return args
					.Append("/p:PackageVersion={0}", semanticVersionForNuget)
					.Append("/p:AssemblyVersion={0}", assemblyVersion)
					.Append("/p:AssemblyFileVersion={0}", assemblyVersion)
					.Append("/p:FileVersion={0}", assemblyVersion)
					.Append("/p:AssemblyInformationalVersion={0}", semanticVersion);
			}
		};
		DotNetBuild(DiadocApiSolutionPath, settings);
	});

Task("Repack")
	.IsDependentOn("Build")
	.Does(() =>
	{
		var sourceDir = buildDir.Combine("DiadocApi");
		var outputDir = buildDir.Combine("DiadocApi.Nuget");
		var keyFile = needSigning
			? new FilePath("./src/diadoc.snk")
			: null;

		RepackWithILMerge("net35", TargetPlatformVersion.v2, keyFile);
		RepackWithILMerge("net45", TargetPlatformVersion.v4, keyFile);
		RepackWithILMerge("net461", TargetPlatformVersion.v4, keyFile);
		RepackWithILRepack("netstandard2.0", TargetPlatformVersion.v4, keyFile);

		void RepackWithILMerge(string targetFramework, TargetPlatformVersion targetPlatformVersion, FilePath signWithKeyFile)
		{
			var ilMergeSettings = new ILMergeSettings
			{
				Internalize = true
			};

			if (needSigning)
			{
				var keyFileAbsolutePath = signWithKeyFile.MakeAbsolute(Context.Environment).FullPath;
				ilMergeSettings.ArgumentCustomization = args => args.Append("/keyfile:" + keyFileAbsolutePath);
			}

			ilMergeSettings.TargetPlatform = new TargetPlatform(targetPlatformVersion);

			CreateDirectory(outputDir.Combine(targetFramework));
			ILMerge(
					outputDir.Combine(targetFramework).CombineWithFilePath("DiadocApi.dll"),
					sourceDir.Combine(targetFramework).CombineWithFilePath("DiadocApi.dll"),
					new FilePath[] { sourceDir.Combine(targetFramework).CombineWithFilePath("protobuf-net.dll") },
					ilMergeSettings);
		}

		void RepackWithILRepack(string targetFramework, TargetPlatformVersion targetPlatformVersion, FilePath signWithKeyFile)
		{
			var ilRepackSettings = new ILRepackSettings
			{
				Internalize = true,
				TargetPlatform = targetPlatformVersion,
				WorkingDirectory = outputDir.Combine(targetFramework),
				Libs = new [] { sourceDir.Combine(targetFramework) }.ToList(),
				Keyfile = signWithKeyFile,
				DelaySign = signWithKeyFile != null
					? true
					: false
			};

			CreateDirectory(outputDir.Combine(targetFramework));
			ILRepack(
				outputDir.Combine(targetFramework).CombineWithFilePath("DiadocApi.dll"),
				sourceDir.Combine(targetFramework).CombineWithFilePath("DiadocApi.dll"),
				new FilePath[] { sourceDir.Combine(targetFramework).CombineWithFilePath("protobuf-net.dll") },
				ilRepackSettings);

			if (signWithKeyFile != null)
			{
				StrongNameSigner(new StrongNameSignerSettings {
					AssemblyFile = outputDir.Combine(targetFramework).CombineWithFilePath("DiadocApi.dll"),
					KeyFile =  signWithKeyFile
				});
				DeleteFiles(GetFiles(outputDir.Combine(targetFramework).FullPath + "/*.unsigned"));
			}
		}
	});

Task("PrepareBinaries")
	.IsDependentOn("GenerateVersionInfo")
	.IsDependentOn("Repack")
	.Does(() =>
	{
		DeleteFiles(GetFiles(buildDir.FullPath + "/**/JetBrains.Annotations*"));

		CopyFileToDirectory("./LICENSE.md", buildDirNuget);

		PrepareBinaries("net35", binariesNet35Zip);
		PrepareBinaries("net45", binariesNet45Zip);
		PrepareBinaries("net461", binariesNet461Zip);
		PrepareBinaries("netstandard2.0", binariesNetStandard2Zip);

		void PrepareBinaries(string targetFramework, FilePath outputZip)
		{
			var files = GetFiles(buildDir.FullPath + $"/DiadocApi/{targetFramework}/*.*")
				.Where(x =>
					!x.FullPath.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) &&
					!x.FullPath.EndsWith(".pdb", StringComparison.OrdinalIgnoreCase) &&
					!x.FullPath.EndsWith(".deps.json", StringComparison.OrdinalIgnoreCase));
			CopyFiles(files, buildDirNuget.Combine(targetFramework));
			Zip(buildDirNuget, outputZip, GetFiles(buildDirNuget + $"/{targetFramework}/*.*"));
		}
	});

Task("Dotnet-Pack")
	.IsDependentOn("PrepareBinaries")
	.Does(() =>
	{
		var settings = new DotNetCorePackSettings
		{
			Configuration = configuration,
			NoBuild = true,
			NoRestore = true,
			NoDependencies = true,
			OutputDirectory = "./bin/Release",
			ArgumentCustomization = (args) =>
			{
				return args
					.Append("/p:PackageVersion={0}", semanticVersionForNuget)
					.Append("/p:AssemblyVersion={0}", assemblyVersion)
					.Append("/p:AssemblyFileVersion={0}", assemblyVersion)
					.Append("/p:FileVersion={0}", assemblyVersion)
					.Append("/p:AssemblyInformationalVersion={0}", semanticVersion);
			}
		};

		DotNetCorePack("./src/DiadocApi.csproj", settings);
	});

Task("Test")
	.IsDependentOn("Build")
	.Does(() =>
	{
		DotNetCoreTest(DiadocApiSolutionPath, new DotNetCoreTestSettings {
			NoRestore = false,
			NoBuild = true,
			Configuration = configuration
		});
	});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("FullBuild")
	.IsDependentOn("GenerateVersionInfo")
	.IsDependentOn("Build");

Task("Rebuild")
	.IsDependentOn("Clean")
	.IsDependentOn("GenerateVersionInfo")
	.IsDependentOn("Build");

Task("Default")
	.IsDependentOn("PrepareBinaries")
	.IsDependentOn("Build")
	.IsDependentOn("Test")
	.IsDependentOn("Repack")
	.IsDependentOn("Dotnet-Pack");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);

//////////////////////////////////////////////////////////////////////
// HELPERS
//////////////////////////////////////////////////////////////////////

public string GetVersionFromTag()
{
	var lastestTag = "";

	if (BuildSystem.GitHubActions.IsRunningOnGitHubActions)
	{
		var workflow = BuildSystem.GitHubActions.Environment.Workflow;
		if(EnvironmentVariable("github_ref_type") == "tag")
		{
			return workflow.Ref.Replace("refs/tags/", "");
		}
	}

	if (string.IsNullOrEmpty(lastestTag))
	{
		try
		{
			lastestTag = GitDescribe(".", false, GitDescribeStrategy.Tags);
		}
		catch (Exception ex)
		{
			Warning(ex.Message, new object[] {});
		}
	}

	return lastestTag;
}

public string GetSemanticVersionV1(string clearVersion)
{
	if (BuildSystem.GitHubActions.IsRunningOnGitHubActions)
	{
		var workflow = BuildSystem.GitHubActions.Environment.Workflow;
		if(EnvironmentVariable("github_ref_type") == "tag")
		{
			return clearVersion;
		}
		
		var buildNumber = workflow.RunNumber;
		return $"{clearVersion}-CI{buildNumber}";
	}

	return $"{clearVersion}-dev";
}

public static string ClearVersionTag(string lastestTag)
{
	if (string.IsNullOrEmpty(lastestTag))
		return null;

	if (lastestTag.StartsWith("versions/"))
	{
		lastestTag = lastestTag.Substring("versions/".Length);
	}

	var match = Regex.Match(lastestTag, @"^([0-9]+\.[0-9]+(\.[0-9]+)*)");
	return match.Success
		? match.Value
		: lastestTag;
}