using Microsoft.AspNetCore.Mvc;
using DotNetMicroDemo.Services;
using DotNetMicroDemo.Models;

namespace DotNetMicroDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CVController : ControllerBase
    {
        private readonly CVDataService _cvDataService;

        public CVController(CVDataService cvDataService)
        {
            _cvDataService = cvDataService;
        }

        [HttpGet]
        public IActionResult GetCV()
        {
            var cvData = _cvDataService.GetCVData();
            return Ok(cvData);
        }

        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var cv = _cvDataService.GetCVData();
            var summary = new
            {
                Name = cv.Personal.Name,
                Title = cv.Personal.Title,
                Experience = _cvDataService.GetExperienceSummary(),
                TopSkills = _cvDataService.GetTopSkills(5),
                RecentProjects = _cvDataService.GetRecentProjects(),
                Contact = new
                {
                    Email = cv.Personal.Email,
                    LinkedIn = cv.Personal.LinkedIn,
                    GitHub = cv.Personal.GitHub
                }
            };

            return Ok(summary);
        }
    }
}