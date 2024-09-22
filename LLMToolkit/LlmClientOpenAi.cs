namespace LLMToolkit;

public class LlmClientOpenAi : LlmClient, ILlmClient
{

    public LlmClientOpenAi(string uri, string apikey)
    {
        
    }

    public override Task<List<double[]>> GetEmbedding(List<string> input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override Task<string> GetCompletion(string input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    //public override Task<string> GetCompletion(string input, IResponseStreamer<ChatResponseStream?> streamer, CancellationToken cancellationToken = default)
    //{
    //    throw new NotImplementedException();
    //}

    public override Task<string> GetCompletion(ChatMessageThread dialog, string toolDefinition, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}