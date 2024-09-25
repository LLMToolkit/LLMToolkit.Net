using System.Dynamic;
using System.Reflection;
using LLMToolkit;
using OllamaSharp;
using static System.Net.Mime.MediaTypeNames;


namespace LLMToolkit.Samples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            //await SamplesEmbeddings.GetEmbeddingsSimpleOllama();
            //await SamplesEmbeddings.GetEmbeddingsListOllama();

            //await SamplesCompletion.GetCompletionOllama();

            //await SamplesChat.ChatSimpleOllama();


            await SamplesTools.TestChatCalling();

        }


    }
}
