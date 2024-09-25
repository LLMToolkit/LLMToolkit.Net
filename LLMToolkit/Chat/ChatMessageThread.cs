namespace LLMToolkit.Chat;

public class ChatMessageThread
{
    // ChatMessage list property
    public List<ChatMessage> Messages { get; }

    // Constructor
    public ChatMessageThread()
    {
        Messages = new List<ChatMessage>();
    }

    // Add system message
    public void AddSystemMessage(string message)
    {
        Messages.Add(new ChatMessage(ChatMessageRole.System, message));
    }


    // Add user message
    public void AddUserMessage(string message)
    {
        Messages.Add(new ChatMessage(ChatMessageRole.User, message));
    }

    //Add assistant message
    public void AddAssistantMessage(string message)
    {
        Messages.Add(new ChatMessage(ChatMessageRole.Assistant, message));
    }


    //add tool message
    public void AddToolMessage(string message)
    {
        Messages.Add(new ChatMessage(ChatMessageRole.Tool, message));
    }
}