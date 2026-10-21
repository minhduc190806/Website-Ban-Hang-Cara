@echo off
setlocal
title Day Du An Len GitHub - Minh Duc TH29.25
cd /d "%~dp0"
echo ============================================================
echo   CONG CU DAY DU AN LEN GITHUB TU DONG
echo   Tai khoan Git may ban: Turtle-99
echo ============================================================
echo.
set /p REPO_URL="Dan link GitHub Repository cua ban vao day: "

if "%REPO_URL%"=="" (
    echo.
    echo Ban chua nhap link GitHub repository!
    pause
    exit /b
)

echo.
echo [1] Dang thiet lap dia chi Remote...
git remote remove origin >nul 2>&1
git remote add origin %REPO_URL%
git branch -M main

echo [2] Dang day ma nguon (263 files) len GitHub...
echo.
git push -u origin main

if errorlevel 1 (
    echo.
    echo ============================================================
    echo   CO LOI KHI PUSH!
    echo   - Kiem tra lai link repository da dung chua.
    echo   - Neu trinh duyet hien cua so dang nhap GitHub, ban hay dang nhap de cap quyen nhe.
    echo ============================================================
) else (
    echo.
    echo ============================================================
    echo   CHUC MUNG! DA DAY TOAN BO DU AN LEN GITHUB THANH CONG!
    echo ============================================================
)
pause
