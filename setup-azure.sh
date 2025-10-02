#!/bin/bash

# Azure Deployment Setup Script
# This script helps set up Azure deployment for the .NET Micro API

echo "🚀 Azure App Service Deployment Setup"
echo "======================================"

# Check if Azure CLI is installed
if ! command -v az &> /dev/null; then
    echo "❌ Azure CLI not found. Please install it first:"
    echo "   Windows: https://aka.ms/installazurecliwindows"
    echo "   macOS: brew install azure-cli"
    echo "   Linux: https://docs.microsoft.com/cli/azure/install-azure-cli-linux"
    exit 1
fi

echo "✅ Azure CLI found"

# Login to Azure
echo "🔐 Logging into Azure..."
az login

# Get subscription info
echo "📋 Available subscriptions:"
az account list --output table

# Prompt for app name
read -p "Enter a unique name for your web app (e.g., yourname-jobfair-site): " APP_NAME

# Validate app name
if [[ ! $APP_NAME =~ ^[a-zA-Z0-9-]+$ ]]; then
    echo "❌ App name can only contain letters, numbers, and hyphens"
    exit 1
fi

# Set variables
RESOURCE_GROUP="${APP_NAME}-rg"
APP_SERVICE_PLAN="${APP_NAME}-plan"
LOCATION="East US"

echo "📦 Creating resources..."

# Create resource group
echo "Creating resource group: $RESOURCE_GROUP"
az group create --name $RESOURCE_GROUP --location "$LOCATION"

# Create App Service plan (Free tier)
echo "Creating App Service plan: $APP_SERVICE_PLAN"
az appservice plan create \
    --name $APP_SERVICE_PLAN \
    --resource-group $RESOURCE_GROUP \
    --sku FREE \
    --is-linux

# Create web app
echo "Creating web app: $APP_NAME"
az webapp create \
    --resource-group $RESOURCE_GROUP \
    --plan $APP_SERVICE_PLAN \
    --name $APP_NAME \
    --runtime "DOTNETCORE:8.0"

# Configure deployment from GitHub
echo "Configuring GitHub deployment..."
az webapp deployment source config \
    --name $APP_NAME \
    --resource-group $RESOURCE_GROUP \
    --repo-url https://github.com/lotriet/jobfair-site \
    --branch master \
    --manual-integration

echo ""
echo "🎉 Azure deployment setup complete!"
echo ""
echo "📱 Your app will be available at:"
echo "   https://${APP_NAME}.azurewebsites.net"
echo ""
echo "🔧 Next steps:"
echo "1. Wait 5-10 minutes for initial deployment"
echo "2. Visit your site to verify it's working"
echo "3. Set up GitHub Actions for automatic deployments (see AZURE-DEPLOYMENT.md)"
echo ""
echo "📖 Full deployment guide: AZURE-DEPLOYMENT.md"