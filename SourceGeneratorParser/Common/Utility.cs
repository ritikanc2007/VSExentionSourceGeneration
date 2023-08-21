using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGeneratorParser.Common
{
    internal static class Utility
    {
        public static string Alternate(this string value, string alternateValue)
        {

            return string.IsNullOrEmpty(value) ? alternateValue : value;

        }

        public static string CleanChars(this string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value.Replace("\"", "");
            else
                return value;
        }
        public static bool IsTypeDeclarationSyntax(SyntaxNode node)
        {
            if (node is InterfaceDeclarationSyntax interfaceDeclarationSyntax ||
                node is ClassDeclarationSyntax classDeclarationSyntax ||
                   node is RecordDeclarationSyntax recordDeclarationSyntax ||
                       node is StructDeclarationSyntax structDeclarationSyntax)
                        return true;
            return false;
        }

        public static TypeOfDeclaration DeclarationType(SyntaxNode node)
        {
            if (node is InterfaceDeclarationSyntax interfaceDeclarationSyntax)
                return TypeOfDeclaration.Interface;
            if (node is ClassDeclarationSyntax classDeclarationSyntax)
                return TypeOfDeclaration.Class;
            if (node is RecordDeclarationSyntax recordDeclarationSyntax)
                return TypeOfDeclaration.Record;
            if (node is StructDeclarationSyntax structDeclarationSyntax)
                return TypeOfDeclaration.Struct;
            return  TypeOfDeclaration.Unknown;
        }
    }
}
