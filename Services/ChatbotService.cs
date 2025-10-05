using System.Text.RegularExpressions;

namespace DotNetMicroDemo.Services
{
    public class ChatbotService
    {
        private readonly Dictionary<string, Func<string>> _dynamicResponses;
        private readonly Dictionary<string, string> _staticResponses;
        private readonly List<string> _suggestedQuestions;
        private readonly Random _random;
        private readonly CVDataService _cvDataService;

        public ChatbotService(CVDataService cvDataService)
        {
            _cvDataService = cvDataService;
            _random = new Random();
            _staticResponses = InitializeStaticResponses();
            _dynamicResponses = InitializeDynamicResponses();
            _suggestedQuestions = InitializeSuggestions();
        }

        public async Task<string> GetResponseAsync(string userMessage)
        {
            if (string.IsNullOrWhiteSpace(userMessage))
                return "I'm here to help! Ask me about Christo's experience, skills, or projects.";

            // Simulate thinking delay for realistic chatbot feel
            await Task.Delay(500 + _random.Next(1000));

            var normalizedMessage = userMessage.ToLowerInvariant().Trim();

            // Check dynamic responses first (real CV data)
            foreach (var pattern in _dynamicResponses.Keys)
            {
                if (IsMatch(normalizedMessage, pattern))
                {
                    return _dynamicResponses[pattern]();
                }
            }

            // Check static responses
            foreach (var pattern in _staticResponses.Keys)
            {
                if (IsMatch(normalizedMessage, pattern))
                {
                    return _staticResponses[pattern];
                }
            }

            // Default response with helpful suggestions
            return GetDefaultResponse();
        }

        public List<string> GetSuggestedQuestions()
        {
            return _suggestedQuestions;
        }

        private bool IsMatch(string message, string pattern)
        {
            var keywords = pattern.Split('|');
            return keywords.Any(keyword => message.Contains(keyword.ToLowerInvariant()));
        }

        private Dictionary<string, Func<string>> InitializeDynamicResponses()
        {
            return new Dictionary<string, Func<string>>
            {
                // Skills based on real CV data
                ["skills|technologies|tech stack|programming languages|what can you do"] = () =>
                {
                    var skills = _cvDataService.GetTopSkills(8);
                    var skillsText = string.Join(", ", skills.Take(6)) + (skills.Count > 6 ? ", and more!" : "");
                    return $"🚀 Christo's core skills include:\n{skillsText}\n\nHe specializes in modern .NET development and cloud technologies.";
                },

                // Experience based on real CV data
                ["experience|years|work|career|background|resume"] = () =>
                {
                    var experienceSummary = _cvDataService.GetExperienceSummary();
                    var latestRole = _cvDataService.GetLatestRole();
                    return $"💼 {experienceSummary}\n\n**Current Role**: {latestRole}\n\nHis focus is on modernizing legacy systems and building scalable cloud solutions.";
                },

                // Projects based on real CV data
                ["projects|portfolio|demos|examples|work samples"] = () =>
                {
                    var projects = _cvDataService.GetRecentProjects();
                    var projectList = string.Join("\n• ", projects.Select((p, i) => $"**{p}**"));
                    return $"🎯 Recent projects include:\n• {projectList}\n\nTry the live demo above to see the Portfolio API in action!";
                },

                // Latest work experience
                ["current job|current role|latest work|recent work"] = () =>
                {
                    var latestRole = _cvDataService.GetLatestRole();
                    return $"💼 **Latest Role**: {latestRole}\n\nHe's experienced in full-stack development with a focus on backend APIs and cloud deployment.";
                },

                // Education from CV
                ["education|degree|university|college|studies"] = () =>
                {
                    var cv = _cvDataService.GetCVData();
                    if (cv.Education.Any())
                    {
                        var education = cv.Education.First();
                        return $"🎓 **Education**: {education.Degree} in {education.Field}\n📍 {education.Institution} ({education.Duration})";
                    }
                    return "🎓 Christo has a strong educational background in software development and continues learning through practical projects and certifications.";
                }
            };
        }

