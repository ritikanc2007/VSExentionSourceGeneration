using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorParser.Common;
using SourceGeneratorParser.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SourceGeneratorParser.Parsers.Code.Visitors.Metadata
{
    internal class AttributeSyntaxVisitor : CSharpSyntaxVisitor
    {
        public AttributeItemInfo  Attribute { get; set; }
        public override void VisitAttribute(AttributeSyntax node)
        {
            base.VisitAttribute(node);

            List<ArgumentItemInfo> argumentsInfo = new List<ArgumentItemInfo>();

            var fieldArguments = new List<(string Name, object Value)>();
            var propertyArguments = new List<(string Name, object Value)>();

            var arguments = node.ArgumentList?.Arguments.ToArray() ?? Array.Empty<AttributeArgumentSyntax>();
            foreach (var syntax in arguments)
            {
                if (syntax.NameColon != null)
                {
                    argumentsInfo.Add(new ArgumentItemInfo()
                    {
                        Name= syntax.NameColon.Name.ToString(),
                        Value = syntax.Expression.ToString().CleanChars(),
                    });
                   // fieldArguments.Add((syntax.NameColon.Name.ToString(), syntax.Expression));
                }
                else if (syntax.NameEquals != null)
                {
                    argumentsInfo.Add(new ArgumentItemInfo()
                    {
                        Name= syntax.NameEquals.Name.ToString(),
                        Value = syntax.Expression.ToString().CleanChars(),
                    });
                    //propertyArguments.Add((syntax.NameEquals.Name.ToString(), syntax.Expression));
                }
                else
                {
                    
                    argumentsInfo.Add(new ArgumentItemInfo()
                    {
                        Name= string.Empty,
                        Value = syntax.Expression.ToString().CleanChars(),
                    });
                    //fieldArguments.Add((string.Empty, syntax.Expression));
                }
            }

            Attribute =(new AttributeItemInfo
            {
                Name = node.Name.ToString(),
                Arguments = argumentsInfo
            });
        }
    }
}
