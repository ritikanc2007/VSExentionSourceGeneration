using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorParser.Models.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace SourceGeneratorParser.Parsers.Code.Visitors.Metadata
{
    internal class MethodSyntaxVisitor: CSharpSyntaxVisitor
    {
        public MethodItemInfo Method { get; set; }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {

            base.VisitMethodDeclaration(node);

            var methodName = node.Identifier.Text;
            var parameters = node.ParameterList.Parameters.ToArray() ?? Array.Empty<ParameterSyntax>();
            var returnType = node.ReturnType;

            var list = node.AttributeLists.SelectMany(al => al.Attributes).ToList();
            List<AttributeItemInfo> AttributeDefinitions = new List<AttributeItemInfo>();
            foreach (var attrSyntax in list)
            {
                var visitor = new AttributeSyntaxVisitor();
                visitor.Visit(attrSyntax);

                //attrSyntax.Accept();
                AttributeDefinitions.Add(visitor.Attribute);
            }


            // Load parameters
            var parameterDefs = new List<ArgumentItemInfo>();
            foreach (var parameter in parameters)
            {

                var name = parameter.Identifier.Text;
                var type = parameter.Type.ToString();

                parameterDefs.Add(new ArgumentItemInfo() { Name=name, Type=type }  );
            }


            Method= new MethodItemInfo
            {
                Name = methodName,
                Arguments  = parameterDefs, //fieldArguments.ToArray(),
                ReturnType = returnType.ToString(),
                Attributes = AttributeDefinitions
            };
        }

    }
}
