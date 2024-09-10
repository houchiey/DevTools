@echo off
set command=%1
set workbookPath=%2
set sheetName=%3
set cellAddress=%4

cd %~dp0
echo %command% "OpenExcel.xlsm" /e /m"OpenOrSelectSheetAndCell %workbookPath% %sheetName% %cellAddress%"
start %command% "OpenExcel.xlsm" /e /m"OpenOrSelectSheetAndCell %workbookPath% %sheetName% %cellAddress%"

pause