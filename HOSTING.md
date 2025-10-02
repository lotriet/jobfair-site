# 🚀 Free Hosting Guide for .NET Micro Demo

## 🔥 **Azure App Service (Recommended - Professional)**

### Best Choice for Professional Portfolio

Azure App Service Free tier offers:
- ✅ **Professional domain**: `yourname.azurewebsites.net`
- ✅ **Industry standard**: Employers recognize Azure
- ✅ **Easy deployment**: GitHub Actions integration
- ✅ **Custom domains**: Add your own domain
- ✅ **60 CPU minutes/day**: Perfect for demo purposes

**Quick Start**: See `AZURE-DEPLOYMENT.md` for detailed setup guide

---

## 🚀 **Railway.app (Alternative - Easiest)**

### Step 1: Setup

1. Go to [railway.app](https://railway.app)
2. Sign in with GitHub
3. Click "Deploy from GitHub repo"
4. Select your repository containing the demo

### Step 2: Deploy

- Railway auto-detects the Dockerfile
- Deploys automatically
- Gives you a live URL like: `https://your-app-name.up.railway.app`

### Step 3: Share

- Send the URL to anyone
- They see the demo landing page immediately
- Interactive API docs at `/api-docs`

---

## 🌐 **Alternative Free Hosting Options**

### **Render.com**

```bash
# Add render.yaml to your repo:
services:
  - type: web
    name: dotnet-micro-demo
    env: docker
    dockerfilePath: ./Dockerfile
    healthCheckPath: /health
```

### **fly.io**

```bash
# Install flyctl and run:
fly launch
fly deploy
```

### **Azure Container Instances** (Free tier)

```bash
az container create \
  --resource-group myResourceGroup \
  --name dotnet-demo \
  --image your-registry/dotnet-demo \
  --dns-name-label dotnet-demo-unique \
  --ports 8080
```

---

## 📱 **What Users See When They Visit:**

### **Root URL (`/`)**

- Beautiful landing page explaining the demo
- "API LIVE" status indicator
- Big buttons to try the API
- Code examples shown inline

### **Interactive API (`/api-docs`)**

- Swagger UI with all endpoints
- "Try it out" buttons for live testing
- Real async/await calls to SQLite

### **Direct API (`/api/products`)**

- Raw JSON response
- Shows async database query results
- Demonstrates retry policy in action

---

## 🎯 **Application Features:**

- ✅ **Interactive Interface** - Beautiful landing page with live status
- ✅ **API Documentation** - Swagger UI for live testing
- ✅ **Self-Documenting** - Code examples shown inline
- ✅ **Professional** - Production-ready application
- ✅ **Shareable** - Accessible via any URL

## 🔗 **Sample URLs After Deployment:**

```
Main Demo:     https://your-app.railway.app/
API Docs:      https://your-app.railway.app/api-docs
Health:        https://your-app.railway.app/health
Products API:  https://your-app.railway.app/api/products
```

## 🛠️ **Local Testing:**

```bash
cd DotNetMicroDemo
dotnet run
# Visit: http://localhost:8080
```

The demo now has a **professional landing page** that immediately shows visitors what's running and how to interact with it!
