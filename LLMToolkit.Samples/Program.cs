using LLMToolkit;


namespace LLMToolkit.Samples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await Test1();
            await Test2();
        }

        private static async Task Test1()
        {
            // create client
            string uri = "http://localhost:11434";
            LlmClient client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama, uri, "apiKey");
            client.Config.ModelName = "llama3.1";
            client.Config.Temperature = 0.0f;


            Console.WriteLine("Getting completion from Ollama\n");
            Console.Write("Ask to Ollama >");
            var input = Console.ReadLine();
            

            // get completion from Large Language Model
            string response = await client.GetCompletion(input);
            

            Console.WriteLine(response);
            Console.WriteLine("\nPress any key to exit ...");
            Console.ReadKey();
        }



        private static async Task Test2()
        {
            // create client
            string uri = "http://localhost:11434";
            LlmClient client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama, uri, "apiKey");
            client.Config.ModelName = "llama3.1";
            client.Config.Temperature = 0.0f;
            

            var dialog = new ChatMessageThread();

            Console.WriteLine(" - - Ask to Ollama - - ");
            while (true)
            {
                Console.Write("\nUser >>>");
                var input = Console.ReadLine();
                dialog.AddHumanMessage(input);

                string resp = await client.GetCompletion(dialog);
                dialog.AddAssistantMessage(resp);
                
                // imprime la respuesta
                Console.WriteLine("\nOllama >>> " + resp);
            }

        }

    }
}
