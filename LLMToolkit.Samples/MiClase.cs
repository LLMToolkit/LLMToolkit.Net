using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LLMToolkit;


namespace LLMToolkit.Samples;


public class MiClase
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


    [LlmTools(Name = "GetCurrentUtcTime", Description = "Obtiene la hora actual en UTC")]
    public static string GetCurrentUtcTime()
    {
        return DateTime.UtcNow.ToString("R");
    }
}