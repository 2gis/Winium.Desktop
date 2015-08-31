Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
#------------------------------

Import-Module '.\setup.ps1' -Args (,('git', 'versioning', 'changelog', 'msbuild', 'nunit', 'github'))

$version = $env:release_version
$description = $env:release_description

# Git checkout master
Invoke-Git ('checkout', 'master') -Verbose

# Update AssembyInfo
Update-AssemblyInfo $assemblyInfoPath $version -Verbose

# Update CHANGELOG.md
Update-Changelog $changelogPath $version $description -Verbose

# Build
Invoke-MSBuild $solution $msbuildProperties -Verbose

# Test
Start-Process $driverMerged -RedirectStandardError $errorLog -RedirectStandardOutput $outputLog
Invoke-NUnit $testFiles -Verbose
& taskkill ('/im', 'Winium.Desktop.Driver.exe', '/f')

# Prepare release artifacts
New-Item -ItemType directory -Path $releaseDir | Out-Null
Add-Type -assembly "system.io.compression.filesystem"
$driverMergedPath = Split-Path $driverMerged
[IO.Compression.ZipFile]::CreateFromDirectory($driverMergedPath, "$releaseDir/Winium.Desktop.Driver.zip")

# Git add changes
Invoke-Git ('add', $assemblyInfoPath) -Verbose
Invoke-Git ('add', $changelogPath) -Verbose

# Git commit and push
Invoke-GitCommit "Version $version" -Verbose
Invoke-Git ('push', 'origin', 'master') -Verbose

# Git tag and push
$buildUrl = $env:BUILD_URL
Invoke-GitTag "Version '$version'. Build url '$buildUrl'." "v$version" -Verbose
Invoke-Git ('push', 'origin', 'master', "v$version") -Verbose

# Create github release
Invoke-CreateGitHubRelease '2gis' $githubProjectName $version $description $releaseDir -Verbose
