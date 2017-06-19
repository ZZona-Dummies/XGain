# projects to build
$projects = @(
    "Src\XGain",
    "Src\XGain.Tests\",
    "Sample\SampleClient",
    "Sample\SampleServer"
);

# build function for project
function Build($path){
    dotnet build $path -c Release
}

function RestorePackages(){
    dotnet restore --no-cache;
}

write-host "Build started";

# restore packages
write-host "Restoring packages";
RestorePackages;

# build each project
foreach ($project in $projects){
    write-host "Building " $project
    Build($project);
}

# Set build as failed if any error occurred
if($LastExitCode -ne 0) { $host.SetShouldExit($LastExitCode )  }

write-host "Build finished";