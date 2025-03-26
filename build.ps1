$ErrorActionPreference = "Stop"
dotnet tool install --global dotnet-encrypto
dotnet tool install --global GitVersion.Tool --version 6.1.0

$keyFile = $null 
if ($env:DIADOC_SIGNING_SECRET -and (Test-Path -Path "./src/diadoc.snk.encrypted") -and $env:GITHUB_ACTIONS -ne "true") 
{
    dotnet-encrypto --roll-forward LatestMajor decrypt -i "./src/diadoc.snk.encrypted" -o "./src/diadoc.snk" -p $env:DIADOC_SIGNING_SECRET
    $keyFile = Get-Item -Path "./src/diadoc.snk"
}

$semVerObject = dotnet-gitversion --roll-forward LatestMajor /overrideconfig assembly-versioning-scheme="MajorMinorPatch" /output json /overrideconfig tag-prefix=versions/ increment=Patch mode=ContinuousDelivery | 
    ConvertFrom-Json

if ($env:GITHUB_ACTIONS -eq "true")
{

}





