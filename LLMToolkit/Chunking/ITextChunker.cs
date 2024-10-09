using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLMToolkit.Chunking;

public interface ITextChunker
{
    void SetText(string text);
    List<string> ChunkText();
    List<string> GetChunks();
    void ClearChunks();
}
