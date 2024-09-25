using LLMToolkit.Client;

namespace LLMToolkit.Client;

public class LlmClientFactory
{
    public static LlmClient CreateLlmClient(LlmProvider provider, string uri, string apikey)
    {
        return provider switch
        {
            LlmProvider.OpenAI => new LlmClientOpenAi(uri, apikey),
            LlmProvider.Ollama => new LlmClientOllama(uri, apikey),
            _ => throw new NotImplementedException()
        };
    }
}