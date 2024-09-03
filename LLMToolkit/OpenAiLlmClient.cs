namespace LLMToolkit;

public class OpenAiLlmClient : LlmClient, ILlmClient
{

    public OpenAiLlmClient(string uri, string apikey)
    {
        
    }

    public override Task<List<double[]>> GetEmbedding(string input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<string> GetCompletion(string input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<string> GetCompletion(string input, IResponseStreamer<ChatResponseStream?> streamer, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<string> GetCompletion(ChatMessageThread dialog, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}