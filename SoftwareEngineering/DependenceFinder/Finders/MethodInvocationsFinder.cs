using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DependenceFinder.Finders
{
    class MethodInvocationsFinder
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
    }
}
//After declarations finder done:
//Regex option: ~2 days (5h)
//Roslyn: 90 seconds.