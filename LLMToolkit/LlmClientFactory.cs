namespace LLMToolkit;

public class LlmClientFactory
{
    public static ILlmClient CreateLlmClient(LlmProvider provider)
    {
        return provider switch
        {
            LlmProvider.OpenAI => new OpenAiLlmClient(),
            LlmProvider.Ollama => new OllamaLlmClient(),
            _ => throw new NotImplementedException()
        };
    }
}