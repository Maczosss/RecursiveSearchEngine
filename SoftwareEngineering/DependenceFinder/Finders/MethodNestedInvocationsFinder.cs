using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenceFinderAndPlotter.Finders
{
    class MethodNestedInvocationsFinder
    {
        public List<InvocationExpressionSyntax> GetNestedInvocations(MethodDeclarationSyntax methodDeclaration)
        {
            var result = new List<InvocationExpressionSyntax>();
            var childNodes = methodDeclaration.ChildNodes();
            var childInvocations = childNodes.OfType<InvocationExpressionSyntax>().ToList();
            result.AddRange(childInvocations);
            return result;
        }
    }
}
