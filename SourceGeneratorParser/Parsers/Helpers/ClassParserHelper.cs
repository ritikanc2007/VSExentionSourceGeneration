using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using Microsoft.CodeAnalysis;
using SourceGeneratorParser.Parsers.Code;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;

namespace SourceGeneratorParser.Parsers.Helpers
{
    public static class ClassParserHelper
    {

        public static string AddNewMethodsToClass(string typeSourceCode, List<string> methodSourceCodes)
        {
            CancellationToken token = new CancellationTokenSource().Token;
            var rootType = CSharpSyntaxTree.ParseText(typeSourceCode).GetRoot(token);

            SyntaxNode typeSyntaxNode = null;
            CodeSyntaxParser.FindTypeDeclarationSyntax(rootType.ChildNodes(), out typeSyntaxNode);

            
            if (typeSyntaxNode is ClassDeclarationSyntax classSyntaxNode)
            {
                // Replacing class node
                // access old node
                ClassDeclarationSyntax old = classSyntaxNode;
                // Search node with identified i.e classname e.g. IUserRepository and store new node
                ClassDeclarationSyntax New = old.WithIdentifier(classSyntaxNode.Identifier);
                foreach (var methodCode in methodSourceCodes)
                {

                    var methodSyntax = CSharpSyntaxTree.ParseText($@" public class temp{{ {methodCode} }}").GetRoot(token);




                    ClassDeclarationSyntax nodeAfterAddingMethod = null;
                    if (methodSyntax != null)
                    {
                        var method = methodSyntax.DescendantNodes()
                          .OfType<MethodDeclarationSyntax>()
                          .FirstOrDefault();

                        

                        
                        // add member to new node
                        if (method is MethodDeclarationSyntax)
                        {
                           
                         
                            nodeAfterAddingMethod = New.AddMembers(method);
                            New =nodeAfterAddingMethod;


                        }
                        
                        //old=nodeAfterAddingMethod;
                        //New=nodeAfterAddingMethod;
                       // var codeAfter = rootType.ToFullString();

                    }

                    
                }

                // replace old node with new in the ROOT
                var code = New.ToFullString();
                rootType = rootType.ReplaceNode(old, New);
                var codeAfter = rootType.ToFullString();
                //old=nodeAfterAddingMethod;
                return rootType.ToFullString();

            }

            return string.Empty;
        }
    }

    public class Rewriter:CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            base.VisitMethodDeclaration(node);
            return node;
            
        }
    }
}
