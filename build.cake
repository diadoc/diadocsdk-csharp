#addin "nuget:?package=Cake.Git&version=1.0.0"
#tool nuget:?package=ILRepack&version=2.0.30
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
var DiadocApiSolutionPath = "./DiadocApi.slnf";
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
		CreateDirectory("./src/Properties");
		var assemblyInfoFilePath = "./src/Properties/AssemblyInfo.cs";
		CreateAssemblyInfo(assemblyInfoFilePath, new AssemblyInfoSettings {
			ComVisible = false,
			Product = "Diadoc",
			Title = "Diadoc.Api",
			Version = assemblyVersion,
			FileVersion = assemblyVersion,
			Copyright= "© 2010-2026 ЗАО «ПФ «СКБ Контур»",
			Guid = "3fd36034-a4be-4110-b0a9-60e4b62d0332",
			InformationalVersion = semanticVersion,
			Company = "ЗАО «ПФ «СКБ Контур»"
		});

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
					.Append("/p:InformationalVersion={0}", semanticVersion)
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
		var keyFile = needSigning ? new FilePath("./src/diadoc.snk") : null;

		var frameworks = new[] { "net35", "net45", "net461", "netstandard2.0" };

		foreach (var framework in frameworks)
			RepackWithILRepack(framework, keyFile);

		void RepackWithILRepack(string targetFramework, FilePath signWithKeyFile = null)
		{
			var source = sourceDir.Combine(targetFramework);
			var output = outputDir.Combine(targetFramework);
			CreateDirectory(output);

			var primaryDll = source.CombineWithFilePath("DiadocApi.dll");
			var newtonsoftDll = source.CombineWithFilePath("Newtonsoft.Json.dll");
			var protobufDll = source.CombineWithFilePath("protobuf-net.dll");
			
			var tempDll = output.CombineWithFilePath("DiadocApi.Internal.dll");
			var finalDll = output.CombineWithFilePath("DiadocApi.dll");

			var ilRepackPath = "./tools/ILRepack.2.0.30/tools/ilrepack.exe";

			var args1 = new ProcessArgumentBuilder();
			args1.Append("/internalize");
			args1.Append("/renameinternalized");
			args1.Append("/lib:" + source.FullPath.Quote());
			args1.Append("/out:" + tempDll.FullPath.Quote());
			args1.Append(primaryDll.FullPath.Quote());
			args1.Append(newtonsoftDll.FullPath.Quote());

			Information("Step 1: Repacking Newtonsoft with renaming...");
			if (StartProcess(ilRepackPath, new ProcessSettings { Arguments = args1 }) != 0)
				throw new CakeException("ILRepack Step 1 failed");

			var args2 = new ProcessArgumentBuilder();
			args2.Append("/internalize"); 
			args2.Append("/lib:" + source.FullPath.Quote());
			args2.Append("/out:" + finalDll.FullPath.Quote());
			args2.Append(tempDll.FullPath.Quote());
			args2.Append(protobufDll.FullPath.Quote());

			Information("Step 2: Repacking Protobuf without renaming...");
			if (StartProcess(ilRepackPath, new ProcessSettings { Arguments = args2 }) != 0)
				throw new CakeException("ILRepack Step 2 failed");

			if (FileExists(tempDll)) DeleteFile(tempDll);

			if (signWithKeyFile != null)
			{
				Information("Signing final assembly...");
				StrongNameSigner(new StrongNameSignerSettings {
					AssemblyFile = finalDll,
					KeyFile = signWithKeyFile
				});
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
					.Append("/p:InformationalVersion={0}", semanticVersion)
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