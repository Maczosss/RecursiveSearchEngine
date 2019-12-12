using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DependenceFinder.Finders
{
    class MethodNamespaceFinder
    {
        public List<NamespaceDeclarationSyntax> GetNamespaceOfMethodDeclaration(MethodDeclarationSyntax method)
        {
            var result = new List<NamespaceDeclarationSyntax>();
            var descendats = method.DescendantNodesAndSelf().OfType<NamespaceDeclarationSyntax>().ToList();
            result.AddRange(descendats);
            return result;
        }
    }
}
