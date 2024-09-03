using System.IO;
using System.Text;
using OllamaSharp;
using OllamaSharp.Models;
using OllamaSharp.Models.Chat;

namespace LLMToolkit;


public class OllamaLlmClient : LlmClient
{
    OllamaApiClient _ollama ;

    public OllamaLlmClient(string uri, string apikey)
    {
        _ollama = new OllamaApiClient(uri);
    }



    public override async Task<List<double[]>> GetEmbedding(String input, CancellationToken cancellationToken = default)
    {
        RequestOptions reqOptions = CreateRequestOptions();

        var embedReq = new EmbedRequest();
        embedReq.Model = Config.ModelName;
        embedReq.Options = reqOptions;
        embedReq.Input = new List<string>() { input };
        

        var embResp = await _ollama.Embed(embedReq);

        return embResp.Embeddings;
    }


    public override async Task<string> GetCompletion(string input, CancellationToken cancellationToken = default)
    {
        // Create request options for Ollama
        RequestOptions reqOptions = CreateRequestOptions();

        // create request for Ollama
        GenerateRequest request = new GenerateRequest();
        request.Prompt = input;
        request.Model = Config.ModelName;
        request.Options = reqOptions;
        
        // get completion from Ollama Large Language Model
        StringBuilder response = new StringBuilder();
        await foreach (var stream in _ollama.Generate(request))
            response.Append(stream.Response); 

        return response.ToString();
    }


    
    // Get completion for a dialog or chat
    public override async Task<string> GetCompletion( ChatMessageThread dialog, CancellationToken cancellationToken = default)
    {

        RequestOptions reqOptions = CreateRequestOptions();

        // create request for Ollama
        ChatRequest request = new ChatRequest();
        request.Model = Config.ModelName;
        request.Options = reqOptions;
        request.Messages = ConvertDialogToOllamaMessages(dialog);
        

        // get completion from Ollama Large Language Model chat version
        StringBuilder response = new StringBuilder();
        await foreach (var stream in _ollama.Chat(request))
            response.Append(stream.Message.Content); 

        return response.ToString();
    }






    public override async Task<string> GetCompletion(string input, IResponseStreamer<ChatResponseStream?> streamer, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }




    #region Private Section Methods

    private RequestOptions CreateRequestOptions()
    {
        RequestOptions reqOptions;
        reqOptions = new RequestOptions();
        reqOptions.Temperature = Config.Temperature;
        reqOptions.NumPredict = Config.NumPredict;
        // TODO : Add more options here
        return reqOptions;
    }



    private IEnumerable<OllamaSharp.Models.Chat.Message> ConvertDialogToOllamaMessages(ChatMessageThread dialog)
    {
        List<OllamaSharp.Models.Chat.Message> result = new List<OllamaSharp.Models.Chat.Message>();
        foreach (var message in dialog.Messages)
        {
            result.Add(ConvertMessage(message));
        }
        return result;
    }



    private OllamaSharp.Models.Chat.Message ConvertMessage(IChatMessage message)
    {
        switch (message.Role)
        {
            case ChatRole.System:
                return new OllamaSharp.Models.Chat.Message(OllamaSharp.Models.Chat.ChatRole.System, message.Content);
            case ChatRole.Human:
                return new OllamaSharp.Models.Chat.Message(OllamaSharp.Models.Chat.ChatRole.User, message.Content);
            case ChatRole.Assistant:
                return new OllamaSharp.Models.Chat.Message(OllamaSharp.Models.Chat.ChatRole.Assistant, message.Content);
            case ChatRole.Tool:
                return new OllamaSharp.Models.Chat.Message(OllamaSharp.Models.Chat.ChatRole.Tool, message.Content);
            default:
                return new OllamaSharp.Models.Chat.Message(OllamaSharp.Models.Chat.ChatRole.User, message.Content);
        }
    }

    #endregion
}