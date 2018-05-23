& ".\cake.ps1"

#get-childitem -directory -recurse | ?{$_.Name -eq 'artifacts'} | remove-item -force -recurse

#& dotnet restore
#& dotnet build -c Release

#$revision = @{ $true = $env:APPVEYOR_BUILD_NUMBER; $false = 1 }[$env:APPVEYOR_BUILD_NUMBER -ne $null];
#$revision = "{0:D4}" -f [convert]::ToInt32($revision, 10)

#& dotnet test ./tests/Lift.Tests/Lift.Tests.csproj -c Release
#& dotnet pack ./src/Lift/Lift.csproj -c Release -o ./artifacts --version-suffix $revision