<#
 coverlet.collector 6.0.4
 dotnet-reportgenerator-globaltool 5.4.17 : dotnet tool intall -g dotnet-reportgenerator-globaltool

 PowerShell: Set-ExecutionPolicy -Scope Process -ExecutionPolicy RemoteSigned
#>

# delete folder
function Remove-Folder-If-Exist ($target_folder) {
  $is_exist = Test-Path $target_folder
  If ($is_exist -eq "True") {
    Remove-Item -Path $target_folder -Recurse -Force
  }
}

# move folder
function Move-Uuid-Folder ($collect_folder, $coverage_folder) {
  $uuid_folders = Get-ChildItem -Path $collect_folder
  $uuid_folder = $uuid_folders[0]
  $src_folder = "$collect_folder\$uuid_folder"
  Move-Item -Path $src_folder $coverage_folder
}

# collect
function Do-Collect ($collect_folder) {
  dotnet test CarerExtensionTest --collect:"XPlat Code Coverage" --results-directory:$collect_folder
}

# create report html
function Do-Report ($coverage_folder, $report_folder) {
  $coverage_file = "$coverage_folder\coverage.cobertura.xml"
  reportgenarator -reports:$coverage_file -targetdir:$report_folder -reporttypes:Html
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
