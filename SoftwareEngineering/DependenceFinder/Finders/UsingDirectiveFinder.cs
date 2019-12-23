using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DependenceFinderAndPlotter
{
    public class UsingDirectiveFinder
    {
        public List<UsingDirectiveSyntax> GetUsingDirecitvesInFileRoslyn(string csFilePath)
        {
            var result = new List<UsingDirectiveSyntax>();

            var file = File.ReadAllText(csFilePath);
            SyntaxTree fileSyntaxTree = CSharpSyntaxTree.ParseText(file);

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
