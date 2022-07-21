. .\BuildFunctions.ps1
$startTime = 
$projectName = "ChurchBulletin"
$base_dir = resolve-path .\
$source_dir = "$base_dir\src"
$unitTestProjectPath = "$source_dir\UnitTests"
$integrationTestProjectPath = "$source_dir\IntegrationTests"
$projectConfig = $env:BuildConfiguration
$framework = "net6.0"
$version = $env:Version
$verbosity = "m"

$build_dir = "$base_dir\build"
$test_dir = "$build_dir\test"
    

$aliaSql = "$source_dir\Database\scripts\AliaSql.exe"
$databaseAction = $env:DatabaseAction
if ([string]::IsNullOrEmpty($databaseAction)) { $databaseAction = "Rebuild"}
$databaseName = $env:DatabaseName
if ([string]::IsNullOrEmpty($databaseName)) { $databaseName = $projectName}
$script:databaseServer = $env:DatabaseServer
if ([string]::IsNullOrEmpty($script:databaseServer)) { $script:databaseServer = "(LocalDb)\MSSQLLocalDB"}
$databaseScripts = "$source_dir\Database\scripts"

if ([string]::IsNullOrEmpty($version)) { $version = "1.0.0.0"}
if ([string]::IsNullOrEmpty($projectConfig)) {$projectConfig = "Release"}
 
Function Init {
    rd $build_dir -recurse -force  -ErrorAction Ignore
	md $build_dir > $null

	exec {
		& dotnet clean $source_dir\$projectName.sln -nologo -v $verbosity
		}
	exec {
		& dotnet restore $source_dir\$projectName.sln -nologo --interactive -v $verbosity  
		}
    

    Write-Host $projectConfig
    Write-Host $version
}


Function Compile{
	exec {
		& dotnet build $source_dir\$projectName.sln -nologo --no-restore -v $verbosity -maxcpucount --configuration $projectConfig --no-incremental /p:Version=$version /p:Authors="Programming with Palermo" /p:Product="Church Bulletin"
	}
}

Function UnitTests{
	Push-Location -Path $unitTestProjectPath

	try {
		exec {
			& dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura -nologo -v $verbosity --logger:trx `
			--results-directory $test_dir --no-build `
			--no-restore --configuration $projectConfig `
			--collect:"Code Coverage" 
		}
	}
	finally {
		Pop-Location
	}
}

Function IntegrationTest{
	Push-Location -Path $integrationTestProjectPath

	try {
		exec {
			& dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura -nologo -v $verbosity --logger:trx `
			--results-directory $test_dir --no-build `
			--no-restore --configuration $projectConfig `
			--collect:"Code Coverage" 
		}
	}
	finally {
		Pop-Location
	}
}

Function MigrateDatabaseLocal {
	exec{
		& $aliaSql $databaseAction $script:databaseServer $databaseName $databaseScripts
	}
}

Function PrivateBuild{
	$projectConfig = "Debug"
	$sw = [Diagnostics.Stopwatch]::StartNew()
	Init
	Compile
	UnitTests
	MigrateDatabaseLocal
	IntegrationTest
	$sw.Stop()
	write-host "Build time: " $sw.Elapsed.ToString()
}

Function CIBuild{
	$sw = [Diagnostics.Stopwatch]::StartNew()
	Init
	Compile
	UnitTests
	MigrateDatabaseLocal
	IntegrationTest
	$sw.Stop()
	write-host "Build time: " $sw.Elapsed.ToString()
}
