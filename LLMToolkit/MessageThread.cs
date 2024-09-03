namespace LLMToolkit;

public class MessageThread
{
    // Lista de mensajes
    public List<ChatMessage> Messages { get; }

    // Constructor
    public MessageThread()
    {
        Messages = new List<ChatMessage>();
    }

    // Add user message
    public void AddUserMessage(string message)
    {
        Messages.Add(new ChatMessage(ChatRole.User, message));
    }

    //Add assistant message
    public void AddAssistantMessage(string message)
    {
        Messages.Add(new ChatMessage(ChatRole.Assistant, message));
    }


}