using SourceGeneratorParser.Parsers.Code;
using SourceGeneratorParser.Parsers.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace SourceGeneratorParser.Parsers
{
    public class TransformFactory
    {

        public static IParser Create(ParserType parserType)
        {
            switch (parserType)
            {
                case ParserType.SyntaxTree:
                    return new CodeSyntaxParser();
                default:
                    return null;
            };
        }
    }
}

