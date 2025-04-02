param (
    [string]$configuration = "Release"
)

$ErrorActionPreference = "Stop"
Write-Host -Object "`n##### Changing directory to $PSScriptRoot"
Push-Location -Path $PSScriptRoot

Write-Host -Object "`n##### Installing necessery dotnet tools"
dotnet tool install --global dotnet-encrypto --version 1.0.6
dotnet tool install --global GitVersion.Tool --version 5.12.0
dotnet tool install --global dotnet-ilrepack

Write-Host -Object "`n##### Installing legacy version of package protobuf-net (1.0.0.280)"
$null = install-Package protobuf-net -MaximumVersion 1.0.0.280 -Confirm:$false -Scope CurrentUser -Force -Destination "./tools/"

# Decrypted chapter
$keyFile = $null 
if ($env:DIADOC_SIGNING_SECRET -and (Test-Path -Path "./src/diadoc.snk.encrypted")) 
{
    Write-Host -Object "`n##### Trying to decrypt diadoc.snk.encrypted"
    dotnet-encrypto --roll-forward LatestMajor decrypt -i "./src/diadoc.snk.encrypted" -o "./src/diadoc.snk" -p $env:DIADOC_SIGNING_SECRET
    if($LASTEXITCODE)
    {
        throw "dotnet-encrypto failed for ./src/diadoc.snk.encrypted"
    }
    $keyFile = Get-Item -Path "./src/diadoc.snk"
}

# Versioning chapter
Write-Host -Object "`n##### Calculating versions"
$semVerObject = dotnet-gitversion --roll-forward LatestMajor /output json /overrideconfig assembly-versioning-scheme="MajorMinorPatch" tag-prefix=versions/ increment=Patch | 
    ConvertFrom-Json

if($LASTEXITCODE)
{
    throw "dotnet-gitversion failed for semver calculating "
}

$semVer = $semVerObject.SemVer + "-pre"
$assemblyVer = "$($semVerObject.Major)" + "." + "$($semVerObject.Minor)" + ".0.0"
if ($env:GITHUB_ACTIONS -eq "true")
{
    $semVer = $semVerObject.SemVer + "-ci" + $env:GITHUB_RUN_NUMBER
    if($env:GITHUB_REF_TYPE -eq "tag")
    {
        $semVer = $semVerObject.MajorMinorPatch
    }
}

Write-Host "Version from tag: $($semVerObject.MajorMinorPatch)"
Write-Host "Assembly version: $assemblyVer"
Write-Host "Nuget version: $semVer"
Write-Host "Semantic version: $semVer"

# Patch cs file
Write-Host -Object "`n##### Patching assemblies version"
$assemblyInfo = @"
[assembly: System.Reflection.AssemblyVersion("$assemblyVer")]
[assembly: System.Reflection.AssemblyFileVersion("$semVer")]
[assembly: System.Reflection.AssemblyInformationalVersion("$semVer")]
"@

Set-Content -Path "./src/Properties/AssemblyVersion.cs" -Value $assemblyInfo
Set-Content -Path "./Samples/Diadoc.Console/Properties/AssemblyVersion.cs" -Value $assemblyInfo
Set-Content -Path "./Samples/Diadoc.Samples/Properties/AssemblyVersion.cs" -Value $assemblyInfo

# Cleanup build dir
Remove-Item -Recurse -Force -Path "./bin/$configuration" -ErrorAction SilentlyContinue
$null = New-Item -ItemType Directory -Path "./bin/$configuration" 
$null = New-Item -ItemType Directory -Path "./src/Proto" -Force

# Generate proto files
Write-Host -Object "`n##### Generate proto classes"
Write-Host -Object "Changing directory to ./proto"
Copy-Item -Path "./tools/protobuf-net.1.0.0.280/lib/protobuf-net.dll" -Destination "./tools/protobuf-net.1.0.0.280/Tools" -Force

# Prepare linux
if($PSVersionTable.Platform -eq "Unix")
{
    Copy-Item -Path "./tools/protoc" -Destination "./tools/protobuf-net.1.0.0.280/Tools/protoc.exe" -Force
}

