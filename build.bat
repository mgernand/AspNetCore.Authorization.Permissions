@echo off
setlocal

:: Configuration
set PACKAGE_DIR=../../Packages

dotnet build ./AspNetCore.Authorization.Permissions.slnx -c Release -p:PackageOutputPath=../../Packages

echo.
echo All packages have been built.
pause