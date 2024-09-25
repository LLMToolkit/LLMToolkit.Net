using LLMToolkit.Client;

namespace LLMToolkit.Samples;

public static class SamplesEmbeddings
{
    public static async Task GetEmbeddingsSimpleOllama()
    {
        // create client
        string uri = "http://localhost:11434";
        LlmClient client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama, uri, "apiKey");
        client.Config.ModelName = "nomic-embed-text";
        client.Config.Temperature = 0.0f;

        Console.WriteLine(" - - Ollama Embeddings - - ");
        Console.WriteLine("<Quit> to exit");
        Console.WriteLine("---------------------------");
        while (true)
        {
            Console.Write("\n\nUser >>>");
            string textToEmbed = Console.ReadLine();
            if (textToEmbed.ToLower() == "quit")
            {
                break;
            }
            Console.WriteLine("Embeddings: ");
            double[] embeds = await client.GetEmbedding(textToEmbed);
            Console.WriteLine(string.Join(";", embeds) + "\n");
            Console.WriteLine("\n\nEmbeddings length: " + embeds.Length);
        }
    }


    public static async Task GetEmbeddingsListOllama()
    {
        // create client
        string uri = "http://localhost:11434";
        LlmClient client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama, uri, "apiKey");
        client.Config.ModelName = "nomic-embed-text";
        client.Config.Temperature = 0.0f;

        Console.WriteLine(" - - Ollama Embeddings - - ");
        Console.WriteLine("<Quit> to exit");
        Console.WriteLine("---------------------------");
        while (true)
        {
            Console.Write("\n\nUser >>>");
            List<string> input = new List<string>();
            string textToEmbed = Console.ReadLine();
            if (textToEmbed.ToLower() == "quit")
            {
                break;
            }
            input.Add(textToEmbed);

            List<double[]> embeds = await client.GetEmbedding(input);

            Console.WriteLine("Embeddings: ");
            foreach (var emb in embeds)
            {
                Console.WriteLine(string.Join(";", emb) + "\n");
            }
            Console.WriteLine("\n\nEmbeddings length: " + embeds[0].Length);
        }
    }




}


