using System.Text;
using OllamaSharp;
using OllamaSharp.Models;

namespace LLMToolkit;


public class OllamaLlmClient : ILlmClient
{
    OllamaApiClient _ollama ;

    public OllamaLlmClient()
    {
        // set up the client
        var uri = new Uri("http://localhost:11434");
        _ollama = new OllamaApiClient(uri);
        
    }

    public async Task<List<double[]>> GetEmbedding(ModelConfig config, string input, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetCompletion(ModelConfig config,  string input, CancellationToken cancellationToken = default)
    {
        RequestOptions reqOptions = new RequestOptions();
        reqOptions.Temperature = config.Temperature;
        reqOptions.TopP = config.TopP;
        reqOptions.NumPredict = config.NumPredict;


        GenerateRequest request = new GenerateRequest();
        request.Prompt = input;
        request.Model = config.ModelName;
        request.Options = reqOptions;
        

        StringBuilder response = new StringBuilder();
        await foreach (var stream in _ollama.Generate(request))
            response.Append(stream.Response); 
        return response.ToString();
    }

    public async Task<string> GetCompletion(ModelConfig config, string input, IResponseStreamer<ChatResponseStream?> streamer, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}