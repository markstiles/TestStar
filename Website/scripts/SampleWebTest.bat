@echo off
set TestLauncherPath=C:\LOCAL_SYSTEM_PATH\Website\bin\NUnitTesting.WebTestLauncher.exe

@echo on
"%TestLauncherPath%" "NUnitTesting.WebTests" "PingTest" "0" "0" ""

pause