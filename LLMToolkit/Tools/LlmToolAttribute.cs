namespace LLMToolkit.Tools;

public sealed class LlmToolAttribute : Attribute
{
    public string Name { get; set; }
    public string Description { get; set; }
}