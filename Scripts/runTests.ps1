# test projects to run with OpenCover
$projects = @(
    ".\Src\XGain.Tests\XGain.Tests.csproj"
);

# 
function RunTests($path){
    Write-Host $path
    dotnet test $path -c Release
}

# run unit tests and calculate code coverage for each test project
foreach ($project in $projects) {
    RunTests $($project)
}

# Set build as failed if any error occurred
if($LastExitCode -ne 0) { $host.SetShouldExit($LastExitCode )  }

write-host "Testing finished"