using LLMToolkit;

namespace LLMToolkit.Tests;

public class LlmClientOllamaTest
{
    
    [Fact]
    public async Task GetCompletion_ReturnsExpectedResult()
    {
        string uri = "http://localhost:11434";
        LlmClient client = LlmClientFactory.CreateLlmClient(LlmProvider.Ollama, uri, "apiKey");
        client.Config.ModelName = "llama3.1";
        client.Config.Temperature = 0.0f;

        // Arrange
        var input = "Hello ollama";
        var cancellationToken = default(CancellationToken);
       

        // Act
        var result = await client.GetCompletion( input, cancellationToken);

        // non empty
        Assert.NotEmpty(result);

        // Assert that the result contains either "hello" or "nice to meet you"
        Assert.True(
               result.Contains("hello",StringComparison.InvariantCultureIgnoreCase) 
            || result.Contains("nice to meet you",StringComparison.InvariantCultureIgnoreCase)
        );
    }
      
    
}