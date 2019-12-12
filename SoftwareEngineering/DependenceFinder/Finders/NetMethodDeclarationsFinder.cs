using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DependenceFinder.Finders
{
    class NetMethodDeclarationsFinder
    {
        public NetMethodDeclarationsFinder()
        {
        }

        public List<MethodDeclarationSyntax> FindMethodsDecalarationsInFIle(string csFilePath)
        {
            var result = new List<MethodDeclarationSyntax>();

            var file = File.ReadAllText(csFilePath);

            var fileSyntaxTree = CSharpSyntaxTree.ParseText(file);
            var root = fileSyntaxTree.GetRoot();

            var methodDeclarationTreeNodes = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach (var declaration in methodDeclarationTreeNodes)
            {
                result.Add(declaration);
            }

            return result;

        }


        //var usingStatements = root.DescendantNodesAndSelf().OfType<UsingStatementSyntax>(); //i.e. using (StreamReader reader = new StreamReader(path)){};

        //var namespaces = root.DescendantNodes().OfType<NamespaceDeclaration>();
        //var namespaces2 = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>();


    }
}
