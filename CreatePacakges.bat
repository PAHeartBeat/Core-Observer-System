@REM Build project for Realase
ECHO Building project for Release configuration.
dotnet build Src/. -c Release

@REM Creating NuGet packages
ECHO Creating NuGet Package
dotnet pack Src/. --include-symbols --force -c Release --output ./Packages/NuGet/.

@REM Creating Unity NPM Project
ECHO Step 1: Publishing Code for Unity Package
@REM set location=__Unity-Package-Id__
dotnet publish Src/%location%.csproj -c Release --no-dependencies --framework net471 --output ./Unity/Packages/%location%/Runtime/.

@REM @REM Removing Extra DLL for Which Code Will avaialble via Unity package registries.
@REM ECHO Step 2: Removing Extra DLLs
@REM cd Unity/Packages/%location%/Runtime
@REM del Newtonsoft.Json.dll
@REM cd..
@REM cd..
@REM cd..
@REM cd..

@REM Create Node package for Unity
ECHO Step 3: Creating Node Package for Unity Pacakge Manger.
cd Unity/Packages/%location%
npm pack --pack-destination="../../../Packages/UnityNPM/"
cd ..
cd ..
cd ..
cls
ECHO Pakcages are Created.
