using System.Text.Json.Nodes;

namespace MathHelperr.Service.Groq;

public class GroqRequest
{
    public JsonObject GetRequestData(string exerciseText)
    {
        var equation = exerciseText
            .Replace("{", "")
            .Replace("}", "");
        
        return new JsonObject
        {
            ["model"] = "mixtral-8x7b-32768",
            ["temperature"] = 0.7,
            ["max_tokens"] = 150,
            ["messages"] = new JsonArray
            {
                new JsonObject
                {
                    ["role"] = "system",
                    ["content"] = "You are a helpful third grade hungarian math teacher."
                },
                new JsonObject
                {
                    ["role"] = "user",
                    ["content"] = $"Here is a mathematical equation: {equation}. Based on this equation, create a word problem that a third-grade student can solve. Make sure the problem is fun and easy to understand, and please send me it in hungarian.   "
                }
            }
        };
        
    }
}