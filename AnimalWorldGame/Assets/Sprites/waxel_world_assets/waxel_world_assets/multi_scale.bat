@echo off 

for /f tokens^=* %%i in ('where /r . *') do aseprite.exe -b %%~dpi^%%~nxi --trim --scale 6 --color-mode rgb --save-as %%~dpi%^_xxx%~nxi
