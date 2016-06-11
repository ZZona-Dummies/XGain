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

write-host "Benchmark finished"