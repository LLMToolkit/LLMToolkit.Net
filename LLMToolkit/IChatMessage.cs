namespace LLMToolkit;

public interface IChatMessage
{
    ChatMessageRole MessageRole { get; }
    string Content { get; }
}