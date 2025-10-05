using System.Text;
using System.Text.Json;

namespace DotNetMicroDemo.Services
{
    public class LLMService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LLMService> _logger;
        private string? _cvKnowledge;

        public LLMService(HttpClient httpClient, IConfiguration configuration, ILogger<LLMService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;

            // Configure for GitHub Models (free to start)
            _httpClient.BaseAddress = new Uri("https://models.github.ai/inference/");
            var githubToken = _configuration["GitHubToken"] ?? Environment.GetEnvironmentVariable("GITHUB_TOKEN");
            if (!string.IsNullOrEmpty(githubToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", githubToken);
            }
        }

        public async Task<string> ChatWithContext(string userMessage)
        {
            try
            {
                // Ensure we have CV knowledge
                await EnsureCVKnowledge();

                // Add current context
                var currentTime = DateTime.Now.ToString("dddd, MMMM d, yyyy 'at' h:mm tt");
                var timeZone = TimeZoneInfo.Local.DisplayName;

                var systemPrompt = @"You are Christo Lotriet's professional AI assistant. You represent him professionally and can answer any questions.

CURRENT CONTEXT:
- Current Date & Time: " + currentTime + @" (" + timeZone + @")
- You are running live on his portfolio website
- This is a real-time conversation happening now
- You have access to his current professional information

CORE KNOWLEDGE - Christo Lotriet's Background:
" + (_cvKnowledge ?? GetDefaultCVKnowledge()) + @"

INSTRUCTIONS:
- You can answer ANY question, not just work-related ones
- For work/career questions: Use specific details from his experience above
- For general questions: Answer helpfully while maintaining professional context
- For time/date questions: Use the current date/time provided above
- For technical questions: Reference his .NET, C#, and cloud expertise
- For personal questions: Be friendly but redirect to professional topics when appropriate
- Always maintain a professional, knowledgeable tone
- Use specific examples from his experience when relevant
- If asked about availability, confirm he's actively seeking opportunities
- You are powered by GPT-4.1-mini through GitHub Models API

Remember: You ARE representing Christo, so speak in first person when appropriate (""I have experience with..."" not ""Christo has experience with..."")";

                var response = await CallChatLLM(systemPrompt, userMessage);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in LLM chat");
                return "I'm having some technical difficulties right now. Please try asking about Christo's .NET experience, skills, or how to contact him!";
            }
        }

        private async Task EnsureCVKnowledge()
        {
            if (string.IsNullOrEmpty(_cvKnowledge))
            {
                try
                {
                    _logger.LogInformation("Loading CV knowledge from website...");
                    var websiteContent = await FetchWebsiteContent("https://lotriet.dev");
                    
                    var prompt = @"Extract key professional information about Christo Lotriet from this website content. 
                    Focus on: work experience, skills, education, projects, contact info.
                    Provide a concise summary in 2-3 paragraphs that I can use to answer questions about him.
                    
                    Website content: " + websiteContent;

                    _cvKnowledge = await CallLLM(prompt, "openai/gpt-4.1-mini");
                    _logger.LogInformation("CV knowledge loaded successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error loading CV knowledge");
                    _cvKnowledge = GetDefaultCVKnowledge();
                }
            }
        }

        private string GetDefaultCVKnowledge()
        {
            return @"Christo Lotriet is a Software Developer specializing in .NET & C# · APIs · Databases. 

EXPERIENCE: Extensive experience in modernizing legacy desktop applications into fast, clean REST-backed services on .NET with SQL databases. Strong focus on measurable wins and boringly reliable systems.

SKILLS: Expert in .NET 8, C#, ASP.NET Core Web APIs, SQL Server, SQLite, Entity Framework Core, Azure cloud services, async/await patterns, dependency injection, and clean architecture. Experienced with public records web access (ASP.NET MVC/C#/SQL), secure online payments integration, and REST APIs & DB optimization.

CURRENT PROJECT: Built this live portfolio demo - a .NET 8 microservice with SQLite database, async/await patterns, Polly retry policies, deployed to Azure with CI/CD pipeline.

AVAILABILITY: Actively seeking new opportunities in software development.

CONTACT: Email gclotriet@outlook.com, LinkedIn linkedin.com/in/gclotriet, GitHub github.com/lotriet";
        }

        private async Task<string> CallChatLLM(string systemPrompt, string userMessage)
        {
            try
            {
                var requestBody = new
                {
                    model = "openai/gpt-4.1-mini",
                    messages = new[]
                    {
                        new { role = "system", content = systemPrompt },
                        new { role = "user", content = userMessage }
                    },
                    max_tokens = 800,
                    temperature = 0.7
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<JsonElement>(responseContent);

                    if (responseData.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                    {
                        var choice = choices[0];
                        if (choice.TryGetProperty("message", out var message) &&
                            message.TryGetProperty("content", out var messageContent))
                        {
                            return messageContent.GetString() ?? "No response generated.";
                        }
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("LLM API call failed with status: {StatusCode}, Response: {Response}", 
                        response.StatusCode, errorContent);
                }

                return "I'm having trouble generating a response right now.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling chat LLM API");
                return "I'm experiencing technical difficulties. Please try again.";
            }
        }

        public async Task<string> AnalyzeCVFromWebsite(string websiteUrl)
        {
            try
            {
                // First, fetch the website content
                var websiteContent = await FetchWebsiteContent(websiteUrl);

                // Analyze the content with LLM
                var prompt = $@"
Please analyze the following website content and extract key CV/resume information about Christo Lotriet.

Website Content:
{websiteContent}

Please extract and structure the following information in a JSON format:
- Personal information (name, title, contact details)
- Work experience with companies, positions, and durations
- Skills and technologies (especially .NET, C#, databases, cloud)
- Education background
- Projects and achievements
- Professional summary

Focus on technical skills, software development experience, and any relevant career highlights.
Return only valid JSON without any markdown formatting.";

                var response = await CallLLM(prompt, "openai/gpt-4.1-mini");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing CV from website");
                return "{}"; // Return empty JSON on error
            }
        }

        public async Task<string> GenerateContextualResponse(string userQuestion, string cvData)
        {
            try
            {
                var prompt = $@"
You are Christo Lotriet's AI portfolio assistant. You have access to his CV data and should answer questions professionally and accurately.

CV Data:
{cvData}

User Question: {userQuestion}

Instructions:
- Answer as if you're representing Christo professionally
- Use specific details from the CV data when relevant
- Keep responses concise but informative
- If the question is about skills, mention specific technologies
- If asked about experience, reference actual work history
- If asked about availability, mention he's actively seeking opportunities
- Use a friendly but professional tone
- Include relevant emojis sparingly for visual appeal

Provide a direct, helpful response:";

                var response = await CallLLM(prompt, "openai/gpt-4.1-mini");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating contextual response");
                return "I'm having trouble accessing that information right now, but I'd be happy to help you connect with Christo directly!";
            }
        }

        private async Task<string> FetchWebsiteContent(string url)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; CV-Analyzer/1.0)");

                var response = await client.GetStringAsync(url);

                // Simple HTML content extraction (you could use HtmlAgilityPack for more sophisticated parsing)
                var text = System.Text.RegularExpressions.Regex.Replace(response, "<[^>]+>", " ");
                text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", " ");

                // Limit content length for LLM processing
                return text.Length > 8000 ? text.Substring(0, 8000) + "..." : text;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching website content from {Url}", url);
                return "Unable to fetch website content.";
            }
        }

        private async Task<string> CallLLM(string prompt, string model)
        {
            try
            {
                var requestBody = new
                {
                    model = model,
                    messages = new[]
                    {
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 1000,
                    temperature = 0.7
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<JsonElement>(responseContent);

                    if (responseData.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                    {
                        var choice = choices[0];
                        if (choice.TryGetProperty("message", out var message) &&
                            message.TryGetProperty("content", out var messageContent))
                        {
                            return messageContent.GetString() ?? "No response generated.";
                        }
                    }
                }
                else
                {
                    _logger.LogError("LLM API call failed with status: {StatusCode}", response.StatusCode);
                }

                return "I'm having trouble generating a response right now.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling LLM API");
                return "I'm experiencing technical difficulties. Please try again.";
            }
        }
    }
}