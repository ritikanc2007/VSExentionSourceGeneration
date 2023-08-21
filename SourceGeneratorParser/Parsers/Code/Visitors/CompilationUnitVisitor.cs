using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorParser.Common;
using SourceGeneratorParser.Models.Types;
using SourceGeneratorParser.Parsers.Code.Visitors.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser.Parsers.Code.Visitors
{
    internal class CompilationUnitVisitor : CSharpSyntaxVisitor, ITypeVisitor
    {
        public TypeDefinitionInfo TypeDefinition { get; set; }
        public override void Visit(SyntaxNode node)
        {
            TypeDeclarationVisitor visitor = new TypeDeclarationVisitor();
            if (Utility.IsTypeDeclarationSyntax(node))
            {
                visitor.Visit(node);

            }
            TypeDefinition = visitor.TypeDefinition;
        }
    }
}
