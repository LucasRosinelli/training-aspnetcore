# Training ASP.NET Core

This training is based on **the Little ASP.NET Core** book writen by _Nate Barbettini_, available at [http://littleasp.net/book](http://littleasp.net/book).

# Environment

- OS: Microsoft Windows 10 (x64)
  - Version: 10.0.17763
- Microsoft Visual Studio 2017
  - Version: 15.9.11
- .NET Core SDK
  - Version: 2.2.104

# Project strucuture

- **./src** folder contains source code
- **./test** folder contains *unit* and *integration* tests
- **./publish** folder contains published versions

# Tests

Open your preferred *command line tool* (e.g. **Command Prompt**, **Windows PowerShell**, **Terminal**).

If you want to run *all tests* at one time, go to the project's root **./** folder and execute:
```
dotnet test
```
All tests (*unit* and *integration*) will be executed.

If you want to run a *specific test*, go to the project's folder you want to test (e.g. *Unit*, *Integration*) under **./test** folder and execute:
```
dotnet test
```
Only this specific test will be executed.

# Publish

Open your preferred *command line tool* (e.g. **Command Prompt**, **Windows PowerShell**, **Terminal**) and go to **./src/AspNetCoreTraining** folder.

- To publish for **Windows**, execute:
```
dotnet publish -f netcoreapp2.2 -c Release -r win-x64 --output ..\..\publish\v1.0\win-x64 --self-contained true
```

- To publish for **Linux**, execute:
```
dotnet publish -f netcoreapp2.2 -c Release -r linux-x64 --output ..\..\publish\v1.0\linux-x64 --self-contained true
```