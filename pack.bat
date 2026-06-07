@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

set /p version=<VERSION.txt

mkdir tmp
cd tmp
mkdir PatternPlus

copy /y "..\Info.json" "PatternPlus\"
copy /y "..\bin\Release\netstandard2.1\PatternPlus.dll" "PatternPlus\"

cd PatternPlus

for /f "delims=" %%a in (Info.json) do (
    SET s=%%a
    SET s=!s:$VERSION=%version%!
    echo !s! >> "..\InfoChanged.json"
)

del /f /q Info.json
move /y "..\InfoChanged.json" "Info.json"

cd ..

tar -c -f PatternPlus-%version%.zip PatternPlus

move /y PatternPlus-%version%.zip ..
cd ..

pause