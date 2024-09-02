using LLMToolkit;

namespace LLMToolkit.Tests;

public class OllamaLlmClientTest
{

    [Fact]
    public async Task GetCompletion_ReturnsExpectedResult()
    {
        // Arrange
        var config = new ModelConfig();
        config.ModelName = "llama3.1";
        var input = "Hello ollama";
        var cancellationToken = default(CancellationToken);
        var client = new OllamaLlmClient();

        // Act
        var result = await client.GetCompletion(config, input, cancellationToken);

        // non empty
        Assert.NotEmpty(result);

        // Assert that the result contains either "hello" or "nice to meet you"
        Assert.True(
            result.Contains("hello",StringComparison.InvariantCultureIgnoreCase) 
            || result.Contains("nice to meet you",StringComparison.InvariantCultureIgnoreCase)
        );

        

        




        
    }
}