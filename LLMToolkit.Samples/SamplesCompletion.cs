namespace LLMToolkit.Samples;
public static class SamplesCompletion
{

    public static async Task GetCompletionOllama()
    {
        // create client
        string uri = "http://localhost:11434";
        LlmClient client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama, uri, "apiKey");
        client.Config.ModelName = "llama3.1";
        client.Config.Temperature = 0.5f;

        Console.WriteLine("Getting completion from Ollama\n");
        Console.Write("Ask to Ollama >");
        
        var input = Console.ReadLine();
        string response = await client.GetCompletion(input);
        Console.WriteLine(response);
    }


}

