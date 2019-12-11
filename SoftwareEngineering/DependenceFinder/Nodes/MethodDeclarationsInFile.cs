using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenceFinder.Nodes
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
