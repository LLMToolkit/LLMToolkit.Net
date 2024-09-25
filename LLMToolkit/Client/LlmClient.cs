using LLMToolkit.Chat;

namespace LLMToolkit.Client;

public abstract class LlmClient : ILlmClient
{

    public ModelConfig Config { get; }

    public LlmClient()
    {
        Config = new ModelConfig();
    }

    public abstract Task<List<double[]>> GetEmbedding(List<string> input, CancellationToken cancellationToken = default);

    public Task<double[]> GetEmbedding(string input, CancellationToken cancellationToken = default)
    {
        //List<string> param = new List<string>(){input};
        var embeddingsTask = GetEmbedding([input], cancellationToken);
        return embeddingsTask.ContinueWith(task => task.Result[0], cancellationToken);
    }



    public abstract Task<string> GetCompletion(string input, CancellationToken cancellationToken = default);

    //public abstract Task<string> Chat(string input, IResponseStreamer<ChatResponseStream?> streamer,CancellationToken cancellationToken = default);

    public abstract Task<string> Chat(ChatMessageThread dialog, string toolDefinition = "", CancellationToken cancellationToken = default);
}