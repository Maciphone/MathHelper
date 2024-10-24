namespace MathHelperr.Service.Groq;

public interface IGroqResultGenerator
{
    Task<string?> GetAiText(string question);
}