namespace LLMToolkit;

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
        Messages.Add(new ChatMessage(ChatRole.System, message));
    }


    // Add user message
    public void AddHumanMessage(string message)
    {
        Messages.Add(new ChatMessage(ChatRole.Human, message));
    }

    //Add assistant message
    public void AddAssistantMessage(string message)
    {
        Messages.Add(new ChatMessage(ChatRole.Assistant, message));
    }


    //add tool message
    public void AddToolMessage(string message)
    {
        Messages.Add(new ChatMessage(ChatRole.Tool, message));
    }
}