# entry folder
$src = ".\Src\"

# test projects to run with OpenCover
$projects = @(
    "Src\XGain.Tests\"
);

# 
function RunTests($path){
    dotnet test $path -c Release
}

# run unit tests and calculate code coverage for each test project
foreach ($project in $projects) {
    RunTests $($src + $project.Path) $project.Filter
}

# Set build as failed if any error occurred
if($LastExitCode -ne 0) { $host.SetShouldExit($LastExitCode )  }

write-host "Testing finished"