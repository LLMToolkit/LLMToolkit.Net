using System.Text.Json.Serialization;

namespace LLMToolkit;

public class ModelConfig
{
    // Model Name prop, defautl value is ""
    public string ModelName { get; set; } = "";
    public float Temperature { get; set; } = 0.2f;
    public int? MiroStat { get; set; }
    public float? MiroStatEta { get; set; }
    public float? MiroStatTau { get; set; }
    public int? NumCtx { get; set; }
    public int? NumGqa { get; set; }
    public int? NumGpu { get; set; }
    public int? NumThread { get; set; }
    public int? RepeatLastN { get; set; }
    public float? RepeatPenalty { get; set; }
    public int? Seed { get; set; }
    public string[]? Stop { get; set; }
    public float? TfsZ { get; set; }
    public int? NumPredict { get; set; }
    public int? TopK { get; set; }
    public float? TopP { get; set; }
    public float? MinP { get; set; }
}