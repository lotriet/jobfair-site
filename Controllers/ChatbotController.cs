using Microsoft.AspNetCore.Mvc;
using DotNetMicroDemo.Services;

namespace DotNetMicroDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        private readonly SmartChatbotService _smartChatbotService;

        public ChatbotController(SmartChatbotService smartChatbotService)
        {
            _smartChatbotService = smartChatbotService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatRequest request)
        {
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