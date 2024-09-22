namespace LLMToolkit;

public sealed class LlmToolsAttribute : Attribute
{
    public string Name { get; set; }
    public string Description { get; set; }
}