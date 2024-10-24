namespace MathHelperr.Service.Groq;

public class GroqResultGenerator : IGroqResultGenerator
{
    private readonly GroqApiClient _groqApiClient;
    private readonly GroqRequest _groqRequest;
    private readonly GroqTextGenerator _groqTextGenerator;

    public GroqResultGenerator(GroqApiClient groqApiClient, GroqRequest groqRequest, GroqTextGenerator groqTextGenerator)
    {
        _groqApiClient = groqApiClient;
        _groqRequest = groqRequest;
        _groqTextGenerator = groqTextGenerator;
    }

    public async Task<string> GetAiText(string question)
    {
        try { 
            var request = _groqRequest.GetRequestData(question); 
            var aiResponse = await _groqApiClient.CreateChatCompletionAsync(request); 
            var aiStringResponse = aiResponse?["choices"]?[0]?["message"]?["content"]?.ToString();

        return aiStringResponse;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}