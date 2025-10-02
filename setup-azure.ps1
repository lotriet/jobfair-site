# Azure Deployment Setup Script (PowerShell)
# This script helps set up Azure deployment for the .NET Micro API

Write-Host "🚀 Azure App Service Deployment Setup" -ForegroundColor Cyan
Write-Host "======================================" -ForegroundColor Cyan

# Check if Azure CLI is installed
if (!(Get-Command az -ErrorAction SilentlyContinue)) {
    Write-Host "❌ Azure CLI not found. Please install it first:" -ForegroundColor Red
    Write-Host "   Download from: https://aka.ms/installazurecliwindows" -ForegroundColor Yellow
    exit 1
}

Write-Host "✅ Azure CLI found" -ForegroundColor Green

# Login to Azure
Write-Host "🔐 Logging into Azure..." -ForegroundColor Yellow
az login

# Get subscription info
Write-Host "📋 Available subscriptions:" -ForegroundColor Cyan
az account list --output table

# Prompt for app name
$APP_NAME = Read-Host "Enter a unique name for your web app (e.g., yourname-jobfair-site)"

# Validate app name
if ($APP_NAME -notmatch "^[a-zA-Z0-9-]+$") {
    Write-Host "❌ App name can only contain letters, numbers, and hyphens" -ForegroundColor Red
    exit 1
}

# Set variables
$RESOURCE_GROUP = "$APP_NAME-rg"
$APP_SERVICE_PLAN = "$APP_NAME-plan"
$LOCATION = "East US"

Write-Host "📦 Creating resources..." -ForegroundColor Yellow

# Create resource group
Write-Host "Creating resource group: $RESOURCE_GROUP" -ForegroundColor Cyan
az group create --name $RESOURCE_GROUP --location $LOCATION

# Create App Service plan (Free tier)
Write-Host "Creating App Service plan: $APP_SERVICE_PLAN" -ForegroundColor Cyan
az appservice plan create `
    --name $APP_SERVICE_PLAN `
    --resource-group $RESOURCE_GROUP `
    --sku FREE `
    --is-linux

# Create web app
Write-Host "Creating web app: $APP_NAME" -ForegroundColor Cyan
az webapp create `
    --resource-group $RESOURCE_GROUP `
    --plan $APP_SERVICE_PLAN `
    --name $APP_NAME `
    --runtime "DOTNETCORE:8.0"

# Configure deployment from GitHub
Write-Host "Configuring GitHub deployment..." -ForegroundColor Cyan
az webapp deployment source config `
    --name $APP_NAME `
    --resource-group $RESOURCE_GROUP `
    --repo-url https://github.com/lotriet/jobfair-site `
    --branch master `
    --manual-integration

Write-Host ""
Write-Host "🎉 Azure deployment setup complete!" -ForegroundColor Green
Write-Host ""
Write-Host "📱 Your app will be available at:" -ForegroundColor Cyan
Write-Host "   https://$APP_NAME.azurewebsites.net" -ForegroundColor Yellow
Write-Host ""
Write-Host "🔧 Next steps:" -ForegroundColor Cyan
Write-Host "1. Wait 5-10 minutes for initial deployment" -ForegroundColor White
Write-Host "2. Visit your site to verify it's working" -ForegroundColor White
Write-Host "3. Set up GitHub Actions for automatic deployments (see AZURE-DEPLOYMENT.md)" -ForegroundColor White
Write-Host ""
Write-Host "📖 Full deployment guide: AZURE-DEPLOYMENT.md" -ForegroundColor Cyan