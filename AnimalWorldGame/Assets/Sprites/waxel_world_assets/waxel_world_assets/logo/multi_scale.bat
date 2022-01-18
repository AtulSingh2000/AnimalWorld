@echo off 

for /f tokens^=* %%i in ('where /r . *') do aseprite.exe -b %%~dpi^%%~nxi --trim --color-mode rgb --save-as %%~dpi^_%%~nxi
