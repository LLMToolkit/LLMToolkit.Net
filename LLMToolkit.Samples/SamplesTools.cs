using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LLMToolkit.Samples;

public static class SamplesTools
{
    private static async Task TestChatCalling()
    {

        LlmToolsProxy llm = new LlmToolsProxy();
        llm.ScanFuncTools(Assembly.GetExecutingAssembly());
        string funcdef = llm.GetFuncToolDefinitions();


        // create client
        string uri = "http://localhost:11434";
        LlmClient client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama, uri, "apiKey");
        client.Config.ModelName = "llama3.1";
        client.Config.Temperature = 0.0f;


        var dialog = new ChatMessageThread();
        dialog.AddSystemMessage("Usa las tools solo si son necesarias para obtener un dato. " +
                                "Sino ignora la tool y continua respondiendo el chat con el usuario. " +
                                "Sigue la conversacion sin hacer aclaraciones sobre las tools. ");

        Console.WriteLine(" - - Ask to Ollama - - ");
        while (true)
        {
            Console.Write("\nUser >>>");
            var input = Console.ReadLine();
            dialog.AddUserMessage(input);

            string resp = await client.GetCompletion(dialog, funcdef);
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
        Console.WriteLine("Press any Key ...");
        Console.ReadKey();
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