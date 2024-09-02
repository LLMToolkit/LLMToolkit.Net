namespace LLMToolkit;

public interface IChatMessage
{
    string Content { get; }
    ChatRole Role { get; }
}
public class ChatMessage : IChatMessage
{
    public string Content { get; }
    public ChatRole Role { get; }
    public ChatMessage(string content, ChatRole role)
    {
        Content = content;
        Role = role;
    }
}

