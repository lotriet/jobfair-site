# .NET Micro-Demo Deployment Guide

## ✅ Single Application Deployment

Your demo is now consolidated into ONE .NET application that serves both:

- **REST API** (`/api/products`, `/health`, etc.)
- **Portfolio Page** (`/portfolio.html`)
- **Static Assets** (CSS, JS, demo pages)

## 🚀 Deployment Options

### 1. **Railway** (Recommended - Free Tier)

```bash
# Already configured in your project
railway login
railway link [your-project-id]
railway up
```

- ✅ Free tier available
- ✅ Automatic deployments from Git
- ✅ Built-in SQLite support
- ✅ Custom domains

### 2. **Azure App Service** (Free Tier)

```bash
# Deploy directly from VS Code or CLI
az webapp up --name your-app-name --location eastus --sku F1
```

- ✅ F1 (Free) tier available
- ✅ Easy .NET deployment
- ✅ Integrated with Visual Studio

### 3. **Heroku** (Free tier discontinued, but still popular)

```bash
# Using buildpack
heroku create your-app-name
heroku buildpacks:set https://github.com/jincod/dotnetcore-buildpack
git push heroku main
```

### 4. **Render** (Free Tier)

- ✅ Connect your GitHub repo
- ✅ Automatic deployments
- ✅ Free tier with 750 hours/month

## 📦 What Gets Deployed

```
Your Single .NET App:
├── API Endpoints:
│   ├── /health          (Health check)
│   ├── /api/products    (REST API)
│   └── /api-docs        (Swagger UI)
├── Portfolio:
│   ├── /portfolio.html  (Your demo page)
│   └── /assets/         (CSS, JS files)
└── Database:
    └── demo.db          (SQLite file)
```

## 🔧 Pre-Deployment Checklist

1. **Update Connection String for Production**:

   ```json
   // In appsettings.json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=demo.db"
     }
   }
   ```

2. **Environment Variables** (if needed):

   - `ASPNETCORE_ENVIRONMENT=Production`
   - `ASPNETCORE_URLS=http://0.0.0.0:$PORT` (for some platforms)

3. **Test Locally**:
   ```bash
   cd DotNetMicroDemo
   dotnet run --urls "http://localhost:5000"
   # Visit: http://localhost:5000/portfolio.html
   ```

## 🌐 Live Demo URLs (After Deployment)

Once deployed, your visitors can access:

- **Portfolio**: `https://your-app.railway.app/portfolio.html`
- **API Docs**: `https://your-app.railway.app/api-docs`
- **Health Check**: `https://your-app.railway.app/health`

## 💡 Benefits of Single App Deployment

✅ **No CORS Issues** - Same domain for everything
✅ **Simpler Hosting** - One app, one deployment
✅ **Better Performance** - No cross-origin requests
✅ **Cost Effective** - Single server instance
✅ **Easier Maintenance** - Everything in one place

## 🎯 Next Steps

1. Choose a hosting platform (Railway recommended)
2. Push your code to GitHub
3. Connect the hosting platform to your repo
4. Deploy!
5. Update your portfolio links to point to the live URL

Your demo will then work exactly the same way, but from a single deployed .NET application! 🚀
