using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace DependenceFinder.Nodes
{
    class UsingDirectivesInFile //: IDependable
    {
        public List<UsingDirectiveSyntax> UsingDirectives { get; set; }
        public string InFile { get; set; }

        public UsingDirectivesInFile()
        {
            UsingDirectives = new List<UsingDirectiveSyntax>();
        }
    }
}