        private Dictionary<string, string> InitializeStaticResponses()
        {
            return new Dictionary<string, string>
            {
                // Greetings
                ["hello|hi|hey|good morning|good afternoon"] =
                    "👋 Hello! I'm Christo's AI assistant. I can tell you about his experience with .NET, C#, databases, and more. What would you like to know?",

                // Skills and Technologies
                ["skills|technologies|tech stack|programming languages|what can you do"] =
                    "🚀 Christo specializes in:\n" +
                    "• .NET 8 & C# (Expert level)\n" +
                    "• REST APIs & Web Services\n" +
                    "• SQL Server, SQLite, Entity Framework\n" +
                    "• Azure Cloud Platform\n" +
                    "• Legacy system modernization\n" +
                    "• Async/await patterns & performance optimization",

                // Experience
                ["experience|years|work|career|background|resume"] =
                    "💼 Christo has extensive experience in:\n" +
                    "• Modernizing legacy desktop applications into cloud-based REST services\n" +
                    "• Building scalable .NET APIs with proper async patterns\n" +
                    "• Database optimization and Entity Framework expertise\n" +
                    "• Azure cloud deployment and DevOps\n" +
                    "• Full-stack development with modern web technologies",

                // Projects
                ["projects|portfolio|demos|examples|work samples"] =
                    "🎯 Check out these live projects:\n" +
                    "• **This Portfolio API** - .NET 8 microservice with SQLite\n" +
                    "• **Products API** - Full CRUD with async/await patterns\n" +
                    "• **Interactive Demos** - Real-time API testing\n" +
                    "• **Azure Deployment** - Professional cloud hosting\n" +
                    "\nTry the live demo above to see the APIs in action!",

                // .NET Specific
                ["net|dotnet|.net|csharp|c#"] =
                    "⚡ Christo's .NET expertise includes:\n" +
                    "• .NET 8 (latest version) with modern C# features\n" +
                    "• ASP.NET Core Web APIs\n" +
                    "• Entity Framework Core for data access\n" +
                    "• Dependency injection and clean architecture\n" +
                    "• Performance optimization with async/await\n" +
                    "• Unit testing and best practices",

                // Databases
                ["database|sql|sqlite|entity framework|data"] =
                    "🗃️ Database expertise:\n" +
                    "• SQL Server (advanced queries, optimization)\n" +
                    "• SQLite for lightweight applications\n" +
                    "• Entity Framework Core (Code First, migrations)\n" +
                    "• Database performance tuning\n" +
                    "• LINQ and complex data operations",

                // Azure/Cloud
                ["azure|cloud|deployment|hosting|devops"] =
                    "☁️ Cloud & DevOps skills:\n" +
                    "• Azure App Services deployment\n" +
                    "• CI/CD pipelines\n" +
                    "• Docker containerization\n" +
                    "• Cloud-native application design\n" +
                    "• Infrastructure as Code",

                // Contact
                ["contact|hire|email|linkedin|github|reach out"] =
                    "📞 Ready to connect with Christo?\n" +
                    "• **Email**: gclotriet@outlook.com\n" +
                    "• **LinkedIn**: linkedin.com/in/gclotriet\n" +
                    "• **GitHub**: github.com/lotriet\n" +
                    "• **Portfolio**: This live demo site!\n" +
                    "\nHe's actively looking for new opportunities!",

                // Availability
                ["available|looking|job|opportunity|hiring|work"] =
                    "✅ **Yes, Christo is actively seeking new opportunities!**\n" +
                    "He's particularly interested in:\n" +
                    "• .NET development roles\n" +
                    "• API and microservices projects\n" +
                    "• Legacy system modernization\n" +
                    "• Cloud migration projects\n" +
                    "\nReady to start immediately!",

                // Demo specific
                ["demo|try|test|api|products"] =
                    "🧪 Try the live demo above! It showcases:\n" +
                    "• Real .NET 8 API endpoints\n" +
                    "• SQLite database with sample data\n" +
                    "• Async/await implementation\n" +
                    "• Error handling with Polly retry policies\n" +
                    "• Swagger documentation\n" +
                    "\nClick 'Try the live demo below!' to interact with the actual API!",

                // About the chatbot
                ["how do you work|ai|chatbot|bot"] =
                    "🤖 I'm a simple AI assistant built with C#! \n" +
                    "• Pattern matching for natural conversations\n" +
                    "• Async response processing\n" +
                    "• Built into the .NET API\n" +
                    "• Demonstrates basic AI concepts\n" +
                    "\nI showcase how to integrate conversational AI into web applications!"
            };
        }

        private List<string> InitializeSuggestions()
        {
            return new List<string>
            {
                "What are your main skills?",
                "Tell me about your .NET experience",
                "What projects have you worked on?",
                "Are you available for hire?",
                "How can I contact you?",
                "What's your database experience?",
                "Show me the live demo"
            };
        }

        private string GetDefaultResponse()
        {
            var responses = new[]
            {
                "🤔 I'm not sure about that specific question, but I can tell you about Christo's .NET expertise, projects, or availability!",
                "💡 Try asking about his skills, experience, or projects. I have lots of information to share!",
                "🚀 I can help you learn about Christo's .NET development experience, Azure skills, or current projects!",
                "📋 Ask me about his technical skills, work experience, or how to get in touch!"
            };

            return responses[_random.Next(responses.Length)] +
                   "\n\n**Try asking**: " + _suggestedQuestions[_random.Next(_suggestedQuestions.Count)];
        }
    }
}