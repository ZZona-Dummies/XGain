param (
    [Parameter(Mandatory=$true)][string]$version = 0
 )


# projects to build
$projects = @(
    "Src\XGain",
    "Src\Tests\XGain.Tests\"
);

# build function for project
function Build($path){
    dotnet build $path --version-suffix $version;
}

function RestorePackages(){
    dotnet restore;
}

write-host "Build started";

# restore packages
write-host "Restoring packages";
RestorePackages;

# build each project
foreach ($project in $projects){
    write-host "Building " $project "with suffix version " $version
    Build($project);
}

write-host "Build finished";