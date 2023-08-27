using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser.Models.Metadata
{
    public class MethodItemInfo
    {
        public string Name { get; set; }
        public List<ArgumentItemInfo> Arguments { get; set; }

        public string ReturnType { get; set; }

        public string QualifiedName { get; set; }
        public List<AttributeItemInfo> Attributes { get; set; }


    }
}
