namespace LLMToolkit;

public class ChatMessage : IChatMessage
{
    public string Content { get; }
    public ChatRole Role { get; }
    public ChatMessage( ChatRole role, string content)
    {
        Content = content;
        Role = role;
    }
}

