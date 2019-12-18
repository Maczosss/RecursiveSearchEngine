using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace DependenceFinderAndPlotter.Nodes
{
    class MethodDeclarationsInFile
    {
        public List<MethodDeclarationSyntax> MethodDeclarations { get; set; }
        public string InFile { get; set; }

        public MethodDeclarationsInFile()
        {
            this.MethodDeclarations = new List<MethodDeclarationSyntax>();
        }
    }
}
