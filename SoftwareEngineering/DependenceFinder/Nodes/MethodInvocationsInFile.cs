using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenceFinder.Nodes
{
    class MethodInvocationsInFile
    {
        public List<InvocationExpressionSyntax> MethodInvocations { get; set; }
        public string InFile { get; set; }

        public MethodInvocationsInFile()
        {
            this.MethodInvocations = new List<InvocationExpressionSyntax>();
        }
    }
}
