using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
