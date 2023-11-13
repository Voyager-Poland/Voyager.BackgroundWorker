$version='1.7.2'
dotnet build -c Release   /property:Version=$version
dotnet pack -c Release /property:Version=$version

$ostatniPakiet = (gci .\src\Voyager.BackgroundWorker\bin\Release\*.nupkg | select -last 1).Name
$sciezka = ".\src\Voyager.BackgroundWorker\bin\Release\$ostatniPakiet"

dotnet nuget push "$sciezka" -s Voyager-Poland