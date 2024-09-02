namespace LLMToolkit;

public class OpenAiLlmClient : ILlmClient
{
    public Task<List<double[]>> GetEmbedding(ModelConfig config, string input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetCompletion(ModelConfig config, string input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetCompletion(ModelConfig config, string input, IResponseStreamer<ChatResponseStream?> streamer, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}