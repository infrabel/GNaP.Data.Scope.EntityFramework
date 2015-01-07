@ECHO OFF

ECHO Packing GNaP.Data.Scope.EntityFramework
nuget pack src\GNaP.Data.Scope.EntityFramework\GNaP.Data.Scope.EntityFramework.csproj -Build -Prop Configuration=Release -Exclude gnap.ico
