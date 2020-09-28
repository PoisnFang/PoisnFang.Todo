"..\..\poisnfang.oqtane.framework\oqtane.package\nuget.exe" pack PoisnFang.Todo.nuspec 
XCOPY "*.nupkg" "..\..\poisnfang.oqtane.framework\Oqtane.Server\wwwroot\Modules\" /Y
