@ECHO off
TITLE Install Print Label System Windows Service
ECHO This must be run as Administrator
PAUSE
ECHO ARE YOU SURE you want to install? Press enter twice
PAUSE
PAUSE
CD C:\myServices

ECHO Installing MyService...
ECHO ---------------------------------------------------
InstallUtil _PrintLabelSystemService\PrintLabelSystemService.exe
ECHO ---------------------------------------------------
ECHO Done.
ECHO INSTALL PROCESS FINISHED
PAUSE