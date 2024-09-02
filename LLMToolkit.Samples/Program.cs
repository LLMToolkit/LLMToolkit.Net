using LLMToolkit;


namespace LLMToolkit.Samples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama);
            
            ModelConfig config = new ModelConfig();
            config.ModelName = "llama3.1";

            config.Temperature =  0.0f ;

            //config.NumPredict = 20;

            Console .WriteLine("Getting completion from Ollama\n");
            Console.Write("Ask to Ollama >");
            var input = Console.ReadLine();
            string response = await client.GetCompletion(config, input);
            Console.WriteLine(response);
            Console.WriteLine("\nPress any key to exit ...");
            Console.ReadKey();
        }
    }
}
