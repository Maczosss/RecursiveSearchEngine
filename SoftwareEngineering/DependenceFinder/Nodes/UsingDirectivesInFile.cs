using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace DependenceFinderAndPlotter.Nodes
{
    public class UsingDirectivesInFile 
    {
        public List<UsingDirectiveSyntax> UsingDirectives { get; set; }
        public string InFile { get; set; }
        public string fileSize { get; set; }

        public UsingDirectivesInFile()
        {
            UsingDirectives = new List<UsingDirectiveSyntax>();
        }
    }
}
