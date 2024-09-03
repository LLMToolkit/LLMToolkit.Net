namespace LLMToolkit;

public class LlmClientFactory
{
    public static LlmClient CreateLlmClient(LlmProvider provider, string uri, string apikey)
    {
        return provider switch
        {
            LlmProvider.OpenAI => new OpenAiLlmClient(uri, apikey),
            LlmProvider.Ollama => new OllamaLlmClient(uri, apikey),
            _ => throw new NotImplementedException()
        };
    }
}