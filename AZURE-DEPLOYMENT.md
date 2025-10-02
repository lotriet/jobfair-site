# Azure Deployment Guide

## Free Azure App Service Deployment

This application can be deployed to Azure App Service using the free F1 tier, which provides:

- 1 GB storage
- 60 CPU minutes per day
- Custom domain support (with your own domain)
- Always-on feature (requires upgrade to Basic)

## Prerequisites

1. **Azure Account**: Create a free Azure account at https://azure.microsoft.com/free/
2. **Azure CLI** (optional): Install from https://docs.microsoft.com/cli/azure/install-azure-cli

## Deployment Steps

### Option 1: Using Azure Portal (Recommended for beginners)

1. **Create App Service**:

   - Go to [Azure Portal](https://portal.azure.com)
   - Click "Create a resource" → "Web App"
   - Fill in the details:
     - **Subscription**: Your free subscription
     - **Resource Group**: Create new (e.g., "jobfair-site-rg")
     - **Name**: Choose unique name (e.g., "yourname-jobfair-site")
     - **Runtime stack**: .NET 8 (LTS)
     - **Operating System**: Linux or Windows
     - **Region**: Choose closest to your location
     - **Pricing plan**: Free F1

2. **Configure Deployment**:

   - Go to your App Service → "Deployment Center"
   - Choose "GitHub" as source
   - Authorize GitHub access
   - Select repository: `lotriet/jobfair-site`
   - Branch: `master`
   - Build provider: "GitHub Actions"
   - Click "Save"

3. **Set Environment Variables** (if needed):
   - Go to "Configuration" → "Application settings"
   - Add any required environment variables

### Option 2: Using GitHub Actions (Automated)

1. **Get Publish Profile**:

   - In Azure Portal, go to your App Service
   - Click "Get publish profile" and download the file
   - Copy the entire contents

2. **Configure GitHub Secrets**:

   - Go to your GitHub repository
   - Settings → Secrets and variables → Actions
   - Add these secrets:
     - `AZURE_WEBAPP_NAME`: Your app service name
     - `AZURE_WEBAPP_PUBLISH_PROFILE`: Paste the publish profile content

3. **Deploy**:
   - Push any changes to master branch
   - GitHub Actions will automatically deploy

### Option 3: Using Azure CLI

```bash
# Login to Azure
az login

# Create resource group
az group create --name jobfair-site-rg --location "East US"

# Create App Service plan (Free tier)
az appservice plan create --name jobfair-site-plan --resource-group jobfair-site-rg --sku FREE --is-linux

# Create web app
az webapp create --resource-group jobfair-site-rg --plan jobfair-site-plan --name your-unique-app-name --runtime "DOTNETCORE:8.0"

# Configure deployment from GitHub
az webapp deployment source config --name your-unique-app-name --resource-group jobfair-site-rg --repo-url https://github.com/lotriet/jobfair-site --branch master --manual-integration
```

## Application Configuration

The application is configured to:

- Serve the portfolio at the root URL (`/`)
- Provide API endpoints at `/api/products`
- Include Swagger UI at `/swagger`
- Use SQLite database (file-based, included in deployment)

## Post-Deployment

1. **Test the deployment**:

   - Visit `https://your-app-name.azurewebsites.net`
   - Check portfolio loads correctly
   - Test API at `https://your-app-name.azurewebsites.net/api/products`
   - Verify Swagger UI at `https://your-app-name.azurewebsites.net/swagger`

2. **Custom Domain** (Optional):
   - Free tier supports custom domains
   - Go to "Custom domains" in Azure Portal
   - Add your domain and configure DNS

## Monitoring

- Use Azure Application Insights (free tier available)
- Monitor performance and errors
- View logs in Azure Portal under "Log stream"

## Cost Management

- Free tier includes:
  - 1 GB storage
  - 60 CPU minutes/day
  - Shared compute resources
- Monitor usage in Azure Portal
- Set up billing alerts

## Troubleshooting

1. **Deployment fails**: Check GitHub Actions logs
2. **App won't start**: Check Application logs in Azure Portal
3. **Database issues**: SQLite database is included in the deployment
4. **CORS issues**: Already configured for cross-origin requests

## Free Tier Limitations

- Sleeps after 20 minutes of inactivity
- 60 CPU minutes per day limit
- Shared infrastructure
- No custom SSL certificates (uses \*.azurewebsites.net)

For production use, consider upgrading to Basic tier ($13/month) for:

- Always-on capability
- Custom SSL certificates
- Daily backups
- Dedicated compute resources
