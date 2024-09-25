using System.IO;
using System.Text;
using LLMToolkit.Chat;
using Newtonsoft.Json;
using OllamaSharp;
using OllamaSharp.Models;
using OllamaSharp.Models.Chat;

namespace LLMToolkit.Client;

public class LlmClientOllama : LlmClient
{
    OllamaApiClient _ollama;

    public LlmClientOllama(string uri, string apikey)
    {
        _ollama = new OllamaApiClient(uri);
    }

    public override async Task<List<double[]>> GetEmbedding(List<string> input, CancellationToken cancellationToken = default)
    {
        var embedReq = CreateEmbedRequest(input);
        var embResp = await _ollama.Embed(embedReq);
        return embResp.Embeddings;
    }

    public override async Task<string> GetCompletion(string input, CancellationToken cancellationToken = default)
    {
        StringBuilder response = new StringBuilder();

        var request = CreateCompletionRequest(input);
        await foreach (var stream in _ollama.Generate(request))
            response.Append(stream.Response);

        return response.ToString();
    }


    public override async Task<string> Chat(ChatMessageThread dialog, string toolDefinition = "", CancellationToken cancellationToken = default)
    {
        var request = CreateChatRequest(dialog, toolDefinition);
        StringBuilder response = new StringBuilder();
        var resp = _ollama.Chat(request);
        await foreach (var stream in resp)
        {
            response.Append(stream.Message.Content);
        }
        return response.ToString();
    }


    #region Private Section Methods

    private EmbedRequest CreateEmbedRequest(List<string> input)
    {
        RequestOptions reqOptions = CreateRequestOptions();
        var embedReq = new EmbedRequest();
        embedReq.Model = Config.ModelName;
        embedReq.Options = reqOptions;
        embedReq.Input = input;
        return embedReq;
    }

    private GenerateRequest CreateCompletionRequest(string input)
    {
        RequestOptions reqOptions = CreateRequestOptions();
        GenerateRequest request = new GenerateRequest();
        request.Prompt = input;
        request.Model = Config.ModelName;
        request.Options = reqOptions;
        return request;
    }


    private ChatRequest CreateChatRequest(ChatMessageThread dialog, string toolDefinition)
    {
        RequestOptions reqOptions = CreateRequestOptions();
        // create request for Ollama
        ChatRequest request = new ChatRequest();
        request.Model = Config.ModelName;
        request.Options = reqOptions;
        request.Messages = ConvertDialogToOllamaMessages(dialog);

        if (toolDefinition != "")
        {
            request.Tools = JsonConvert.DeserializeObject<List<Tool>>(toolDefinition);
        }
        return request;
    }

    private RequestOptions CreateRequestOptions()
    {
        RequestOptions reqOptions;
        reqOptions = new RequestOptions();
        reqOptions.Temperature = Config.Temperature;
        reqOptions.NumPredict = Config.NumPredict;
        reqOptions.NumCtx = Config.NumCtx;
        reqOptions.TopK = Config.TopK;
        reqOptions.TopP = Config.TopP;

        // TODO : Add more options here
        return reqOptions;
    }



    private IEnumerable<Message> ConvertDialogToOllamaMessages(ChatMessageThread dialog)
    {
        List<Message> result = new List<Message>();
        foreach (var message in dialog.Messages)
        {
            result.Add(ConvertMessage(message));
        }
        return result;
    }



    private Message ConvertMessage(IChatMessage message)
    {
        switch (message.MessageRole)
        {
            case ChatMessageRole.System:
                return new Message(ChatRole.System, message.Content);
            case ChatMessageRole.User:
                return new Message(ChatRole.User, message.Content);
            case ChatMessageRole.Assistant:
                return new Message(ChatRole.Assistant, message.Content);
            case ChatMessageRole.Tool:
                return new Message(ChatRole.Tool, message.Content);
            default:
                return new Message(ChatRole.User, message.Content);
        }
    }

    #endregion
}