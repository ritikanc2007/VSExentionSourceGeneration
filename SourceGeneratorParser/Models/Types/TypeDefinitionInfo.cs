using SourceGeneratorParser.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser.Models.Types
{
    public class TypeDefinitionInfo
    {
        public string Name { get; set; }

        public string PluralName { get; set; }

        public TypeOfDeclaration DeclarationType { get; set; }
        private TypeInfo DeclarationInfo { get; set; }
        private List<InheritedTypeInfo> InheritredTypes { get; set; }
        private List<ConstructorItemInfo> Constructors { get; set; }

        public List<AttributeItemInfo> Attributes { get; set; }
        public List<MemberItemInfo> Members { get; set; }

        public List<MethodItemInfo> Methods { get; set; }


    }
}
