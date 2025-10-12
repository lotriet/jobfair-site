using System.Text.RegularExpressions;

namespace DotNetMicroDemo.Services
{
    public class ModerationResult
    {
        public bool Allowed { get; set; }
        public string Reason { get; set; } = string.Empty;
    }

    /// <summary>
    /// Lightweight content moderation to block malicious/jailbreak prompts and abusive requests
    /// before they reach the LLM (to preserve tokens).
    /// This is intentionally conservative and fast — it's not a substitute for a hosted
    /// content moderation API but reduces obvious abuse.
    /// </summary>
    public class ContentModerationService
    {
        // Simple banned keywords and patterns (case-insensitive)
        private static readonly string[] BannedKeywords = new[]
        {
            "ignore all previous", "ignore previous", "ignore instructions", "bypass", "jailbreak",
            "break out", "sudo", "root", "get me access", "exploit", "malware", "virus",
            "bomb", "harm", "attack", "ddos", "cheat code", "how to kill", "how to murder",
            "illegal", "illicit", "sql injection", "xss", "cross-site scripting",
            "steal", "phish", "credit card", "ssn", "social security",
        };

        // Quick regexes for suspicious patterns
        private static readonly Regex UrlRegex = new(@"https?://", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex CodeExecutionRegex = new(@"\b(exec|execute|run)\b.*\b(shell|bash|cmd|powershell|python)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex InstrRegex = new(@"(ignore (the )?instructions|disregard (the )?policy|you are now)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // Maximum allowed message length to avoid huge token usage
        private const int MaxMessageLength = 1200; // characters

        public ModerationResult Moderate(string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return new ModerationResult { Allowed = false, Reason = "Message is empty." };
            }

            var trimmed = message.Trim();

            // Length check
            if (trimmed.Length > MaxMessageLength)
            {
                return new ModerationResult { Allowed = false, Reason = "Message is too long and may consume excessive tokens." };
            }

            // URL check - often used to try to leak or fetch external content
            if (UrlRegex.IsMatch(trimmed))
            {
                return new ModerationResult { Allowed = false, Reason = "Messages containing URLs are disallowed." };
            }

            // Instruction/jailbreak patterns
            if (InstrRegex.IsMatch(trimmed))
            {
                return new ModerationResult { Allowed = false, Reason = "Potential jailbreak or instruction to ignore policies detected." };
            }

            // Code execution / shell requests
            if (CodeExecutionRegex.IsMatch(trimmed))
            {
                return new ModerationResult { Allowed = false, Reason = "Requests to execute code or run shells are not allowed." };
            }

            // Keyword scanning
            var lower = trimmed.ToLowerInvariant();
            foreach (var kw in BannedKeywords)
            {
                if (lower.Contains(kw))
                {
                    return new ModerationResult { Allowed = false, Reason = "Message contains disallowed content or keywords." };
                }
            }

            // If nothing matched, allow
            return new ModerationResult { Allowed = true };
        }
    }
}
