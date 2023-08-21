using Microsoft.CodeAnalysis;
using SourceGeneratorParser.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser.Parsers.Code.Visitors
{
    internal interface ITypeVisitor
    {
        TypeDefinitionInfo  TypeDefinition
        {
            get;
            set;
        }

    }
}
