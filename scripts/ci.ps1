Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
#------------------------------

Import-Module '.\setup.ps1' -Args (,('msbuild', 'nunit'))

# Build
Invoke-MSBuild $solution $msbuildProperties -Verbose

# Test
Start-Process $driverMerged -RedirectStandardError $errorLog -RedirectStandardOutput $outputLog
Invoke-NUnit $testFiles -Verbose
& taskkill ('/im', 'Winium.Desktop.Driver.exe', '/f')

# Artifacts
New-Item -ItemType directory -Path $artifactsDir | Out-Null
Copy-Item -Path ($errorLog, $outputLog) -Destination $artifactsDir
