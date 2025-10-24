<#
  coverlet.collector 6.0.4
  dotnet-reportgenerator-globaltool 5.4.17
  -> dotnet tool install -g dotnet-reportgenerator-globaltool
#>
# Needs Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass

# Delete folder.
function Remove-Folder-If-Exist($target_folder) {
  $exist_folder = Test-Path $target_folder
  If ($exist_folder -eq "True") {
    Remove-Item -Path $target_folder -Recurse -Force
  }
}

# Move folder.
function Move-Uuid-Folder($collect_folder, $coverage_folder) {
  $uuid_folders = Get-ChildItem -Path $Collect_folder
  $uuid_folder = $uuid_folders[0]
  $source_folder ="$collect_folder\$uuid_folder"
  Move-Item -Path $source_folder $coverage_folder
}

# Collect coverage data.
function Do-Collect($collect_folder) {
  dotnet test CarerExtensionTest --collect:"XPlat Code Coverage" --results-directory:$collect_folder
}

# Output coverage report.
function Do-Report($coverage_folder, $report_folder) {
  $coverage_file = "$coverage_folder\coverage.cobertura.xml"
  reportgenerator -reports:$coverage_file -targetdir:$report_folder -reporttypes:Html
}

$collect_folder = ".\TestResults"
$coverage_folder = ".\CoverageResults"
$report_folder = ".\CoverageReport"

Remove-Folder-If-Exist $collect_folder
Remove-Folder-If-Exist $coverage_folder
Remove-Folder-If-Exist $report_folder

Do-Collect $collect_folder
Move-Uuid-Folder $collect_folder $coverage_folder
Do-Report $coverage_folder $report_folder

Remove-Folder-If-Exist $collect_folder
Remove-Folder-If-Exist $coverage_folder
