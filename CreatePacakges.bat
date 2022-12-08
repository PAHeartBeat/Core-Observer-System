@REM Build project for Realase
ECHO Building project for Release configuration.
dotnet build Src/. -c Release

@REM Creating NuGet packages
ECHO Creating NuGet Package
dotnet pack Src/. --include-symbols --force -c Release --output ./Packages/NuGet/.

@REM Creating Unity NPM Project
ECHO Step 1: Publishing Code for Unity Package
set location=com.iPAHeartBeat.Core.Observer-System
dotnet publish Src/. -c Release --no-dependencies --framework net471 --output ./Unity/Packages/%location%/Runtime/.

@REM Removing Extra DLL for Which Code Will avaialble via Unity package registries.
ECHO Step 2: Removing Extra DLLs
cd Unity/Packages/%location%/Runtime
del Newtonsoft.Json.dll
del iPAHeartBeat.Core.Abstraction.dll
del iPAHeartBeat.Core.Dependency-net471.dll
del iPAHeartBeat.Core.Extensions-net471.dll
del iPAHeartBeat.Core.Singleton-net471.dll
cd..
cd..
cd..
cd..

@REM Create Node package for Unity
ECHO Step 3: Creating Node Package for Unity Pacakge Manger.
cd Unity/Packages/%location%
npm pack --pack-destination="../../../Packages/UnityNPM/"
cd ..
cd ..
cd ..
cls
ECHO Pakcages are Created.
