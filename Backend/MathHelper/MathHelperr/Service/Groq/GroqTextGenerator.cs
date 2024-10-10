using System.Text.Json;

namespace MathHelperr.Service.Groq;

public class GroqTextGenerator
{
    private readonly GroqApiClient _groqApiClient;
    private readonly GroqRequest _groqRequest;

    public GroqTextGenerator(GroqApiClient groqApiClient, GroqRequest groqRequest)
    {
        _groqApiClient = groqApiClient;
        _groqRequest = groqRequest;
    }

    public async Task<string> GetGeneratedText(IMathExcercise exercise)
    {
        try
        {
            Console.WriteLine(exercise.Question());
            var createJsonForRequest = _groqRequest.GetRequestData(exercise.Question());
            var aIResponse = await _groqApiClient.CreateChatCompletionAsync(createJsonForRequest);
            var generatedText = aIResponse?["choices"]?[0]?["message"]?["content"]?.ToString();
            return generatedText ?? string.Empty;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"API request failed: {e.Message}");
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Failed to parse API response: {e.Message}");
        }

        return string.Empty;
    }
}