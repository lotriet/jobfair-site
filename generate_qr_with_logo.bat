@echo off
echo 🎨 QR Code with Logo Generator
echo ================================
echo.

echo Step 1: Creating sample logo...
C:/Expo/DotNetMicroDemo/.venv/Scripts/python.exe create_sample_logo.py

echo.
echo Step 2: Generating QR code with logo...
C:/Expo/DotNetMicroDemo/.venv/Scripts/python.exe generate_qr.py

echo.
echo ✅ Generation complete!
echo.
echo 📁 Files created:
echo   - logo.png (sample logo - replace with your own)
echo   - portfolio_qr_code_with_logo.png (QR code with logo)
echo.
echo 🌐 HTML version: Open qr-code-generator.html in your browser
echo.
echo 💡 Tip: Replace logo.png with your own logo file for custom branding
echo.
pause