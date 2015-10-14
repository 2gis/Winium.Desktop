Param(
    [string[]]$modules
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
#------------------------------

$configuration = 'Release'
$solution = Join-Path $PSScriptRoot '..\src\Winium.sln'
$testFiles = ,"..\src\TestApps.Tests\WindowsFormsTestApplication.Tests\bin\$configuration\WindowsFormsTestApplication.Tests.dll"
$testFiles += "..\src\TestApps.Tests\WpfTestApplication.Tests\bin\$configuration\WpfTestApplication.Tests.dll"
$releaseDir = Join-Path $PSScriptRoot '..\Release'
$artifactsDir = Join-Path $PSScriptRoot '..\Artifacts'
$driverMerged = "..\src\Winium.Desktop.Driver\bin\$configuration\Merge\Winium.Desktop.Driver.exe"
$errorLog = 'error.log'
$outputLog = 'output.log'
$assemblyInfoPath = Join-Path $PSScriptRoot '..\src\Winium.Desktop.Driver\Properties\AssemblyInfo.cs'
$changelogPath = Join-Path $PSScriptRoot '..\CHANGELOG.md'
$githubProjectName = 'Winium.Desktop'

$msbuildProperties = @{
    'Configuration' = $configuration
}

$modulesUrl = 'https://raw.githubusercontent.com/skyline-gleb/dev-help/v0.2.1/psm'

if (!(Get-Module -ListAvailable -Name PsGet))
{
    (new-object Net.WebClient).DownloadString("http://psget.net/GetPsGet.ps1") | iex
}

foreach ($module in $modules)
{
    Install-Module -ModuleUrl "$modulesUrl/$module.psm1" -Update
}
