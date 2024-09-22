using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using LLMToolkit;

namespace LLMToolkit.Tests;




public class TestClass
{
    [LlmTools(Name = "TestGetValue", Description = "Convierte el numero a string")]
    public static string TestGetValue(
        [Description("Identifier to convert"), Required]
        int id
    )
    {
        return "Foo";
    }
}

[TestSubject(typeof(LlmToolsProxy))]
public class LlmToolsProxyTest
{
    [Fact]
    public void LlmToolsProxy_ShouldBeEmptyBeforeScan()
    {
        // Arrange
        var proxy = new LlmToolsProxy();

        // Assert
        Assert.Empty(proxy.GetFuncToolDefinitions());
    }


    [Fact]
    public void LlmToolsProxy_ShouldScanFuncTools()
    {
        // Arrange
        var proxy = new LlmToolsProxy();
        var assembly = typeof(TestClass).Assembly;

        // Act
        proxy.ScanFuncTools(assembly);

        // Assert
        var expectedDefinition = "[{\r\n   \"type\": \"function\",\r\n   \"function\": {\r\n  \"Name\": \"TestGetValue\",\r\n  \"description\": \"Convierte el numero a string\",\r\n  \"parameters\": {\r\n    \"type\": \"object\",\r\n    \"properties\": {\r\n      \"id\": {\r\n        \"Type\": \"Int32\",\r\n        \"Description\": \"Identifier to convert\"\r\n      }\r\n    },\r\n    \"required\": [\r\n      \"id\"\r\n    ]\r\n  }\r\n}\r\n}]";
        var funcToolDefinitions = proxy.GetFuncToolDefinitions();
        Assert.Equal(expectedDefinition, funcToolDefinitions);
    }
}
