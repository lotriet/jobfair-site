using DotNetMicroDemo.Models;

namespace DotNetMicroDemo.Services
{
    public class SmartChatbotService
    {
        private readonly LLMService _llmService;
        private readonly ILogger<SmartChatbotService> _logger;
        private readonly Random _random;
        private readonly List<string> _suggestedQuestions;

        public SmartChatbotService(LLMService llmService, ILogger<SmartChatbotService> logger)
        {
            _llmService = llmService;
            _logger = logger;
            _random = new Random();
            _suggestedQuestions = InitializeSuggestions();
        }

        public async Task<string> GetResponseAsync(string userMessage)
        {
            if (string.IsNullOrWhiteSpace(userMessage))
                return "👋 Hi! I'm Christo's AI assistant powered by GPT-4. I can answer ANY question you have about anything, but I'm particularly knowledgeable about his work experience, technical skills, and career goals. What would you like to know?";

            // Add realistic thinking delay
            await Task.Delay(500 + _random.Next(1000));

            try
            {
                // Use real LLM for all responses
                var response = await _llmService.ChatWithContext(userMessage);
                
                // If response is empty or generic error, provide fallback
                if (string.IsNullOrEmpty(response) || response.Contains("technical difficulties"))
                {
                    return GetFallbackResponse(userMessage);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting chatbot response");
                return GetFallbackResponse(userMessage);
            }
        }

        public List<string> GetSuggestedQuestions()
        {
            return _suggestedQuestions;
        }

        private string GetFallbackResponse(string userMessage)
        {
            var normalizedMessage = userMessage.ToLowerInvariant();

            // Provide intelligent fallbacks based on question type
            if (normalizedMessage.Contains("skill") || normalizedMessage.Contains("technology") || normalizedMessage.Contains("tech"))
            {
                return "🚀 I specialize in .NET 8, C#, ASP.NET Core Web APIs, SQL Server, SQLite, Entity Framework Core, and Azure cloud services. I'm experienced in async/await patterns, microservices, and modernizing legacy applications. Want to know about any specific technology?";
            }

            if (normalizedMessage.Contains("experience") || normalizedMessage.Contains("work") || normalizedMessage.Contains("job") || normalizedMessage.Contains("career"))
            {
                return "💼 I have extensive experience modernizing legacy desktop applications into fast, clean REST-backed services using .NET and SQL databases. My focus is on building measurably reliable systems. I've worked on public records web access, payment integrations, and database optimization. This portfolio demo showcases my current skills!";
            }

            if (normalizedMessage.Contains("available") || normalizedMessage.Contains("hire") || normalizedMessage.Contains("opportunity"))
            {
                return "✅ Yes! I'm actively seeking new opportunities in software development. I'm particularly interested in .NET development, API projects, and cloud solutions. Ready to start immediately! Contact me at gclotriet@outlook.com";
            }

            if (normalizedMessage.Contains("contact") || normalizedMessage.Contains("reach") || normalizedMessage.Contains("email"))
            {
                return "� You can reach me at:\n• **Email**: gclotriet@outlook.com\n• **LinkedIn**: linkedin.com/in/gclotriet\n• **GitHub**: github.com/lotriet\n\nI typically respond within a few hours!";
            }

            if (normalizedMessage.Contains("project") || normalizedMessage.Contains("demo") || normalizedMessage.Contains("portfolio"))
            {
                return "🎯 This portfolio demo showcases a .NET 8 microservice with:\n• SQLite database with Entity Framework\n• Async/await patterns throughout\n• Polly retry policies for resilience\n• Azure cloud deployment\n• Interactive API testing\n\nTry the live demo above to see it in action!";
            }

            if (normalizedMessage.Contains("salary") || normalizedMessage.Contains("rate") || normalizedMessage.Contains("cost"))
            {
                return "� I'm open to discussing compensation based on the role and responsibilities. I believe in fair market rates that reflect the value delivered. Let's talk about the opportunity first!";
            }

            if (normalizedMessage.Contains("hello") || normalizedMessage.Contains("hi") || normalizedMessage.Contains("hey"))
            {
                return "👋 Hello! Great to meet you! I'm Christo's AI assistant. I can tell you about his .NET expertise, work experience, projects, or anything else you'd like to know. What interests you most?";
            }

            // General fallback with suggestions
            var fallbacks = new[]
            {
                "🤔 That's an interesting question! I'd be happy to help. Could you ask about my technical skills, work experience, or current availability?",
                "💡 I can share details about my .NET development experience, recent projects, or how to get in touch. What would be most helpful?",
                "🚀 Feel free to ask about my programming expertise, database skills, cloud experience, or anything else!"
            };

            return fallbacks[_random.Next(fallbacks.Length)] + 
                   $"\n\n**Try asking**: \"{_suggestedQuestions[_random.Next(_suggestedQuestions.Count)]}\"";
        }

        private List<string> InitializeSuggestions()
        {
            return new List<string>
            {
                "What are your main technical skills?",
                "Tell me about your .NET experience",
                "What projects have you worked on recently?",
                "Are you available for new opportunities?",
                "What's your experience with cloud technologies?",
                "How many years of experience do you have?",
                "What databases do you work with?",
                "Tell me about your API development experience",
                "What's your strongest programming language?",
                "How can I contact you?",
                "What kind of role are you looking for?",
                "What's your experience with Azure?"
            };
        }
    }
}