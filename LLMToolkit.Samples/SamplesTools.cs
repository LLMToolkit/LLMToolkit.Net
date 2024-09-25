using System.Reflection;
using LLMToolkit.Chat;
using LLMToolkit.Client;
using LLMToolkit.Tools;

namespace LLMToolkit.Samples;

public static class SamplesTools
{
    public static async Task TestChatCalling()
    {
        // get the tools from current Assembly
        LlmToolsProxy llm = new LlmToolsProxy();
        llm.ScanFuncTools(Assembly.GetExecutingAssembly());
        string funcToolDefinitions = llm.GetFuncToolDefinitions();


        // create client
        string uri = "http://localhost:11434";
        LlmClient client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama, uri, "apiKey");
        client.Config.ModelName = "llama3.1-mariano";
        client.Config.Temperature = 0.0f;
        


        var dialog = new ChatMessageThread();

        dialog.AddSystemMessage("" +
                                "Use tools only if they are strictly necessary to obtain information to answer. " +
                                "You can only use the tools available. Do not invent new tools. If there is no tool that " +
                                "gives you the information you need, ignore the tools and continue answering the chat " +
                                "with the user without using tools and without making clarifications or comments about tools." +
                                "For example, avoid saying things like 'I cannot use tools to answer this question'.");
        Console.Clear();
        Console.WriteLine(" - - Ask to Ollama - - ");
        while (true)
        {
            Console.Write("\nUser >>>");
            var input = Console.ReadLine();
            dialog.AddUserMessage(input);

            string resp = await client.Chat(dialog, funcToolDefinitions);
            dialog.AddAssistantMessage(resp);

            // imprime la respuesta
            Console.WriteLine("\nOllama >>> " + resp);
        }

    }

    private static void TooCalligDefinitions()
    {
        Console.Clear();
        LlmToolsProxy llm = new LlmToolsProxy();
        llm.ScanFuncTools(Assembly.GetExecutingAssembly());
        Console.WriteLine(llm.GetFuncToolDefinitions());
    }


    private static void ToolCallingDemo()
    {

        Console.Clear();
        LlmToolsProxy llm = new LlmToolsProxy();
        llm.ScanFuncTools(Assembly.GetExecutingAssembly());
        string sampleParam = "{\n \"Id\": 5,\n \"Id3\": 7,\n \"Description\": \"Descripcion Id 5\" \n}";
        Console.WriteLine($"Parameter passed {sampleParam}");
        llm.Invoke("GetValue", sampleParam);
        Console.WriteLine("Press any Key ...");
        Console.ReadKey();
    }


}