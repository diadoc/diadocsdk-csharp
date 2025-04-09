$ErrorActionPreference = "Stop"
Push-Location -Path $PSScriptRoot
dotnet tool restore 
dotnet cake ./build.cake
Pop-Location