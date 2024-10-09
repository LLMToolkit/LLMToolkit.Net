using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using LLMToolkit.Chunking;
namespace LLMToolkit.Samples;

public static class SamplesChunking
{
    public static async Task TestChunk1()
    {
        // create a recursive text chunker
        //TextChunkerCharacter chunker = new TextChunkerCharacter(500,30, " ");
        TextChunkerRecursive chunker = new TextChunkerRecursive(null,600);

        chunker.SetText("Implicit conversions seem to be a growing trend in the SQL Server performance " +
                        "tuning work that I’ve been engaged in recently, and I’ve blogged in the past about " +
                        "ways to identify when implicit conversions are occurring using the plan cache.\r\n\r\nFor " +
                        "those of you who don’t know, implicit conversions occur whenever data with two different " +
                        "data types are being compared, based on the data type precedence.  The precedence " +
                        "establishes the hierarchy of of the types, and lower precedence data types will always " +
                        "be implicitly converted up to the higher precedence type.  These conversions increase " +
                        "CPU usage for the operation, and when the conversion occurs on a table column can also " +
                        "result in an index scan where an index seek would have been possible without the implicit " +
                        "conversion.\r\n\r\nFor a long time I’ve wanted to map out the most common data types and the " +
                        "effect of a column-side implicit conversion for creating an index seek versus an index scan " +
                        "and recently I finally got around to mapping it all out.\r\n\r\nSetting up the tests\r\nTo " +
                        "map out the implicit conversion affects I created two databases using different collations, " +
                        "one using SQL_Latin_General_CP1_CI_AS and the other using Latin_General_CI_AS, and then " +
                        "created the following table in each of the databases.");

        var x = chunker.ChunkText();
        foreach (var chunk in x)
        {
            Console.WriteLine(chunk);
            Console.WriteLine("-------------------------------------------------");
        }
    }

    private static int LengthFunction(string text)
    {
        return text.Length;
    }
}