using System;
using System.Collections.Generic;
using System.Text;
using SourceGeneratorParser.Models.Types;

namespace SourceGeneratorParser.Models.Metadata
{
    public class InheritedTypeInfo
    {
        public List<ClassDefinitionTypeInfo> Classes { get; set; }
        public List<InterfaceDefinitionTypeInfo> Interfaces { get; set; }


    }
}
