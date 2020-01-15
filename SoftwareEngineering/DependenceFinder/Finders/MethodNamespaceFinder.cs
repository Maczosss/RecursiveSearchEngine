using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DependenceFinderAndPlotter.Finders
{
    public class MethodNamespaceFinder
    {
        public NamespaceDeclarationSyntax GetNamespaceOfMethodDeclaration(MethodDeclarationSyntax method)
        {
            NamespaceDeclarationSyntax result;
            //var tree = method.SyntaxTree;
            //CSharpCompilation compilation = CSharpCompilation.Create("MethodCompilation", syntaxTrees: new[] { tree });
            //var semanticModel = compilation.GetSemanticModel(tree, true);
            var descendantNamespace = method.AncestorsAndSelf().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
            result = descendantNamespace;
            return result;
        }
    }
}
