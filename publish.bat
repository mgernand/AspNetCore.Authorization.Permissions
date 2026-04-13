@echo off
setlocal

:: Configuration
set PACKAGE_DIR=./Packages
set API_KEY=
set SOURCE=https://nuget.org

:: Change to packages directory.
cd /d "%PACKAGE_DIR%"

echo Publishing all packages from: %PACKAGE_DIR%

:: Loof over all .nupkg files.
for %%f in (*.nupkg) do (
    echo.
    echo Processing: %%f
    dotnet nuget push "%%f" -k %API_KEY% -s %SOURCE% --skip-duplicate
)

cd ..

echo.
echo All packages have been processed.
pause