Push-Location -Path "./proto"
$ProtoFiles = Get-ChildItem -File -Filter "*.proto" -Recurse |
    ForEach-Object {
        ($_ | Resolve-Path -Relative) -replace "(^\.\/)|(^\.\\)"
    }

foreach($ProtoFile in $ProtoFiles)
{
    Write-Host -Object "Attempt to compile $ProtoFile"
    if($PSVersionTable.Platform -eq "Unix")
    {
        mono "../tools/protobuf-net.1.0.0.280/Tools/protogen.exe" -i:$ProtoFile -o:"../src/Proto/$ProtoFile.cs" -q | out-null
        if($LASTEXITCODE)
        {
            throw "protogen failed for $ProtoFile generating"
        }
        continue
    }

    & "../tools/protobuf-net.1.0.0.280/Tools/protogen.exe" -i:$ProtoFile -o:"../src/Proto/$ProtoFile.cs" -q
    if($LASTEXITCODE)
    {
        throw "protogen failed for $ProtoFile generating"
    }
}
Pop-Location

# Build chapter
Write-Host -Object "`n##### Build solution"
dotnet build -c $configuration "./DiadocApi.sln" --nologo -v q --property WarningLevel=0 /clp:ErrorsOnly
if($LASTEXITCODE)
{
    throw "dotnet build ./DiadocApi.sln"
}

# Test chapter
Write-Host -Object "`n##### Dotnet test"
dotnet test --configuration $configuration "./DiadocApi.sln" --no-build --nologo -v q /clp:ErrorsOnly

# Repack chapter
Push-Location -Path "./bin"
if($keyFile)
{
    $advancedArgs = "/keyFile:`"$keyFile`""
    $advancedArgsStandard = " /delaysign /keyFile:`"$keyFile`""
}

Write-Host -Object "`n##### Repack libraries"
$netTargets = @(
    @{"Target" = "net45"; "adArgs" = $advancedArgs},
    @{"Target" = "net461"; "adArgs" = $advancedArgs},
    @{"Target" = "netstandard2.0"; "adArgs" = $advancedArgsStandard}
    )

foreach($netTarget in $netTargets)
{
    Write-Host -Object "`n Repack $($netTarget.Target)"
    ilrepack --roll-forward LatestMajor /lib:"./$configuration/DiadocApi/$($netTarget.Target)" /internalize /out:"./$configuration/DiadocApi.Nuget/$($netTarget.Target)/DiadocApi.dll" "./$configuration/DiadocApi/net45/DiadocApi.dll" "./$configuration/DiadocApi/net45/protobuf-net.dll" $($netTarget.adArgs)
    if($LASTEXITCODE)
    {
        throw "ilrepack failed for $($netTarget.Target)"
    }
    Get-ChildItem -Path "./$configuration/DiadocApi/$($netTarget.Target)" -File -Filter "*.xml" | Copy-Item -Destination "./$configuration/DiadocApi.Nuget/$($netTarget.Target)" -Force
    Compress-Archive -Path "./$configuration/DiadocApi.Nuget/$($netTarget.Target)" -DestinationPath "./$configuration/diadocsdk-csharp-$($netTarget.Target)-binaries.zip" -Force
}

Copy-Item -Path "../LICENSE.md" -Destination "./$configuration/DiadocApi.Nuget/LICENSE.md" -Force
Pop-Location

Write-Host -Object "`n##### Pack nuget"
dotnet pack --no-build --no-restore -o "./bin/Release/DiadocApi.Nuget" /p:WarningLevel=0 /p:NuspecFile="../nuspec/DiadocApi.nuspec" /p:NuspecBasePath="../bin/Release/DiadocApi.Nuget" /p:NuspecProperties=version=$semVer "./src/DiadocApi.csproj"
if($LASTEXITCODE)
{
        throw "dotnet pack failed for ./src/DiadocApi.csproj"
}

Pop-Location
