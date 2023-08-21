using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser.Models.Metadata
{
    public class AttributeItemInfo
    {
        public string Name { get; set; }

        public List<ArgumentItemInfo> Arguments { get; set; }

    }
}
