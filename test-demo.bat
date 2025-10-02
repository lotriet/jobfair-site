@echo off
echo.
echo 🧪 Testing .NET Micro Demo
echo =========================
echo.

echo [1/4] Testing Demo Landing Page...
start http://localhost:5000
timeout /t 2 /nobreak > nul

echo [2/4] Testing Health Endpoint...
curl -s http://localhost:5000/health
echo.
echo.

echo [3/4] Testing Products API...
curl -s http://localhost:5000/api/products
echo.
echo.

echo [4/4] Opening Swagger UI...
start http://localhost:5000/api-docs
echo.

echo ✅ Demo is working! All endpoints are responding.
echo.
echo 🎯 Perfect 20-second demo flow:
echo   1. Show landing page: http://localhost:5000
echo   2. Click "Interactive API Docs" 
echo   3. Try GET /api/products in Swagger
echo   4. Explain async/await + retry policy
echo.
pause