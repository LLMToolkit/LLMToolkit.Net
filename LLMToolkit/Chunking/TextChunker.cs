namespace LLMToolkit.Chunking;

public abstract class TextChunker : ITextChunker
{
    protected int _chunkSize;
    protected int _chunkOverlap;

    protected bool _keepSeparator;
    protected bool _addStartIndex;
    protected bool _stripWhitespace;

    protected Func<string, int> _lengthFunction;

    protected string _text;
    protected List<string> _chunks;


    public TextChunker(
        int chunkSize = 500,
        int chunkOverlap = 100,
        Func<string, int> lengthFunction = null,
        bool keepSeparator = false,
        bool addStartIndex = false,
        bool stripWhitespace = true
        ) : base()
    {
        // _chunkSize mayor que cero
        if (chunkSize <= 0)
        {
            throw new ArgumentException("Chunk size must be greater than zero", nameof(chunkSize));
        }
        _chunkSize = chunkSize;
        
        if (chunkOverlap > chunkSize)
        {
            throw new ArgumentException($"Got a larger chunk overlap ({chunkOverlap}) than chunk size ({chunkSize}), should be smaller.");
        }
        
        _chunkOverlap = chunkOverlap;
        _lengthFunction = lengthFunction ?? (s => s.Length);
        _keepSeparator = keepSeparator;
        _addStartIndex = addStartIndex;
        _stripWhitespace = stripWhitespace;
        _chunks = new List<string>();
    }



    public void SetText(string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Text cannot be null or empty", nameof(text));

        _text = text;
    }



    public abstract List<string> ChunkText();



    public List<string> GetChunks()
    {
        return _chunks;
    }



    public void ClearChunks()
    {
        _chunks.Clear();
    }
    
    
    protected List<string> MergeSplits(IEnumerable<string> splits, string separator)
    {
        int separatorLen = _lengthFunction(separator);
        List<string> docs = new List<string>();
        List<string> currentDoc = new List<string>();
        int total = 0;

        foreach (var d in splits)
        {
            int length = _lengthFunction(d);
            if (total + length + (currentDoc.Count > 0 ? separatorLen : 0) > _chunkSize)
            {
                if (total > _chunkSize)
                {
                    Console.WriteLine($"Created a chunk of size {total}, which is longer than the specified {_chunkSize}");
                }
                if (currentDoc.Count > 0)
                {
                    var doc = JoinDocs(currentDoc, separator);
                    if (doc != null)
                    {
                        docs.Add(doc);
                    }

                    while (total > _chunkOverlap || (total + length + (currentDoc.Count > 0 ? separatorLen : 0) > _chunkSize && total > 0))
                    {
                        total -= _lengthFunction(currentDoc[0]) + (currentDoc.Count > 1 ? separatorLen : 0);
                        currentDoc.RemoveAt(0);
                    }
                }
            }

            currentDoc.Add(d);
            total += length + (currentDoc.Count > 1 ? separatorLen : 0);
        }

        var finalDoc = JoinDocs(currentDoc, separator);
        if (finalDoc != null)
        {
            docs.Add(finalDoc);
        }

        return docs;
    }


    private string JoinDocs(List<string> docs, string separator)
    {
        return string.Join(separator, docs);
    }



}