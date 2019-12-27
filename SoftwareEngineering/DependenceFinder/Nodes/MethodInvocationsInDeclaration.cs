using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace DependenceFinderAndPlotter.Nodes
{
    public class MethodInvocationsInDeclaration
    {
        public List<InvocationExpressionSyntax> MethodInvocations { get; set; }
        public MethodDeclarationSyntax InDeclaration { get; set; }

        public MethodInvocationsInDeclaration()
        {
            this.MethodInvocations = new List<InvocationExpressionSyntax>();
        }
    }
}
