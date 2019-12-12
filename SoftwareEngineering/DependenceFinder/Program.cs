using DependenceFinder.Finders;
using DependenceFinder.Nodes;
using FilesFinder;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
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
            var usingDirectiveFinder = new UsingDirectiveFinder();

            var methodDeclarationsFinder = new MethodDeclarationsFinder();
            var methodInvocationsFinder = new MethodInvocationsFinder();

            var declarationsInFilesResult = new List<MethodDeclarationsInFile>();
            var invocationsInFilesResult = new List<MethodInvocationsInFile>();
            var usingsInFilesResult = new List<UsingDirectivesInFile>();

            
            foreach (var path in foundCsFiles)
            {
                var singleFileUsings = new UsingDirectivesInFile()
                {
                    InFile = path,
                    UsingDirectives = usingDirectiveFinder.GetUsingDirecitvesInFileRoslyn(path)
                };
                usingsInFilesResult.Add(singleFileUsings);

                var singleFileDeclarations = new MethodDeclarationsInFile()
                {
                    InFile = path,
                    MethodDeclarations = methodDeclarationsFinder.GetMethodsDecalarationsInFIle(path)
                };
                declarationsInFilesResult.Add(singleFileDeclarations);

                var singleFileInvocations = new MethodInvocationsInFile()
                {
                    InFile = path,
                    MethodInvocations = methodInvocationsFinder.GetMethodsInvocationsInFIle(path)
                };
                invocationsInFilesResult.Add(singleFileInvocations);

            }
            foreach (var declarations in declarationsInFilesResult)
            {
                foreach (var declaration in declarations.MethodDeclarations)
                {
                    Console.WriteLine(declaration.Identifier);
                }
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
