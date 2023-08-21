using SourceGeneratorParser.Models.Metadata;
using SourceGeneratorParser.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser.Parsers.Common
{
    public interface IParser
    {

        TypeDefinitionInfo Parse(ParsingDetails parsingDetails);
        
    }
}
