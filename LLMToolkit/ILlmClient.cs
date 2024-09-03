namespace LLMToolkit;

public interface ILlmClient
{

    // GetEmbedding Method
    public Task<List<double[]>> GetEmbedding(string input, CancellationToken cancellationToken = default);

    // GetCompletion
    public Task<string> GetCompletion(string input, CancellationToken cancellationToken = default);

    public Task<string> GetCompletion(string input, IResponseStreamer<ChatResponseStream?> streamer, CancellationToken cancellationToken = default);


    public Task<string> GetCompletion(ChatMessageThread dialog, CancellationToken cancellationToken = default);


}