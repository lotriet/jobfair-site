@echo off
echo.
echo ⏱️  .NET Application - QUICK START
echo ====================================
echo.

echo [Application Setup]
echo 1. Building the application...
cd /d "%~dp0"
dotnet build -c Release --no-restore -v quiet

echo 2. Starting the application...
start "Demo API" /MIN dotnet run -c Release --urls "http://localhost:5555"

echo 3. Waiting for API to be ready...
timeout /t 8 /nobreak > nul

:check_ready
curl -s http://localhost:5555/health > nul 2>&1
if %errorlevel% neq 0 (
    echo    Still starting... please wait
    timeout /t 2 /nobreak > nul
    goto check_ready
)

echo.
echo ✅ APPLICATION IS READY! API running at http://localhost:5555
echo.
echo 🎯 LIVE APPLICATION:
echo    1. Open: http://localhost:5555
echo    2. Browse landing page
echo    3. Click "Interactive API Docs" 
echo    4. Try GET /api/products
echo    5. View async/await implementation
echo.
echo Press any key to open the application...
pause > nul
start http://localhost:5555