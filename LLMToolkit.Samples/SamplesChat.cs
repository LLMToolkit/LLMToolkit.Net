using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLMToolkit.Chat;
using LLMToolkit.Client;

namespace LLMToolkit.Samples;

public static class SamplesChat
{

    public static async Task ChatSimpleOllama()
    {
        // create client
        string uri = "http://localhost:11434";
        LlmClient client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama, uri, "apiKey");
        client.Config.ModelName = "llama3.1";
        client.Config.Temperature = 0.1f;

        var dialog = new ChatMessageThread();
        Console.WriteLine(" - - Chat with Ollama - - ");
        while (true)
        {
            Console.Write("\nUser >>>");
            var input = Console.ReadLine();
            dialog.AddUserMessage(input);
            string resp = await client.Chat(dialog, "");
            dialog.AddAssistantMessage(resp);
            Console.WriteLine("\nOllama >>> " + resp);
        }
    }
}