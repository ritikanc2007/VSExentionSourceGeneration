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
            StringBuilder wrongQualifiedString = new StringBuilder(); // need to check how to get QualifiedName of method
            int index = 0;

           
            foreach (var parameter in parameters)
            {
                index+=1;
                var name = parameter.Identifier.Text;
                var type = parameter.Type.ToString();

                                wrongQualifiedString.Append($"{type}" + (index ==  parameters.Count()-1 ? "," : "")); 
                parameterDefs.Add(new ArgumentItemInfo() { Name=name, Type=type }  );
            }
            var paramString = wrongQualifiedString.ToString();
            var qualifiedName = string.Empty;
            if (!string.IsNullOrEmpty(paramString))
            {
                qualifiedName = $"{methodName}({paramString})";
            }
            else
                qualifiedName = methodName;

            Method= new MethodItemInfo
            {
                Name = methodName,
                Arguments  = parameterDefs, //fieldArguments.ToArray(),
                ReturnType = returnType.ToString(),
                Attributes = AttributeDefinitions,
                QualifiedName= qualifiedName
            };
        }

    }
}
