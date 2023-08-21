using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorParser.Common;
using SourceGeneratorParser.Models.Types;
using SourceGeneratorParser.Parsers.Code.Visitors;
using SourceGeneratorParser.Parsers.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser.Parsers.Code
{
    public class CodeSyntaxParser : IParser
    {
        public TypeDefinitionInfo Parse(ParsingDetails sourceCode)
        {

            var root = ValidateSourceCode(sourceCode); // Try to parse and get Root syntax node

            SyntaxNode typeDeclarationSyntaxNode = null;

            FindTypeDeclarationSyntax(root.ChildNodes(), out typeDeclarationSyntaxNode); //Recursively find type declaration node
            if (typeDeclarationSyntaxNode != null)
            {
                CompilationUnitVisitor visitor = new CompilationUnitVisitor();

                visitor.Visit(typeDeclarationSyntaxNode);
                return visitor.TypeDefinition;
             }
            return null;
        }

        private SyntaxNode ValidateSourceCode(ParsingDetails sourceCode)
        {
            CompilationUnitSyntax root = null;
            try
            {
                root =   CSharpSyntaxTree.ParseText(sourceCode.Data).GetCompilationUnitRoot();
            }
            catch (Exception ex)
            {

                throw new Exception("Couldnot parse the provided source code string", ex);
            }

            return root;
        }

        private void FindTypeDeclarationSyntax(IEnumerable<SyntaxNode> childNodes, out SyntaxNode typeDeclarationSyntaxNode)
        {
            typeDeclarationSyntaxNode = null;
            foreach (var node in childNodes)
            {
                if (Utility.IsTypeDeclarationSyntax(node))
                {
                    typeDeclarationSyntaxNode = node;
                    return;
                }

                FindTypeDeclarationSyntax(node.ChildNodes(), out typeDeclarationSyntaxNode);
            }
        }

    }
}
