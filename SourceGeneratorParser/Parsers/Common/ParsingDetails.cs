using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser.Parsers.Common
{
    public class ParsingDetails
    {
        public ParsingDetails(string data)
        {
            Data=data;
        }

        public string Data { get; set; }
        
    }
}
