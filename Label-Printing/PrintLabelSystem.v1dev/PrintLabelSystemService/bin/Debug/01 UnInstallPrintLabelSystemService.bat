@ECHO off
TITLE UNinstall Print Label System Windows Service
ECHO This must be run as Administrator
PAUSE
ECHO ARE YOU SURE you want to UNinstall? Press enter twice
PAUSE
PAUSE
CD C:\myServices

ECHO Uninstalling MyService...
ECHO ---------------------------------------------------
InstallUtil /U _PrintLabelSystemService\PrintLabelSystemService.exe
ECHO ---------------------------------------------------
ECHO Done.
ECHO UNINSTALL PROCESS FINISHED
PAUSE