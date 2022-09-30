. .\BuildFunctions.ps1
$startTime = 
$projectName = "ChurchBulletin"
$base_dir = resolve-path .\
$source_dir = "$base_dir\src"
$unitTestProjectPath = "$source_dir\UnitTests"
$integrationTestProjectPath = "$source_dir\IntegrationTests"
$acceptanceTestProjectPath = "$source_dir\AcceptanceTests"
$uiProjectPath = "$source_dir\UI\Server"
$databaseProjectPath = "$source_dir\Database"
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
	rm -r -fo $build_dir -ErrorAction Ignore
	md $build_dir > $null

	exec {
		& dotnet clean $source_dir\$projectName.sln -nologo -v $verbosity
		}
	exec {
		& dotnet restore $source_dir\$projectName.sln -nologo --interactive -v $verbosity  
		}
    

    Write-Output $projectConfig
    Write-Output $version
}


Function Compile{
	exec {
		& dotnet build $source_dir\$projectName.sln -nologo --no-restore -v `
			$verbosity -maxcpucount --configuration $projectConfig --no-incremental `
			/p:TreatWarningsAsErrors="true" `
			/p:Version=$version /p:Authors="Programming with Palermo" `
			/p:Product="Church Bulletin"
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

Function AcceptanceTest{
	$serverProcess = Start-Process dotnet.exe "run --project $source_dir\UI\Server\UI.Server.csproj --configuration $projectConfig -nologo --no-restore --no-build -v $verbosity" -PassThru
	Start-Sleep 1 #let the server process spin up for 1 second

	Push-Location -Path $acceptanceTestProjectPath

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
		Stop-Process -id $serverProcess.Id
	}
}

Function MigrateDatabaseLocal {
	exec{
		& $aliaSql $databaseAction $script:databaseServer $databaseName $databaseScripts
	}
}

Function PackageUI {    
    exec{
        & dotnet publish $uiProjectPath -nologo --no-restore --no-build -v $verbosity --configuration $projectConfig
    }
	exec{
		& dotnet-octo pack --id "$projectName.UI" --version $version --basePath $uiProjectPath\bin\$projectConfig\$framework\publish --outFolder $build_dir --overwrite
	}
}

Function PackageDatabase {    
    exec{
		& dotnet-octo pack --id "$projectName.Database" --version $version --basePath $databaseProjectPath --outFolder $build_dir --overwrite
	}
}

Function PackageAcceptanceTests {       
    # Use Debug configuration so full symbols are available to display better error messages in test failures
    exec{
        & dotnet publish $acceptanceTestProjectPath -nologo --no-restore -v $verbosity --configuration Debug
    }
	exec{
		& dotnet-octo pack --id "$projectName.AcceptanceTests" --version $version --basePath $acceptanceTestProjectPath\bin\Debug\$framework\publish --outFolder $build_dir --overwrite
	}
}

Function Package{
	Write-Output "Packaging nuget packages"
	dotnet tool install --global Octopus.DotNet.Cli | Write-Output $_ -ErrorAction SilentlyContinue #prevents red color is already installed
    PackageUI
    PackageDatabase
    PackageAcceptanceTests
}

Function PrivateBuild{
	$projectConfig = "Debug"
	$sw = [Diagnostics.Stopwatch]::StartNew()
	Init
	Compile
	UnitTests
	MigrateDatabaseLocal
	IntegrationTest
	AcceptanceTest
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
	Package
	$sw.Stop()
	write-host "Build time: " $sw.Elapsed.ToString()
}
