using System.Text.Json.Nodes;

namespace MathHelperr.Service.Groq;

public class GroqRequest
{
    
    // https://console.groq.com/docs/models - all ai modells
    public JsonObject GetRequestData(string exerciseText)
    {
        Console.WriteLine(exerciseText);
        var equation = exerciseText
            .Replace("{", "")
            .Replace("}", "");
        //["model"] = "mixtral-8x7b-32768",
        return new JsonObject
        {
            ["model"] = "llama3-70b-8192",
            ["temperature"] = 1,
            ["max_tokens"] = 1024,
            ["messages"] = new JsonArray
            {
                // new JsonObject
                // {
                //     ["role"] = "system",
                //     ["content"] = "You are a helpful third grade hungarian math teacher."
                // },
                new JsonObject
                {
                    ["role"] = "system",
                    ["content"] = $"You are a helpful third-grade Hungarian math teacher. Here is a mathematical " +
                                  $"equation: {equation}. Based on this equation, create a word problem in Hungarian " +
                                  $"that a third-grade student can solve,also give an explanation how to solve the math problem" +
                                  $". Please provide  the word problem, and explanation in " +
                                  $"Hungarian, with no translation or additional explanation." 
                }
            }
        };
        
    }
}