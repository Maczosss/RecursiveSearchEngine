using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

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
