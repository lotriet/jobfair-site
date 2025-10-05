namespace DotNetMicroDemo.Models
{
    public class CVData
    {
        public PersonalInfo Personal { get; set; } = new();
        public List<WorkExperience> Experience { get; set; } = new();
        public List<Education> Education { get; set; } = new();
        public List<string> Skills { get; set; } = new();
        public List<Project> Projects { get; set; } = new();
        public List<string> Certifications { get; set; } = new();
    }

    public class PersonalInfo
    {
        public string Name { get; set; } = "";
        public string Title { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string LinkedIn { get; set; } = "";
        public string GitHub { get; set; } = "";
        public string Location { get; set; } = "";
        public string Summary { get; set; } = "";
    }

    public class WorkExperience
    {
        public string Company { get; set; } = "";
        public string Position { get; set; } = "";
        public string Duration { get; set; } = "";
        public string Location { get; set; } = "";
        public List<string> Responsibilities { get; set; } = new();
        public List<string> Technologies { get; set; } = new();
        public List<string> Achievements { get; set; } = new();
    }

    public class Education
    {
        public string Institution { get; set; } = "";
        public string Degree { get; set; } = "";
        public string Field { get; set; } = "";
        public string Duration { get; set; } = "";
        public string Grade { get; set; } = "";
    }

    public class Project
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public List<string> Technologies { get; set; } = new();
        public string Duration { get; set; } = "";
        public string Link { get; set; } = "";
        public List<string> Highlights { get; set; } = new();
    }
}