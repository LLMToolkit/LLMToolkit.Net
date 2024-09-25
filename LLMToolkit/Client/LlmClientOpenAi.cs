using LLMToolkit.Chat;

namespace LLMToolkit.Client;

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

    //public override Task<string> Chat(string input, IResponseStreamer<ChatResponseStream?> streamer, CancellationToken cancellationToken = default)
    //{
    //    throw new NotImplementedException();
    //}

    public override Task<string> Chat(ChatMessageThread dialog, string toolDefinition, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}