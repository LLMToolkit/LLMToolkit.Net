using System.Text.RegularExpressions;

namespace LLMToolkit.Chunking;

public class TextChunkerCharacter : TextChunker
{
    private string _separator;
    private bool _isSeparatorRegex;

    public TextChunkerCharacter(
        int chunkSize,
        int chunkOverlap,
        string separator = "\n\n",
        Func<string, int> lengthFunction = null,
        bool keepSeparator = false,
        bool addStartIndex = false,
        bool isSeparatorRegex = false,
        bool stripWhitespace = true)
        : base(chunkSize, chunkOverlap, lengthFunction, keepSeparator, addStartIndex, stripWhitespace)
    {
        _separator = separator;
        _isSeparatorRegex = isSeparatorRegex;
    }


    public override List<string> ChunkText()
    {
        string separator = _isSeparatorRegex ? _separator : Regex.Escape(_separator);
        var splits = SplitTextWithRegex(_text, separator, _keepSeparator);
        string separatorToUse = _keepSeparator ? "" : _separator;
        return MergeSplits(splits, separatorToUse);
    }

    private List<string> SplitTextWithRegex(string text, string separator, bool keepSeparator)
    {
        List<string> splits = new List<string>();
        if (!string.IsNullOrEmpty(separator))
        {
            if (keepSeparator)
            {
                var splitArray = Regex.Split(text, $"({separator})");
                for (int i = 0; i < splitArray.Length - 1; i += 2)
                {
                    splits.Add(splitArray[i] + splitArray[i + 1]);
                }
                if (splitArray.Length % 2 == 0)
                {
                    splits.Add(splitArray.Last());
                }
            }
            else
            {
                splits.AddRange(Regex.Split(text, separator));
            }
        }
        else
        {
            splits.AddRange(text.Select(c => c.ToString()));
        }
        return splits.Where(s => !string.IsNullOrEmpty(s)).ToList();
    }
}