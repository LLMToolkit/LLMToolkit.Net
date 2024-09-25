namespace LLMToolkit.Chat;

public interface IChatMessage
{
    ChatMessageRole MessageRole { get; }
    string Content { get; }
}