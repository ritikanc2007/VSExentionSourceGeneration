using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser.Models.Metadata
{
    public class MemberItemInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }

        List<AttributeItemInfo> Attributes { get; set; }
    }
}
