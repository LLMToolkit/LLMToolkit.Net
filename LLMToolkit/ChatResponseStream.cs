namespace LLMToolkit;

public class ChatResponseStream
{
    public string Model { get; set; } = null!;

    public string CreatedAt { get; set; } = null!;

    public IChatMessage Message { get; set; } = null!;

    public bool Done { get; set; }
}