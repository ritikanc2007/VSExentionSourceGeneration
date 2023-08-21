using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser
{
   public enum ParserType
    {
       
        SyntaxTree,
        Json,
        Xml

    }

    public enum TypeOfDeclaration
    {
        Unknown,
        Class,
        Interface,
        Struct,
        Record

    }
}
