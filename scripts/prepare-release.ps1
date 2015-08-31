Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
#------------------------------

Import-Module '.\setup.ps1' -Args (,('msbuild', 'nunit'))

if (Test-Path $releaseDir)
{
    Remove-Item $releaseDir -Force -Recurse
}

New-Item -ItemType directory -Path $releaseDir | Out-Null

Write-Output "Update CHANGELOG.md"
Write-Output "Update version in Assemblies"

Pause

# Build
Invoke-MSBuild $solution $msbuildProperties -Verbose

# Test
Start-Process $driverMerged -RedirectStandardError $errorLog -RedirectStandardOutput $outputLog
Invoke-NUnit $testFiles -Verbose
& taskkill ('/im', 'Winium.Desktop.Driver.exe', '/f')

# Pack driver
Add-Type -assembly "system.io.compression.filesystem"
$driverMergedPath = Split-Path $driverMerged
[IO.Compression.ZipFile]::CreateFromDirectory($driverMergedPath, "$releaseDir/Winium.Desktop.Driver.zip")

Write-Output "Add and push tag using git tag -a -m 'Version *.*.*' v*.*.*"
Write-Output "Upload and attach $releaseDir\* files to release"
