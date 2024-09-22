using OllamaSharp.Models;
using System.Text.Json.Serialization;

// See
// https://github.com/ggerganov/llama.cpp/blob/master/examples/main/README.md


namespace LLMToolkit;

public class ModelConfig
{
    // Model Name prop, defautl value is ""
    public string ModelName { get; set; } = "";

    // The temperature of the model.Increasing the temperature will make the model answer more creatively.
    // (Default: 0.7)
    public float Temperature { get; set; } = 0.7f;

    //Enable Mirostat sampling for controlling perplexity.
    //(default: 0, 0 = disabled, 1 = Mirostat, 2 = Mirostat 2.0)
    public int? MiroStat { get; set; } = 0;

    // Influences how quickly the algorithm responds to feedback from the generated text.
    // A lower learning rate will result in slower adjustments, while a higher learning
    // rate will make the algorithm more responsive. (Default: 0.1)
    public float? MiroStatEta { get; set; } = 0.1f;

    // Controls the balance between coherence and diversity of the output. A lower value will result in more
    // focused and coherent text. (Default: 5.0)
    public float? MiroStatTau { get; set; }= 5.0f;

    //Sets the size of the context window used to generate the next token. (Default: 2048)
    public int? NumCtx { get; set; } = 2048;

    //The number of GQA groups in the transformer layer. Required for some models,
    //for example it is 8 for llama2:70b
    public int? NumGqa { get; set; }
    public int? NumGpu { get; set; }

    //Sets the number of threads to use during computation. By default, Ollama will detect this for optimal performance.
    //It is recommended to set this value to the number of physical CPU cores your system has
    //(as opposed to the logical number of cores).
    public int? NumThread { get; set; }

    //Sets how far back for the model to look back to prevent repetition. (Default: 64, 0 = disabled, -1 = num_ctx)
    public int? RepeatLastN { get; set; } = 64;

    //Sets how strongly to penalize repetitions. A higher value (e.g., 1.5) will penalize repetitions more strongly,
    //while a lower value (e.g., 0.9) will be more lenient. (Default: 1.1)
    public float? RepeatPenalty { get; set; } = 1.1f;

    //Sets the random number seed to use for generation. Setting this to a specific number will make the model
    //generate the same text for the same prompt. (Default: 0)
    public int? Seed { get; set; } = 0;
    public string[]? Stop { get; set; }

    //Tail free sampling is used to reduce the impact of less probable tokens from the output.
    //A higher value (e.g., 2.0) will reduce the impact more, while a value of 1.0 disables this setting.
    //(default: 1)
    public float? TfsZ { get; set; }
    public int? NumPredict { get; set; }
    
    //Reduces the probability of generating nonsense. A higher value (e.g. 100) will give more diverse answers,
    //while a lower value (e.g. 10) will be more conservative. (Default: 40)
    public int? TopK { get; set; } = 40;

    //Works together with top-k. A higher value (e.g., 0.95) will lead to more diverse text, while a lower value
    //(e.g., 0.5) will generate more focused and conservative text. (Default: 0.9)
    public float? TopP { get; set; } = 0.9f;

    //Sets a minimum base probability threshold for token selection (default: 0.1).
    public float? MinP { get; set; } = 0.1f;

}