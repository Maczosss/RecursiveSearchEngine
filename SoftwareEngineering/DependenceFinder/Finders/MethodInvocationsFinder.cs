using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DependenceFinderAndPlotter.Finders
{
    public class MethodInvocationsFinder
    {
        public List<InvocationExpressionSyntax> GetMethodsInvocationsInFIle(string csFilePath)
        {
            var result = new List<InvocationExpressionSyntax>();

            var file = File.ReadAllText(csFilePath);

            var fileSyntaxTree = CSharpSyntaxTree.ParseText(file);
            var root = fileSyntaxTree.GetRoot();

            var methodInvocationTreeNodes = root.DescendantNodes().OfType<InvocationExpressionSyntax>();
            foreach (var invocation in methodInvocationTreeNodes)
            {
                result.Add(invocation);
            }
            return result;
        }

        public List<InvocationExpressionSyntax> GetMethodsInvocationsDeclaration(MethodDeclarationSyntax ancestor)
        {
            var result = new List<InvocationExpressionSyntax>();
            var descendantInvocation = ancestor.DescendantNodes().OfType<InvocationExpressionSyntax>();
            result.AddRange(descendantInvocation);
            return result;
        }
    }
}
//After declarations finder done:
//Regex option: ~2 days (5h)
//Roslyn: 90 seconds.