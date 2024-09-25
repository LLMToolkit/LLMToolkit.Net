namespace LLMToolkit.Chat;

public class ChatMessage : IChatMessage
{
    public string Content { get; }
    public ChatMessageRole MessageRole { get; }
    public ChatMessage(ChatMessageRole messageRole, string content)
    {
        Content = content;
        MessageRole = messageRole;
    }
}

