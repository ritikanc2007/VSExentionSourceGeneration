using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorParser.Common;
using SourceGeneratorParser.Models.Metadata;
using SourceGeneratorParser.Models.Types;
using SourceGeneratorParser.Parsers.Code.Visitors.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;

namespace SourceGeneratorParser.Parsers.Code.Visitors.Types
{
    internal class TypeDeclarationVisitor : CSharpSyntaxVisitor, ITypeVisitor
    {
        public TypeDefinitionInfo TypeDefinition { get; set; } = new TypeDefinitionInfo();
        public override void Visit(SyntaxNode node)
        {
            var baseNode = (BaseTypeDeclarationSyntax)node;

            List<MethodItemInfo> methods = GetMethods(node);

            // load basic type info i.e. name
            TypeDefinition = new TypeDefinitionInfo()
            {
                Name = baseNode.Identifier.Text,
                DeclarationType= Utility.DeclarationType(node),
                Members= GetMembers(node),
                Methods=  GetMethods(node),
                Attributes = GetAttributes(node)
            };



        }
        List<AttributeItemInfo> GetAttributes(SyntaxNode node)
        {
            List<AttributeItemInfo> attributes = new List<AttributeItemInfo>();
            List<AttributeSyntax> attributeSyntaxes = null;

            if (node is InterfaceDeclarationSyntax interfaceDeclarationSyntax)
                attributeSyntaxes =  interfaceDeclarationSyntax.AttributeLists.SelectMany(al => al.Attributes).ToList();
            if (node is ClassDeclarationSyntax classDeclarationSyntax)
                attributeSyntaxes =  classDeclarationSyntax.AttributeLists.SelectMany(al => al.Attributes).ToList();
            if (node is RecordDeclarationSyntax recordDeclarationSyntax)
                attributeSyntaxes =  recordDeclarationSyntax.AttributeLists.SelectMany(al => al.Attributes).ToList();
            if (node is StructDeclarationSyntax structDeclarationSyntax)
                attributeSyntaxes =  structDeclarationSyntax.AttributeLists.SelectMany(al => al.Attributes).ToList();
            List<MethodItemInfo> methods = new List<MethodItemInfo>();

            if (attributeSyntaxes != null)
            {
                foreach (var syntax in attributeSyntaxes)
                {
                    AttributeSyntaxVisitor visitor = new AttributeSyntaxVisitor();
                    visitor.Visit(syntax);
                    attributes.Add(visitor.Attribute);
                }
            }
            return attributes;
        }
        private List<MethodItemInfo> GetMethods(SyntaxNode node)
        {
            List<MethodItemInfo> methods = new List<MethodItemInfo>();

            var methodSyntaxes = GetMethodSyntaxes(node);

            foreach (var methodSyntax in methodSyntaxes)
            {
                MethodSyntaxVisitor visitor = new MethodSyntaxVisitor();
                visitor.Visit(methodSyntax);
                methods.Add(visitor.Method);
            }

            return methods;
        }

        private List<MethodDeclarationSyntax> GetMethodSyntaxes(SyntaxNode node)
        {
            SyntaxNode root = node.SyntaxTree.GetRoot();
            var methods = root.DescendantNodes()
               .OfType<MethodDeclarationSyntax>()
               .ToList();
            return methods;
        }
        private List<MemberItemInfo> GetMembers(SyntaxNode node)
        {
            SyntaxList<MemberDeclarationSyntax> memberSyntaxes;
            if (node is InterfaceDeclarationSyntax interfaceDeclarationSyntax)
                memberSyntaxes = interfaceDeclarationSyntax.Members;
            if (node is ClassDeclarationSyntax classDeclarationSyntax)
                memberSyntaxes = classDeclarationSyntax.Members;
            if (node is RecordDeclarationSyntax recordDeclarationSyntax)
                memberSyntaxes = recordDeclarationSyntax.Members;
            if (node is StructDeclarationSyntax structDeclarationSyntax)
                memberSyntaxes = structDeclarationSyntax.Members;


            var memberList = new List<MemberItemInfo>();

            foreach (var member in memberSyntaxes)
            {
                if (member is PropertyDeclarationSyntax prop)
                {
                    string name = prop.Identifier.ToString();
                    var type = prop.Type.ToString();

                    memberList.Add(new MemberItemInfo() { Name=name, Type=type });
                }
            }
            return memberList;
        }
    }
}
