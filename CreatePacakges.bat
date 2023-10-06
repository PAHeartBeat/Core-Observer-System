@REM Build project for Relapse
ECHO Building project for Release configuration.
dotnet clean Src/. -c Debug
dotnet clean Src/. -c Release
dotnet build Src/. -c Release

@REM Creating NuGet packages
ECHO Creating NuGet Package
dotnet pack Src/. --include-symbols --force -c Release --output ./Packages/NuGet/.

@REM Creating Unity NPM Project
ECHO Step 1: Publishing Code for Unity Package
set LOCATION=com.iPAHeartBeat.Core.Observer-System
del ./Unity/Packages/%LOCATION%/Runtime/*.*
dotnet publish Src/%LOCATION%.csproj -c Release --no-dependencies --framework net471 --output ./Unity/Packages/%LOCATION%/Runtime/.

@REM Removing Extra DLL for Which Code Will available via Unity package registries.
ECHO Step 2: Removing Extra DLLs
del ./Unity/Packages/%LOCATION%/Runtime/Newtonsoft.Json.dll
del ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Abstraction.dll
del ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Extensions.dll
del ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Dependency.dll
del ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Singleton.dll
del ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Abstraction.pdb
del ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Extensions.pdb
del ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Dependency.pdb
del ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Singleton.pdb

@REM Create Node package for Unity
ECHO Step 3: Creating Node Package for Unity Package Manager.
cd Unity/Packages/%LOCATION%
npm pack --pack-destination="../../../Packages/UnityNPM/"
cd ..
cd ..
cd ..
ECHO Packages are Created.
