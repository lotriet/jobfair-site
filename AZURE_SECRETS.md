# Azure Deployment Security Guide

## 🔐 GitHub Token Security for Azure

### Local Development

For local development, set the GitHub token as an environment variable:

**PowerShell:**

```powershell
$env:GITHUB_TOKEN = "your_github_token_here"
dotnet run
```

**Command Prompt:**

```cmd
set GITHUB_TOKEN=your_github_token_here
dotnet run
```

### Azure App Service Configuration

#### Method 1: Application Settings (Recommended)

1. Go to your Azure App Service in the portal
2. Navigate to **Configuration** → **Application settings**
3. Click **+ New application setting**
4. Set:
   - **Name**: `GITHUB_TOKEN`
   - **Value**: `your_github_token_here`
   - **Deployment slot setting**: ✅ (checked)

#### Method 2: Azure CLI

```bash
az webapp config appsettings set \
  --resource-group your-resource-group \
  --name your-app-name \
  --settings GITHUB_TOKEN="your_github_token_here"
```

#### Method 3: ARM Template

```json
{
  "type": "Microsoft.Web/sites/config",
  "apiVersion": "2021-02-01",
  "name": "[concat(parameters('appName'), '/appsettings')]",
  "dependsOn": ["[resourceId('Microsoft.Web/sites', parameters('appName'))]"],
  "properties": {
    "GITHUB_TOKEN": "[parameters('githubToken')]"
  }
}
```

### Azure Key Vault Integration (Enterprise)

For enterprise-grade security, store secrets in Azure Key Vault:

1. **Create Key Vault**:

```bash
az keyvault create \
  --name your-keyvault \
  --resource-group your-rg \
  --location eastus
```

2. **Store Secret**:

```bash
az keyvault secret set \
  --vault-name your-keyvault \
  --name "github-token" \
  --value "your_github_token_here"
```

3. **Configure App Service**:
   - Enable **System Assigned Managed Identity**
   - Grant Key Vault access permissions
   - Reference in Application Settings:
     ```
     Name: GITHUB_TOKEN
     Value: @Microsoft.KeyVault(VaultName=your-keyvault;SecretName=github-token)
     ```

### Environment Variable Priority

The LLMService checks for the token in this order:

1. **Configuration["GitHubToken"]** (appsettings.json)
2. **Environment.GetEnvironmentVariable("GITHUB_TOKEN")** (Environment variables)

```csharp
var githubToken = _configuration["GitHubToken"] ??
                  Environment.GetEnvironmentVariable("GITHUB_TOKEN");
```

### Security Best Practices

✅ **DO:**

- Use Azure Application Settings for secrets
- Enable "Deployment slot setting" for production secrets
- Use Azure Key Vault for enterprise scenarios
- Rotate tokens regularly
- Use Managed Identity when possible

❌ **DON'T:**

- Store secrets in appsettings.json
- Commit secrets to source control
- Use the same token across environments
- Share tokens in documentation

### GitHub Token Permissions

For this application, your GitHub token needs:

- **No additional scopes** (public repository access only)
- Or **repo** scope if using private repositories

### Monitoring & Alerts

Set up Azure Monitor alerts for:

- Authentication failures (401 responses)
- High API usage approaching rate limits
- Unusual geographic access patterns

### Free Tier Deployment

For Azure Free Tier deployment:

1. Use **Application Settings** (included in free tier)
2. GitHub Models API is free up to rate limits
3. No additional costs for secret management

## Deployment Commands

```bash
# Build and deploy
dotnet publish -c Release
az webapp deploy --resource-group your-rg --name your-app --src-path ./bin/Release/net8.0/publish.zip

# Verify configuration
az webapp config appsettings list --resource-group your-rg --name your-app
```

## Testing Secret Configuration

Test that secrets are properly configured:

1. Deploy without setting GITHUB_TOKEN (should use fallback responses)
2. Set GITHUB_TOKEN in Azure (should use live responses)
3. Monitor logs for authentication success/failure

Your chatbot will gracefully degrade to intelligent fallbacks if the GitHub token is missing or invalid.
