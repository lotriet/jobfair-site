using DotNetMicroDemo.Models;

namespace DotNetMicroDemo.Services
{
    public class CVDataService
    {
        private readonly CVData _cvData;

        public CVDataService()
        {
            _cvData = LoadCVData();
        }

        public CVData GetCVData() => _cvData;

        private CVData LoadCVData()
        {
            // TODO: Replace this with your actual CV data
            return new CVData
            {
                Personal = new PersonalInfo
                {
                    Name = "Christo Lotriet",
                    Title = "Software Developer — .NET & C# · APIs · Databases",
                    Email = "gclotriet@outlook.com",
                    LinkedIn = "linkedin.com/in/gclotriet",
                    GitHub = "github.com/lotriet",
                    Location = "South Africa", // Update with your location
                    Summary = "Experienced software developer specializing in .NET technologies, API development, and database solutions. Passionate about modernizing legacy systems and building scalable cloud-based applications."
                },

                Experience = new List<WorkExperience>
                {
                    new WorkExperience
                    {
                        Company = "[Your Current/Recent Company]",
                        Position = "Software Developer",
                        Duration = "[Start Date] - Present",
                        Location = "[Location]",
                        Responsibilities = new List<string>
                        {
                            "Develop and maintain .NET applications and web APIs",
                            "Design and implement database solutions using SQL Server and Entity Framework",
                            "Modernize legacy desktop applications into cloud-based services",
                            "Implement async/await patterns for improved performance",
                            "Deploy applications to Azure cloud platform"
                        },
                        Technologies = new List<string> { ".NET 8", "C#", "ASP.NET Core", "Entity Framework", "SQL Server", "Azure" },
                        Achievements = new List<string>
                        {
                            "Successfully migrated legacy system to modern REST API",
                            "Improved application performance by 40% through async optimization",
                            "Reduced deployment time by implementing CI/CD pipelines"
                        }
                    }
                    // Add more work experiences here
                },

                Education = new List<Education>
                {
                    new Education
                    {
                        Institution = "[Your University/College]",
                        Degree = "[Your Degree]",
                        Field = "[Your Field of Study]",
                        Duration = "[Start Year] - [End Year]",
                        Grade = "[Your Grade/GPA if applicable]"
                    }
                    // Add more education entries here
                },

                Skills = new List<string>
                {
                    // Programming Languages
                    "C#", ".NET 8", ".NET Core", ".NET Framework",
                    
                    // Web Technologies
                    "ASP.NET Core", "Web API", "REST Services", "MVC",
                    
                    // Databases
                    "SQL Server", "SQLite", "Entity Framework Core", "LINQ",
                    
                    // Cloud & DevOps
                    "Microsoft Azure", "Azure App Services", "CI/CD", "Docker",
                    
                    // Tools & Frameworks
                    "Visual Studio", "Git", "Swagger/OpenAPI", "Postman",
                    
                    // Patterns & Practices
                    "Async/Await", "Dependency Injection", "Repository Pattern", "Clean Architecture"
                },

                Projects = new List<Project>
                {
                    new Project
                    {
                        Name = "Portfolio Micro Demo API",
                        Description = "A demonstration .NET 8 microservice showcasing modern development practices",
                        Technologies = new List<string> { ".NET 8", "SQLite", "Entity Framework", "Swagger", "Azure" },
                        Duration = "2025",
                        Link = "https://lotriet-jobfair-site.azurewebsites.net",
                        Highlights = new List<string>
                        {
                            "Implements async/await patterns throughout",
                            "Features Polly retry policies for resilience",
                            "Deployed to Azure with CI/CD pipeline",
                            "Interactive demo with real-time API testing"
                        }
                    }
                    // Add more projects here
                },

                Certifications = new List<string>
                {
                    // Add your certifications here, for example:
                    // "Microsoft Certified: Azure Developer Associate",
                    // "Microsoft Certified: .NET Developer"
                }
            };
        }

        // Helper methods for the chatbot
        public string GetExperienceSummary()
        {
            var totalYears = CalculateYearsOfExperience();
            return $"Christo has {totalYears}+ years of experience in software development, specializing in .NET technologies and cloud solutions.";
        }

        public List<string> GetTopSkills(int count = 10)
        {
            return _cvData.Skills.Take(count).ToList();
        }

        public string GetLatestRole()
        {
            var latestJob = _cvData.Experience.FirstOrDefault();
            return latestJob != null
                ? $"{latestJob.Position} at {latestJob.Company} ({latestJob.Duration})"
                : "Current role information available upon request";
        }

        public List<string> GetRecentProjects(int count = 3)
        {
            return _cvData.Projects.Take(count).Select(p => p.Name).ToList();
        }

        private int CalculateYearsOfExperience()
        {
            // Simple calculation - you can make this more sophisticated
            return _cvData.Experience.Count > 0 ? 5 : 0; // Placeholder - update with real calculation
        }
    }
}