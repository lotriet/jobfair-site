@echo off
color 0A
echo.
echo  ████████████████████████████████████████████████
echo  █                                              █
echo  █      🎯 QR Code with Logo Generator          █  
echo  █              Professional Edition            █
echo  █                                              █
echo  ████████████████████████████████████████████████
echo.

:MENU
echo 📋 What would you like to do?
echo.
echo 1. Generate QR code with current logo
echo 2. Create new logo styles (Coffee, Tech, Business, Creative)
echo 3. Use specific logo style
echo 4. Open HTML version in browser
echo 5. View generated files
echo 6. Exit
echo.
set /p choice=Enter your choice (1-6): 

if "%choice%"=="1" goto GENERATE_QR
if "%choice%"=="2" goto CREATE_LOGOS
if "%choice%"=="3" goto USE_LOGO_STYLE
if "%choice%"=="4" goto OPEN_HTML
if "%choice%"=="5" goto VIEW_FILES
if "%choice%"=="6" goto EXIT

echo Invalid choice. Please try again.
goto MENU

:GENERATE_QR
echo.
echo 🔄 Generating QR code with logo...
C:/Expo/DotNetMicroDemo/.venv/Scripts/python.exe generate_qr.py
echo.
pause
goto MENU

:CREATE_LOGOS
echo.
echo 🎨 Creating logo style options...
C:/Expo/DotNetMicroDemo/.venv/Scripts/python.exe create_advanced_logos.py
echo.
pause
goto MENU

:USE_LOGO_STYLE
echo.
echo 🎯 Available logo styles:
echo 1. Coffee/Restaurant logo
echo 2. Tech/Coding logo  
echo 3. Business/Professional logo
echo 4. Creative/Artistic logo
echo.
set /p logochoice=Choose logo style (1-4): 

if "%logochoice%"=="1" (
    copy logo_coffee.png logo.png >nul
    echo ✅ Coffee logo selected
)
if "%logochoice%"=="2" (
    copy logo_tech.png logo.png >nul
    echo ✅ Tech logo selected
)
if "%logochoice%"=="3" (
    copy logo_business.png logo.png >nul
    echo ✅ Business logo selected
)
if "%logochoice%"=="4" (
    copy logo_creative.png logo.png >nul
    echo ✅ Creative logo selected
)

echo.
echo 🔄 Generating QR code with selected logo...
C:/Expo/DotNetMicroDemo/.venv/Scripts/python.exe generate_qr.py
echo.
pause
goto MENU

:OPEN_HTML
echo.
echo 🌐 Opening HTML QR generator in browser...
start qr-code-generator.html
echo.
pause
goto MENU

:VIEW_FILES
echo.
echo 📁 Generated files:
dir *.png
echo.
echo 📄 Documentation:
echo   - QR_CODE_LOGO_README.md (Complete guide)
echo   - qr-code-generator.html (Interactive version)
echo.
pause
goto MENU

:EXIT
echo.
echo 👋 Thank you for using QR Code with Logo Generator!
echo 📁 Your files are ready for use.
echo.
pause
exit