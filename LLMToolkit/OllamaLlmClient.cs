using System.IO;
using System.Text;
using OllamaSharp;
using OllamaSharp.Models;
using OllamaSharp.Models.Chat;

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
        // Create request options for Ollama
        RequestOptions reqOptions = new RequestOptions();
        reqOptions.Temperature = config.Temperature;
        reqOptions.TopP = config.TopP;
        reqOptions.NumPredict = config.NumPredict;
        // TODO : Add more options here

        // create request for Ollama
        GenerateRequest request = new GenerateRequest();
        request.Prompt = input;
        request.Model = config.ModelName;
        request.Options = reqOptions;
        
        // get completion from Ollama Large Language Model
        StringBuilder response = new StringBuilder();
        await foreach (var stream in _ollama.Generate(request))
            response.Append(stream.Response); 

        return response.ToString();
    }

    public async Task<string> GetCompletion(ModelConfig config, string input, IResponseStreamer<ChatResponseStream?> streamer, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }



    public async Task<string> GetCompletion(ModelConfig config, MessageThread dialog, CancellationToken cancellationToken = default)
    {
        // Create request options for Ollama
        RequestOptions reqOptions = new RequestOptions();
        reqOptions.Temperature = config.Temperature;
        reqOptions.TopP = config.TopP;
        reqOptions.NumPredict = config.NumPredict;
        // TODO : Add more options here

        // create request for Ollama
        ChatRequest request = new ChatRequest();
        request.Model = config.ModelName;
        request.Options = reqOptions;

        request.Messages = ConvertDialog(dialog);
        
        // get completion from Ollama Large Language Model
        StringBuilder response = new StringBuilder();
        await foreach (var stream in _ollama.Chat(request))
            response.Append(stream.Message.Content); 

        return response.ToString();
    }


    //ConvertMessage IEnumerable<ChatMessage> a IEnumerable<OllamaSharp.Models.Chat.Message>
    private IEnumerable<OllamaSharp.Models.Chat.Message> ConvertDialog(MessageThread dialog)
    {
        List<OllamaSharp.Models.Chat.Message> result = new List<OllamaSharp.Models.Chat.Message>();
        foreach (var message in dialog.Messages)
        {
            result.Add(ConvertMessage(message));
        }
        return result;
    }


    private OllamaSharp.Models.Chat.Message ConvertMessage (IChatMessage message)
    {
        ;
        switch (message.Role)
        {
            case ChatRole.User:
                return new OllamaSharp.Models.Chat.Message(OllamaSharp.Models.Chat.ChatRole.User, message.Content);
            case ChatRole.Assistant:
                return new OllamaSharp.Models.Chat.Message(OllamaSharp.Models.Chat.ChatRole.Assistant, message.Content);
            default:
                return new OllamaSharp.Models.Chat.Message(OllamaSharp.Models.Chat.ChatRole.User, message.Content);
        }
    }

}