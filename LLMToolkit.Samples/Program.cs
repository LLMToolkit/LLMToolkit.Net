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
            var client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama);
            
            // create model configuration
            ModelConfig config = new ModelConfig();
            config.ModelName = "llama3.1";
            config.Temperature =  0.0f ;
            //config.NumPredict = 20;

            Console .WriteLine("Getting completion from Ollama\n");
            Console.Write("Ask to Ollama >");
            var input = Console.ReadLine();
            
            // get completion from Large Language Model
            string response = await client.GetCompletion(config, input);
            
            Console.WriteLine(response);
            Console.WriteLine("\nPress any key to exit ...");
            Console.ReadKey();
        }



        private static async Task Test2()
        {
            // create client
            var client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama);

            // create model configuration
            ModelConfig config = new ModelConfig();
            config.ModelName = "llama3.1";
            config.Temperature = 0.0f;
            //config.NumPredict = 20;

            var dialog = new MessageThread();


            Console.WriteLine(" - - Ask to Ollama - - ");
            while (true)
            {
                Console.Write("User >>>");
                var input = Console.ReadLine();
                dialog.AddUserMessage(input);

                string resp = await client.GetCompletion(config, dialog);
                dialog.AddAssistantMessage(resp);
                
                // imprime la respuesta
                Console.WriteLine("Ollama >>> " + resp);
            }

        }

    }
}
