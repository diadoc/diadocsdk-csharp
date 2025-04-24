$ErrorActionPreference = "Stop"
Push-Location -Path $PSScriptRoot
dotnet tool restore
dotnet cake ./build.cake
dotnet test --configuration "Release" "./DiadocApi.sln" --no-build --nologo -v q /clp:ErrorsOnly
dotnet pack --no-build --no-restore -o "./bin/Release/DiadocApi.Nuget" -p:PackageVersion=$env:PackageVersionForNuget /p:WarningLevel=0 "./src/DiadocApi.csproj"
Pop-Location