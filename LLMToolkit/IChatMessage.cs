namespace LLMToolkit;

public interface IChatMessage
{
    ChatRole Role { get; }
    string Content { get; }
}