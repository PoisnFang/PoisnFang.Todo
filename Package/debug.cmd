XCOPY "..\Client\bin\Debug\netstandard2.1\PoisnFang.Todo.Client.Oqtane.dll" "..\..\poisnfang.oqtane.framework\Oqtane.Server\bin\Debug\netcoreapp3.1\" /Y
XCOPY "..\Client\bin\Debug\netstandard2.1\PoisnFang.Todo.Client.Oqtane.pdb" "..\..\poisnfang.oqtane.framework\Oqtane.Server\bin\Debug\netcoreapp3.1\" /Y
XCOPY "..\Server\bin\Debug\netcoreapp3.1\PoisnFang.Todo.Server.Oqtane.dll" "..\..\poisnfang.oqtane.framework\Oqtane.Server\bin\Debug\netcoreapp3.1\" /Y
XCOPY "..\Server\bin\Debug\netcoreapp3.1\PoisnFang.Todo.Server.Oqtane.pdb" "..\..\poisnfang.oqtane.framework\Oqtane.Server\bin\Debug\netcoreapp3.1\" /Y
XCOPY "..\Shared\bin\Debug\netstandard2.1\PoisnFang.Todo.Shared.Oqtane.dll" "..\..\poisnfang.oqtane.framework\Oqtane.Server\bin\Debug\netcoreapp3.1\" /Y
XCOPY "..\Shared\bin\Debug\netstandard2.1\PoisnFang.Todo.Shared.Oqtane.pdb" "..\..\poisnfang.oqtane.framework\Oqtane.Server\bin\Debug\netcoreapp3.1\" /Y
XCOPY "..\Server\wwwroot\Modules\PoisnFang.Todo\*" "..\..\poisnfang.oqtane.framework\Oqtane.Server\wwwroot\Modules\PoisnFang.Todo\" /Y /S /I
