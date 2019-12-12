using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DependenceFinder
{
    class UsingDirectiveFinder
    {
        public List<UsingDirectiveSyntax> GetUsingDirecitvesInFileRoslyn(string csFilePath)
        {
            var result = new List<UsingDirectiveSyntax>();

            var file = File.ReadAllText(csFilePath);
            var fileSyntaxTree = CSharpSyntaxTree.ParseText(csFilePath);
            var root = fileSyntaxTree.GetRoot();
            
            var usingDirectives = root.DescendantNodesAndSelf().OfType<UsingDirectiveSyntax>();
            foreach (var @using in usingDirectives)
            {
                result.Add(@using);
            }

            return result;
        }
    }
}
