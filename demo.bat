@echo off
echo.
echo 🚀 Starting .NET Micro Demo - 20 Second Showcase
echo ================================================
echo.

echo [1/4] Building the project...
dotnet build --nologo -q

echo [2/4] Starting the API server...
start "API Server" dotnet run --urls "https://localhost:7001"

timeout /t 3 /nobreak > nul

echo [3/4] Opening Swagger UI...
start https://localhost:7001

echo [4/4] Demo endpoints ready!
echo.
echo 📝 Swagger UI: https://localhost:7001
echo 🔍 Health Check: https://localhost:7001/health  
echo 📦 Products API: https://localhost:7001/api/products
echo.
echo ⭐ Features showcased:
echo   ✅ Async/Await patterns
echo   ✅ SQL Database with Entity Framework
echo   ✅ Retry Policy with Polly
echo   ✅ REST API with full CRUD
echo.
echo Press any key to test the API with curl...
pause > nul

echo.
echo 🧪 Testing API endpoints:
echo.

echo → GET all products:
curl -s https://localhost:7001/api/products | jq .

echo.
echo → GET product by ID:
curl -s https://localhost:7001/api/products/1 | jq .

echo.
echo → Health check:
curl -s https://localhost:7001/health | jq .

echo.
echo ✨ Demo complete! Check the controller code to see async/await and retry patterns.
pause