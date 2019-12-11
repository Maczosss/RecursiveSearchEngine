using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenceFinder.Finders
{
    class NetMethodInvocationsFinder
    {
        public List<InvocationExpressionSyntax> FindMethodsInvocationsInFIle(string csFilePath)
        {
            var result = new List<InvocationExpressionSyntax>();

            var file = File.ReadAllText(csFilePath);

            var fileSyntaxTree = CSharpSyntaxTree.ParseText(file);
            //CompilationUnitSyntax compilationUnitSytnax = fileSyntaxTree.GetCompilationUnitRoot();
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