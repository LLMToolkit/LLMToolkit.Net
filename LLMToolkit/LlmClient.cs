namespace LLMToolkit;

public abstract class LlmClient : ILlmClient
{

    public ModelConfig Config { get; }

    public LlmClient()
    {
        Config = new ModelConfig();
    }

    public abstract Task<List<double[]>> GetEmbedding( string input, CancellationToken cancellationToken = default);
    public abstract Task<string> GetCompletion(string input, CancellationToken cancellationToken = default);

    public abstract Task<string> GetCompletion(string input, IResponseStreamer<ChatResponseStream?> streamer,
        CancellationToken cancellationToken = default);

    public abstract Task<string> GetCompletion(ChatMessageThread dialog, CancellationToken cancellationToken = default);
}