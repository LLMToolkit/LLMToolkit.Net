using LLMToolkit.Chat;

namespace LLMToolkit.Client;

public interface ILlmClient
{

    // GetEmbedding Method
    public Task<List<double[]>> GetEmbedding(List<string> input, CancellationToken cancellationToken = default);

    // Chat
    public Task<string> GetCompletion(string input, CancellationToken cancellationToken = default);


    public Task<string> Chat(ChatMessageThread dialog, string toolDefinition, CancellationToken cancellationToken = default);


}