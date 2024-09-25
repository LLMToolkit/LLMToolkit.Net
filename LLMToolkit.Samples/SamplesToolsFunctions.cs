using LLMToolkit.Tools;

namespace LLMToolkit.Samples;


public static class SamplesToolsFunctions
{
    //[LlmTools(Name = "GetValue", Description = "Convierte el numero a string")]
    //public static string GetValue(
        
    //    [Description("Identifier to convert"), Required]
    //    int Id,

    //    [Description("Description for Id")]
    //    string Description)
    //{
    //    Console.WriteLine($"id: {Id} Description {Description} ");
    //    return Id.ToString();
    //}


    [LlmTool(Name = "GetCurrentUtcTime", Description = "This function retrieves the current Coordinated Universal Time (UTC).")]
    public static string GetCurrentUtcTime()
    {
        return DateTime.UtcNow.ToString("R");
    }
}