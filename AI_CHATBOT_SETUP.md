# Smart AI Chatbot Setup

This enhanced chatbot uses real AI (GPT-4.1) to analyze your CV from https://lotriet.dev and provide intelligent responses.

## Setup Instructions

### 1. Get a GitHub Personal Access Token (Free)

1. Go to [GitHub Settings > Personal Access Tokens](https://github.com/settings/tokens)
2. Click "Generate new token (classic)"
3. Give it a name like "Portfolio Chatbot"
4. **No specific scopes needed** for GitHub Models (just leave defaults)
5. Copy the generated token

### 2. Configure the Token

**Option A: Environment Variable (Recommended)**

```bash
# Windows (PowerShell)
$env:GITHUB_TOKEN="your_github_token_here"

# Windows (Command Prompt)
set GITHUB_TOKEN=your_github_token_here

# Linux/Mac
export GITHUB_TOKEN=your_github_token_here
```

**Option B: appsettings.json**

```json
{
  "GitHubToken": "your_github_token_here"
}
```

### 3. Run the Application

```bash
dotnet run
```

## Features

✅ **Real AI Analysis**: Uses GPT-4.1-mini to analyze your CV  
✅ **Live Data**: Fetches content from https://lotriet.dev  
✅ **Contextual Responses**: Provides accurate, personalized answers  
✅ **Fallback Handling**: Works even if AI service is unavailable  
✅ **Cost Effective**: GitHub Models are free to start

## How It Works

1. **CV Analysis**: On first use, the AI fetches and analyzes your CV website
2. **Smart Responses**: Uses the analyzed data to answer questions intelligently
3. **Caching**: Stores analyzed data to avoid repeated API calls
4. **Fallback**: If AI fails, falls back to basic pattern matching

## Sample Questions to Try

- "What are Christo's main technical skills?"
- "Tell me about his .NET experience"
- "What projects has he worked on?"
- "Is he available for new opportunities?"
- "What's his experience with cloud technologies?"

The chatbot will provide detailed, accurate answers based on the real CV data!

## Note

Without a GitHub token, the chatbot still works but uses basic pattern matching instead of AI analysis.
