param (
    [Parameter(Mandatory=$true)][string]$fileName = ""
)

$openCover = Get-ChildItem -Path "C:\Users\$([Environment]::UserName)\.nuget\packages\OpenCover\" -Filter "OpenCover.Console.exe" -Recurse | % { $_.FullName }

# entry folder
$src = ".\Src\"

# test projects to run with OpenCover
$projects = @(
    @{Path=".\XGain.Tests"; Filter="+[XGain]*"}
)

function RunCodeCoverage($testProject, $filter) {
    & $openCover -target:dotnet.exe `"-targetargs:test $testProject`" -output:$fileName -register:'user' -filter:$filter -mergeoutput -oldStyle
}

# run unit tests and calculate code coverage for each test project
foreach ($project in $projects) {
    RunCodeCoverage $($src + $project.Path) $project.Filter
}

# Set build as failed if any error occurred
if($LastExitCode -ne 0) { $host.SetShouldExit($LastExitCode )  }

write-host "Testing finished"