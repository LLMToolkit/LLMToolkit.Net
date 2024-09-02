namespace LLMToolkit;

public interface IResponseStreamer<in T>
{
    void Stream(T stream);
}