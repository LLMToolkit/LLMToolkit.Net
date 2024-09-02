using System.Text.Json.Serialization;

namespace LLMToolkit;

public interface ILlmClient
{

      // GetEmbedding Method
      public Task<List<double[]>> GetEmbedding(ModelConfig config, string input, CancellationToken cancellationToken = default) ;

      // GetCompletion
      public Task<string> GetCompletion(ModelConfig config,  string input, CancellationToken cancellationToken = default);

      public Task<string> GetCompletion(ModelConfig config, string input, IResponseStreamer<ChatResponseStream?> streamer, CancellationToken cancellationToken = default);


}