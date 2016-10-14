#addin "Cake.Git"
#tool nuget:?package=ILMerge&version=2.12.803
#tool "secure-file"
using Cake.Common.Diagnostics;
using Cake.Git;
using System.Text.RegularExpressions;

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var dbgSuffix = (configuration == "Debug" ? "-dbg" : "");

var buildDir = new DirectoryPath("./bin").Combine(configuration);
var buildDirNuget = buildDir.Combine("DiadocApi.Nuget");
var DiadocApiSolutionPath = "./DiadocApi.sln";
var binariesZip = buildDir.CombineWithFilePath("diadocsdk-csharp-binaries.zip");
var needSigning = false;

const string protobufNetDll = "./packages/protobuf-net.1.0.0.280/lib/protobuf-net.dll";
var packageVersion = ""; 

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Setup(() =>
{
	if (BuildSystem.IsRunningOnAppVeyor && AppVeyor.Environment.PullRequest.IsPullRequest)
	{
		needSigning = false;
		return;
	}
	
	if (FileExists("diadoc.snk"))
	{
		needSigning = true;
		return;
	}

	if (HasEnvironmentVariable("diadoc_signing_secret"))
	{
		var secureFileArguments = new ProcessSettings()
			.WithArguments(x =>	x
				.AppendSwitch("-decrypt", @"src\diadoc.snk.enc")
				.AppendSwitch("-secret", EnvironmentVariable("diadoc_signing_secret")));
		var exitCode = StartProcess("./tools/secure-file/tools/secure-file.exe", secureFileArguments);
		if (exitCode != 0)
		{
			Warning("secure-file exit with error {0}", exitCode);
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

Task("Restore-NuGet-Packages")
	.Does(() =>
	{
		NuGetRestore(DiadocApiSolutionPath);
	});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
	.IsDependentOn("GenerateProtoFiles")
	.Does(() =>
	{
		if(IsRunningOnWindows())
		{
			// Use MSBuild
			MSBuild(DiadocApiSolutionPath, settings => settings.SetConfiguration(configuration));
		}
		else
		{
			// Use XBuild
			XBuild(DiadocApiSolutionPath, settings => settings.SetConfiguration(configuration));
		}
	});

Task("GenerateVersionInfo")
	.Does(context =>
	{
		var tagVersion = GetVersionFromTag();
		var clearVersion = ClearVersionTag(tagVersion) ?? "1.0.0";
		var semanticVersionForNuget = GetSemanticVersionV1(clearVersion);
		var semanticVersion = GetSemanticVersionV2(clearVersion) + dbgSuffix;
		var appveyorVersion = GetAppVeyorBuildVersion(clearVersion);

		var versionParts = clearVersion.Split('.');
		var majorVersion = 1;
		var minorVersion = 0;
		int.TryParse(versionParts[0], out majorVersion);
		if (versionParts.Length > 1)
			int.TryParse(versionParts[1], out minorVersion);
		var assemblyVersion = string.Format("{0}.{1}.0.0", majorVersion, minorVersion);
		
		if (!string.IsNullOrEmpty(clearVersion))
		{
			Information("Version from tag: {0}", clearVersion);
			Information("Assembly version: {0}", assemblyVersion);
			Information("Nuget version: {0}", semanticVersionForNuget);
			Information("Semantic version: {0}", semanticVersion);
			Information("AppVeyor version: {0}", appveyorVersion);
		}

		var datetimeNow = DateTime.Now;
		var secondsPart = (long)datetimeNow.TimeOfDay.TotalSeconds;
		var assemblyInfo = new AssemblyInfoSettings
		{
			Version = assemblyVersion,
			FileVersion = semanticVersionForNuget,
			InformationalVersion = semanticVersion
		};
		packageVersion = assemblyInfo.FileVersion;
		CreateAssemblyInfo("./src/Properties/AssemblyVersion.cs", assemblyInfo);
		CreateAssemblyInfo("./Samples/Diadoc.Console/Properties/AssemblyVersion.cs", assemblyInfo);
		CreateAssemblyInfo("./Samples/Diadoc.Samples/Properties/AssemblyVersion.cs", assemblyInfo);

		if (BuildSystem.IsRunningOnAppVeyor)
		{
			AppVeyor.UpdateBuildVersion(appveyorVersion);
		}
	});

Task("GenerateProtoFiles")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() =>
	{
		if (!FileExists("./packages/protobuf-net.1.0.0.280/Tools/protobuf-net.dll"))
			CopyFileToDirectory(protobufNetDll, "./packages/protobuf-net.1.0.0.280/Tools");
			
		var files = GetFiles("./proto/**/*.proto");
		var filesWithError = files.AsParallel()
			.Select(x => GenerateProtoFile(x))
			.Where(x => x != null)
			.ToList();

		if (filesWithError.Count > 0)
			throw new Exception("There was several errors when generating proto classes");
	});

Task("ILMerge")
	.IsDependentOn("Build")
	.Does(() =>
	{
		var sourceDir = buildDir.Combine("DiadocApi");
		var outputDir = buildDir.Combine("DiadocApi.Nuget");
		CreateDirectory(outputDir.Combine("net35"));
		var ilMergeSettings = new ILMergeSettings
		{
			Internalize = true,
		};
		if (needSigning)
		{
			var keyFile = new FilePath("./src/diadoc.snk").MakeAbsolute(Context.Environment).FullPath;
			ilMergeSettings.ArgumentCustomization = args => args.Append("/keyfile:" + keyFile);
		}
		ILMerge(
			outputDir.CombineWithFilePath("net35/DiadocApi.dll"),
			sourceDir.CombineWithFilePath("net35/DiadocApi.dll"),
			new FilePath[] { protobufNetDll },
			ilMergeSettings);
	});
	
Task("PrepareBinaries")
	.IsDependentOn("GenerateVersionInfo")
	.IsDependentOn("ILMerge")
	.Does(() =>
	{
		var files = GetFiles(buildDir.FullPath + "/DiadocApi/**/*.*")
			.Where(x =>
				!x.FullPath.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) &&
				!x.FullPath.EndsWith(".pdb", StringComparison.OrdinalIgnoreCase));
		CopyFiles(files, buildDirNuget.Combine("net35"));
		CopyFileToDirectory("./LICENSE.md", buildDirNuget);
		Zip(buildDirNuget, binariesZip, GetFiles(buildDirNuget + "/**/*.*"));
	});

Task("Nuget-Pack")
	.IsDependentOn("PrepareBinaries")
	.Does(() =>
	{
		var nuGetPackSettings = new NuGetPackSettings
		{
			Version = packageVersion,
			BasePath = buildDirNuget.FullPath,
			OutputDirectory = buildDir.FullPath
		};
		NuGetPack("./nuspec/DiadocApi.nuspec", nuGetPackSettings);
	});
	
Task("PublishArtifactsToAppVeyor")
	.IsDependentOn("Nuget-Pack")
	.WithCriteria(x => BuildSystem.IsRunningOnAppVeyor)
	.Does(() =>
	{
		AppVeyor.UploadArtifact(binariesZip);
		foreach (var upload in GetFiles(buildDir + "/*.nupkg"))
		{
			AppVeyor.UploadArtifact(upload​);
		}
	});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
	.IsDependentOn("AppVeyor");

Task("FullBuild")
	.IsDependentOn("GenerateVersionInfo")
	.IsDependentOn("Build");
	
Task("Rebuild")
	.IsDependentOn("Clean")
	.IsDependentOn("GenerateVersionInfo")
	.IsDependentOn("Build");

Task("Appveyor")
	.IsDependentOn("PrepareBinaries")
	.IsDependentOn("Build")
	.IsDependentOn("ILMerge")
	.IsDependentOn("Nuget-Pack")
	.IsDependentOn("PublishArtifactsToAppVeyor");

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

	if (BuildSystem.IsRunningOnAppVeyor)
	{
		var tag = BuildSystem.AppVeyor.Environment.Repository.Tag;
		if (tag.IsTag)
		{
			return tag.Name;
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
	if (BuildSystem.IsRunningOnAppVeyor)
	{
		var tag = BuildSystem.AppVeyor.Environment.Repository.Tag;
		if (tag.IsTag)
		{
			return clearVersion;
		}
		
		var buildNumber = BuildSystem.AppVeyor.Environment.Build.Number;
		return string.Format("{0}-CI{1}", clearVersion, buildNumber);
	}
	
	return string.Format("{0}-dev", clearVersion);
}

public string GetSemanticVersionV2(string clearVersion)
{
	if (BuildSystem.IsRunningOnAppVeyor)
	{
		var tag = BuildSystem.AppVeyor.Environment.Repository.Tag;
		if (tag.IsTag)
		{
			return clearVersion;
		}

		return GetAppVeyorBuildVersion(clearVersion);		
	}
	return string.Format("{0}-dev", clearVersion); 
}

public string GetAppVeyorBuildVersion(string clearVersion)
{
	if (BuildSystem.IsRunningOnAppVeyor)
	{
		var buildNumber = BuildSystem.AppVeyor.Environment.Build.Number;
		clearVersion += string.Format("-CI.{0}", buildNumber);
		return (AppVeyor.Environment.PullRequest.IsPullRequest
			? clearVersion += string.Format("-PR.{0}", AppVeyor.Environment.PullRequest.Number)
			: clearVersion += "-" + AppVeyor.Environment.Repository.Branch);
	}
	return clearVersion;
}

public static string ClearVersionTag(string lastestTag)
{
	if (string.IsNullOrEmpty(lastestTag))
		return null;
		
	if (lastestTag.StartsWith("versions/"))
	{
		lastestTag = lastestTag.Substring("versions/".Length);
	}
	
	var match = Regex.Match(lastestTag, @"^([0-9]+\.[0-9]+(\.[0-9])*)");
	return match.Success
		? match.Value
		: lastestTag;
}

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
		Arguments = string.Format("-i:{0} -o:{1} -q", file, destinationFile),
		WorkingDirectory = sourceProtoDir 
	};

	var exitCode = StartProcess("./packages/protobuf-net.1.0.0.280/Tools/protogen.exe", protogenArguments);
	if (exitCode != 0)
	{
		Error("Error processing file {0} to {1}, protogen exit code: {2}",
			file,
			outputFile,
			exitCode);
		return file;
	}
	return null;
}
