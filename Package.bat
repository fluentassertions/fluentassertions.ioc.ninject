@echo off
echo Cleaning nuget publish folder.

if exist nuget rd nuget /S /Q
md nuget

.nuget\NuGet.exe pack src\FluentAssertions.Ioc.Ninject\FluentAssertions.Ioc.Ninject.csproj -build -Properties Configuration=Release -OutputDirectory nuget