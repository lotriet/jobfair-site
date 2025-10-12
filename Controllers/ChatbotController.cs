using Microsoft.AspNetCore.Mvc;
using DotNetMicroDemo.Services;

namespace DotNetMicroDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        private readonly SmartChatbotService _smartChatbotService;
        private readonly ContentModerationService _moderationService;

        public ChatbotController(SmartChatbotService smartChatbotService, ContentModerationService moderationService)
        {
            _smartChatbotService = smartChatbotService;
            _moderationService = moderationService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
            // Moderate input before calling chatbot logic
            var mod = _moderationService.Moderate(request.Message);
            if (!mod.Allowed)
            {
                // Return HTTP 400 with structured error
                return BadRequest(new
                {
                    error = "Message blocked by moderation.",
                    reason = mod.Reason
                });
            }
            try
            {
                var response = await _smartChatbotService.GetResponseAsync(request.Message);
                return Ok(new ChatResponse
                {
                    Message = response,
                    Timestamp = DateTime.UtcNow,
                    IsBot = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("suggestions")]
        public IActionResult GetSuggestions()
        {
            var suggestions = _smartChatbotService.GetSuggestedQuestions();
            return Ok(suggestions);
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; } = string.Empty;
    }

    public class ChatResponse
    {
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public bool IsBot { get; set; }
    }
}