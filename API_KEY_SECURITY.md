# 🔐 API Key Security Guide

This guide explains how to keep your GitHub Models API key secure in different environments.

## 🚨 **IMPORTANT SECURITY NOTICE**

**Your GitHub Token has been removed from the code** and is now configured through environment variables for security.

## 📁 **Current Configuration**

### **Local Development (.env file)**
- ✅ **File**: `.env` (ignored by git)
- ✅ **Variable**: `GITHUB_TOKEN=your_token_here`
- ✅ **Security**: Never committed to repository

### **Production Deployment**
The application reads the GitHub token from environment variables in this order:
1. `GITHUB_TOKEN` environment variable
2. `GitHubToken` from appsettings.json (should be empty in production)

## 🔧 **Setup Instructions**

### **For Local Development:**
1. Use the existing `.env` file (already configured)
2. Or set environment variable: `$env:GITHUB_TOKEN="your_token_here"`

### **For Azure App Service:**
1. Go to **Configuration** > **Application Settings**
2. Add new setting:
   - **Name**: `GITHUB_TOKEN`
   - **Value**: `your_github_token_here`
3. Click **Save**

### **For Azure Container Instances:**
```bash
az container create \
  --name dotnetmicrodemo \
  --resource-group your-rg \
  --image your-image \
  --environment-variables GITHUB_TOKEN=your_token_here
```

### **For Docker:**
```bash
docker run -e GITHUB_TOKEN=your_token_here your-image
```

## 🛡️ **Security Best Practices**

### **✅ What We Do:**
- Store secrets in environment variables
- Use `.env` files for local development (git-ignored)
- Use Azure Key Vault for production secrets
- Never commit tokens to source control

### **❌ What We Don't Do:**
- Put API keys in `appsettings.json`
- Commit `.env` files
- Hardcode secrets in source code
- Share tokens in chat/email

## 🚀 **For Production Deployment**

### **Azure App Service (Recommended):**
1. **Deploy your code** (tokens not included)
2. **Set environment variable** in Azure portal
3. **Application reads token** from environment
4. **Secure and scalable** ✅

### **Azure Key Vault (Enterprise):**
```csharp
// For enterprise applications
builder.Configuration.AddAzureKeyVault(
    keyVaultUrl, 
    new DefaultAzureCredential()
);
```

## 📋 **Environment Variable Names**

| Environment | Variable Name | Source |
|-------------|---------------|--------|
| Local Dev | `GITHUB_TOKEN` | `.env` file |
| Azure | `GITHUB_TOKEN` | App Settings |
| Docker | `GITHUB_TOKEN` | `-e` flag |
| CI/CD | `GITHUB_TOKEN` | Secrets |

## 🔍 **How to Verify Setup**

The application will log:
- ✅ `"GitHub token configured successfully"` - Token found
- ❌ `"GitHub token not found"` - Check environment setup

## 🆘 **Troubleshooting**

### **Token Not Working?**
1. Check environment variable is set: `echo $env:GITHUB_TOKEN`
2. Verify token has GitHub Models access
3. Check Azure Application Settings
4. Restart application after setting variables

### **Still Having Issues?**
1. Check the logs for authentication errors
2. Verify the token hasn't expired
3. Ensure the token has the correct permissions

---

**Remember**: Your API key is now safely stored in environment variables and will never be committed to the repository! 🛡️