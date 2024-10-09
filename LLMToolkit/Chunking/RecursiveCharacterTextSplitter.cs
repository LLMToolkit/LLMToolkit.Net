using System.Text.RegularExpressions;
using LLMToolkit.Chunking;

public class TextChunkerRecursive : TextChunker
{
    private List<string> _separators;
    private bool _isSeparatorRegex;

    public TextChunkerRecursive(
        List<string> separators = null,
        bool keepSeparator = true,
        bool isSeparatorRegex = false,
        int chunkSize = 4000,
        int chunkOverlap = 200,
        Func<string, int> lengthFunction = null,
        bool addStartIndex = false,
        bool stripWhitespace = true)
        : base(chunkSize, chunkOverlap, lengthFunction, keepSeparator, addStartIndex, stripWhitespace)
    {
        _separators = separators ?? new List<string> { "\n\n", "\n", " ", "" };
        _isSeparatorRegex = isSeparatorRegex;
    }

    public override List<string> ChunkText()
    {
        return SplitTextRecursive(_text, _separators);
    }

    private List<string> SplitTextRecursive(string text, List<string> separators)
    {
        List<string> finalChunks = new List<string>();
        string separator = separators.Last();
        List<string> newSeparators = new List<string>();

        foreach (var sep in separators)
        {
            string currentSeparator = _isSeparatorRegex ? sep : Regex.Escape(sep);
            if (string.IsNullOrEmpty(currentSeparator))
            {
                separator = sep;
                break;
            }
            if (Regex.IsMatch(text, currentSeparator))
            {
                separator = sep;
                newSeparators = separators.Skip(separators.IndexOf(sep) + 1).ToList();
                break;
            }
        }

        var splits = SplitTextWithRegex(text, separator, _keepSeparator);
        string separatorToUse = _keepSeparator ? "" : separator;
        List<string> goodSplits = new List<string>();

        foreach (var split in splits)
        {
            if (_lengthFunction(split) < _chunkSize)
            {
                goodSplits.Add(split);
            }
            else
            {
                if (goodSplits.Count > 0)
                {
                    finalChunks.AddRange(MergeSplits(goodSplits, separatorToUse));
                    goodSplits.Clear();
                }
                if (newSeparators.Count == 0)
                {
                    finalChunks.Add(split);
                }
                else
                {
                    finalChunks.AddRange(SplitTextRecursive(split, newSeparators));
                }
            }
        }

        if (goodSplits.Count > 0)
        {
            finalChunks.AddRange(MergeSplits(goodSplits, separatorToUse));
        }

        return finalChunks;
    }
}