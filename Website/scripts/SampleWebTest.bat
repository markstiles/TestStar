@echo off
set TestLauncherPath=%0\..\..\bin\NUnitTesting.TestLauncher.exe

@echo on
"%TestLauncherPath%" "-w" "NUnitTesting.WebTests" "PingTest" "0" "0" ""

pause