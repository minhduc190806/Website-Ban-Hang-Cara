@echo off
setlocal
title Website Minh Duc TH29.25
cd /d "%~dp0MinhDucWeb"

echo [1] Dang kiem tra va tat tien trinh cu neu dang chay ngam...
taskkill /F /IM MinhDucWeb.exe >nul 2>&1
timeout /t 1 /nobreak >nul 2>&1

echo.
echo ============================================================
echo   DU AN WEBSITE BAN HANG CARA - MINH DUC TH29.25
echo   HE THONG ASP.NET CORE MVC VA CO SO DU LIEU
echo ============================================================
echo.
echo   Dang khoi dong may chu web va ket noi CSDL...
echo   Khi may chu san sang, he thong se tu dong bat trinh duyet!
echo.
echo   Dia chi truy cap: http://localhost:5000
echo   (Nhan to hop phim Ctrl + C de tat may chu khi dung xong)
echo ============================================================
echo.

dotnet run --project "%~dp0MinhDucWeb\MinhDucWeb.csproj"
if errorlevel 1 (
    echo.
    echo ============================================================
    echo   CO LOI KHI KHOI DONG!
    echo   Kiem tra lai ket noi CSDL trong MinhDucWeb\appsettings.json
    echo ============================================================
)
pause
