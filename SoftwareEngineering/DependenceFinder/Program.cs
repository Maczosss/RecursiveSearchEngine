using DependenceFinder.Finders;
using DependenceFinder.Nodes;
using FilesFinder;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DependenceFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            NetFilesFinder netFilesFinder = new NetFilesFinder();
            var foundCsFiles = netFilesFinder.FindCsFiles();

            //NetClassDefinitionsFinder netClassesFinder = new NetClassDefinitionsFinder(foundNetFiles);
            //var foundClassesDefinitionsInFiles = netClassesFinder.findClassDefinitions();

            //var result = netClassesFinder.findClassesUsages(foundClassesDefinitionsInFiles, foundNetFiles);
            var usingDirectiveFinder = new NetUsingDirectiveFinder();

            var methodDeclarationsFinder = new NetMethodDeclarationsFinder();
            var methodInvocationsFinder = new NetMethodInvocationsFinder();

            var declarationsInFilesResult = new List<MethodDeclarationsInFile>();
            var invocationsInFilesResult = new List<MethodInvocationsInFile>();
            var usingsInFilesResult = new List<UsingDirectivesInFile>();

            foreach (var path in foundCsFiles)
            {
                var singleFileUsings = new UsingDirectivesInFile()
                {
                    InFile = path,
                    UsingDirectives = usingDirectiveFinder.findUsingDirecitvesInFileRoslyn(path)
                };
                usingsInFilesResult.Add(singleFileUsings);

                var singleFileDeclarations = new MethodDeclarationsInFile()
                {
                    InFile = path,
                    MethodDeclarations = methodDeclarationsFinder.FindMethodsDecalarationsInFIle(path)
                };
                declarationsInFilesResult.Add(singleFileDeclarations);

                var singleFileInvocations = new MethodInvocationsInFile()
                {
                    InFile = path,
                    MethodInvocations = methodInvocationsFinder.FindMethodsInvocationsInFIle(path)
                };
                invocationsInFilesResult.Add(singleFileInvocations);

            }

            //finding if any descendant of mehod declaration is of method invocation type:
            foreach (var declarations in declarationsInFilesResult)
            {
                foreach (var declaration in declarations.MethodDeclarations)
                {
                    
                    IEnumerable<InvocationExpressionSyntax> allInvocations = 
                        declaration.DescendantNodes().OfType<InvocationExpressionSyntax>();
                }
            }

        }
    }
}
