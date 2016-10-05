# benchmarks to run
$projects = @(
    "Src\XGain.Benchmark"
)

function Run($path) {
    dotnet run -p $path --framework netcoreapp1.0 --configuration Release
}

function RestorePackages(){
    dotnet restore
}

write-host "Benchmark started"

# restore packages
write-host "Restoring packages"
RestorePackages

# build each project
foreach ($project in $projects){
    write-host "Starting benchmark" $project
    Run($project)
}

# Set build as failed if any error occurred
if($LastExitCode -ne 0) { $host.SetShouldExit($LastExitCode )  }

write-host "Benchmark finished"