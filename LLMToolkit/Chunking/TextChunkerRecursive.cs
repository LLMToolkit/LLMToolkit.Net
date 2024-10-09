using LLMToolkit.Chunking;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace LLMToolkit.Chunking;

public class TextChunkerRecursive : TextChunker
{
    private bool _isSeparatorRegex;
    //private bool _keepSeparator;
    //private int _chunkSize;    
    // Función para obtener la longitud del texto
    //private Func<string, int> _lengthFunction;

    private string[] _separators;



    public TextChunkerRecursive(
        string[] separators, 
        int chunkSize = 500,
        int chunkOverlap = 100,
        Func<string, int> lengthFunction = null,
        bool keepSeparator = false,
        bool addStartIndex = false,
        bool stripWhitespace = true,
        bool isSeparatorRegex = false
        ) : base(chunkSize, chunkOverlap, lengthFunction, keepSeparator, addStartIndex, stripWhitespace)
    {
        _isSeparatorRegex = isSeparatorRegex;
        //if separators is null
        if (separators == null)
        {
            _separators = new[] { "\n\n", "\r\n", "\n", " ", "" };
        }
        else
        {
            _separators = separators;
        }
        
    }


    

    public override List<string> ChunkText()
    {
        if (string.IsNullOrEmpty(_text))
            throw new InvalidOperationException("Text has not been set");

        if (_chunkSize <= 0)
            throw new ArgumentException("Chunk size must be greater than zero", nameof(_chunkSize));

        _chunks.Clear();
        _chunks = ChunkTextRecursive(_text, new List<string>(_separators));
        return _chunks;
    }





    private List<string> ChunkTextRecursive(string text, List<string> separators)
    {
        
        var finalChunks = new List<string>();
        string separator = separators[separators.Count - 1];
        List<string> newSeparators = new List<string>();

        for (int i = 0; i < separators.Count; i++)
        {
            string _separator = _isSeparatorRegex ? separators[i] : Regex.Escape(separators[i]);
            if (separators[i] == "")
            {
                separator = separators[i];
                break;
            }
            if (Regex.IsMatch(text, _separator))
            {
                separator = separators[i];
                newSeparators = separators.GetRange(i + 1, separators.Count - (i + 1));
                break;
            }
        }

        string escapedSeparator = _isSeparatorRegex ? separator : Regex.Escape(separator);
        List<string> splits = SplitTextWithRegex(text, escapedSeparator, _keepSeparator);

        List<string> goodSplits = new List<string>();
        string finalSeparator = _keepSeparator ? "" : separator;

        foreach (string s in splits)
        {
            if (_lengthFunction(s) < _chunkSize)
            {
                goodSplits.Add(s);
            }
            else
            {
                if (goodSplits.Count > 0)
                {
                    var mergedText = MergeSplits(goodSplits, finalSeparator);
                    finalChunks.AddRange(mergedText);
                    goodSplits.Clear();
                }

                if (newSeparators.Count == 0)
                {
                    finalChunks.Add(s);
                }
                else
                {
                    List<string> otherInfo = ChunkTextRecursive(s, newSeparators);
                    finalChunks.AddRange(otherInfo);
                }
            }
        }

        if (goodSplits.Count > 0)
        {
            var mergedText = MergeSplits(goodSplits, finalSeparator);
            finalChunks.AddRange(mergedText);
        }

        return finalChunks;
    }




    private List<string> SplitTextWithRegex(string text, string separator, bool keepSeparator)
    {
        string pattern = keepSeparator ? $"({separator})" : separator;
        return new List<string>(Regex.Split(text, pattern));
    }


}
