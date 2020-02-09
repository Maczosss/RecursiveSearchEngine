using DependenceFinderAndPlotter;
using DependenceFinderAndPlotter.Finders;
using DependenceFinderAndPlotter.Nodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Msagl.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace VisualRepresentation.Models
{
    public class GraphModels
    {
        public Graph GenerateGraph(bool story1, bool story2, bool story3, bool story6, List<string> VMFoundFiles)
        {
            Graph resultGraph = new Graph();
            var story1FileNode = resultGraph.AddNode("story1File");
            story1FileNode.Attr.Shape = Shape.Octagon;
            story1FileNode.Attr.FillColor = Color.DarkSeaGreen;

            var story1UsingNode = resultGraph.AddNode("story1UsingNode");
            story1UsingNode.Attr.Shape = Shape.Box;
            story1UsingNode.Attr.FillColor = Color.Khaki;

            resultGraph.AddEdge("story1File", "story1UsingNode");

            var story2DeclarationNode = resultGraph.AddNode("story2DeclarationNode");
            story2DeclarationNode.Attr.Shape = Shape.Ellipse;
            story2DeclarationNode.Attr.FillColor = Color.CadetBlue;

            var story2InvocationNode = resultGraph.AddNode("story2InvocationNode");
            story2InvocationNode.Attr.Shape = Shape.Ellipse;
            story2InvocationNode.Attr.FillColor = Color.AliceBlue;

            resultGraph.AddEdge("story2DeclarationNode", "story2InvocationNode");


            #region generate graphs
            List<Graph> Graphs = new List<Graph>();
            if (story1)
            {
                var story1Graph = generateStory1Graph(VMFoundFiles);
                Graphs.Add(story1Graph);
            }
            if (story2)
            {
                var story2Graph = generateStory2Graph(VMFoundFiles, true);
                Graphs.Add(story2Graph);
            }
            if (story3)
            {
                var story3Graph = generateStory3Graph(VMFoundFiles);
                Graphs.Add(story3Graph);
            }
            if (story6)
            {
                var story6Graph = generateStory6Graph(VMFoundFiles);
                Graphs.Add(story6Graph);
            }

            foreach (var graph in Graphs)
            {
                foreach (var node in graph.Nodes)
                {
                    if (!resultGraph.Nodes.Where(n=> n.Id == node.Id).Any())
                    {
                        resultGraph.AddNode(node);
                    }
                    
                }
                var edges = graph.Edges.ToList().AsReadOnly();
                foreach (var edge in edges)
                {
                    if (!resultGraph.Edges.Where(e => e.Target == edge.Target && e.Source == edge.Source).Any())
                    {
                        resultGraph.AddEdge(edge.Source, edge.LabelText, edge.Target);
                    }
                    
                }
            }

            return resultGraph;
    #endregion
        }

        #region Usings graph
        private Graph generateStory1Graph(List<string> VMFoundFiles)
        {
            //TODO: add other cases than usings: partial classes, interfaces, inheritance
            Graph resultGraph = new Graph("UsingsGraph");
            var usingsInFilesResult = new List<UsingDirectivesInFile>();
            var usingDirectiveFinder = new UsingDirectiveFinder();
            foreach (var path in VMFoundFiles)
            {
                var singleFileUsings = new UsingDirectivesInFile()
                {
                    InFile = this.splitPath(path),
                    UsingDirectives = usingDirectiveFinder.GetUsingDirecitvesInFileRoslyn(path),
                    fileSize = getFileSize(path)
                };
                usingsInFilesResult.Add(singleFileUsings);
            }

            foreach (var usingNode in usingsInFilesResult)
            {
                string fileNodeName = usingNode.InFile + "\n" + usingNode.fileSize;
                var fileNode = resultGraph.AddNode(fileNodeName);
                fileNode.Attr.FillColor = Color.DarkSeaGreen;
                fileNode.Attr.Shape = Shape.Octagon;

                foreach (var usingInFile in usingNode.UsingDirectives)
                {
                    string usingInFileNodeName = usingInFile.Name.ToString();
                    var node = resultGraph.FindNode(usingInFileNodeName);
                    if (node is null)
                    {
                        resultGraph.AddNode(usingInFileNodeName);
                    }
                    var createdUsingNode = resultGraph.FindNode(usingInFileNodeName);
                    createdUsingNode.Attr.Shape = Shape.Box;
                    createdUsingNode.Attr.FillColor = Color.Khaki;
                    var usingInFileEdge = resultGraph.AddEdge(fileNodeName, usingInFileNodeName);


                    var thatUsingCount = usingNode.UsingDirectives.Where(e => e.Name == usingInFile.Name).Count();
                    usingInFileEdge.LabelText = thatUsingCount.ToString();
                }
            }
            resultGraph.LayoutAlgorithmSettings.NodeSeparation = 10;
            resultGraph.Attr.OptimizeLabelPositions = false;
            return resultGraph;
        }
        #endregion

        #region Invocations in declaration region
        private Graph generateStory2Graph(List<string> VMFoundFiles, bool ignoreUnknownDeclaration)
        {
            Graph resultGraph = new Graph("MethodsGraph");
            var declarationFinder = new MethodDeclarationsFinder();
            var declarationsInFiles = new List<MethodDeclarationSyntax>();
            
            foreach (var path in VMFoundFiles)
            {
                var foundDeclarations = declarationFinder.GetMethodsDecalarationsInFIle(path);
                declarationsInFiles.AddRange(foundDeclarations);
            }

            var knownDeclarationsNames = declarationsInFiles.Select(e => e.Identifier.Text).ToList();

            List<MethodInvocationsInDeclaration> InvocationsInDeclarations = new List<MethodInvocationsInDeclaration>();
            var invocationFinder = new MethodInvocationsFinder();
            
            foreach (var declaration in declarationsInFiles)
            {
                List<string> resultInvocations = new List<string>();
                var invocationsInDeclaration = invocationFinder.GetMethodsInvocationsDeclaration(declaration);
                if (ignoreUnknownDeclaration)
                {
                    foreach (var invocation in invocationsInDeclaration)
                    {
                        var expr = invocation.Expression;
                        if (expr is IdentifierNameSyntax)
                        {
                            IdentifierNameSyntax identifierName = expr as IdentifierNameSyntax;
                            if (knownDeclarationsNames.Contains(identifierName.Identifier.Text))
                            {
                                resultInvocations.Add(identifierName.Identifier.Text);
                            }

                        }

                        if (expr is MemberAccessExpressionSyntax)
                        {
                            MemberAccessExpressionSyntax memberAccessExpressionSyntax = expr as MemberAccessExpressionSyntax;
                            if (knownDeclarationsNames.Contains(memberAccessExpressionSyntax.Name.Identifier.Text))
                            {
                                resultInvocations.Add(/*invocation.Expression.*/"MemberAccessExpressionSyntax");
                            }
                        }
                    }

                    MethodInvocationsInDeclaration semiResult = new MethodInvocationsInDeclaration()
                    {
                        MethodInvocations = invocationsInDeclaration,
                        InDeclaration = declaration
                    };

                    InvocationsInDeclarations.Add(semiResult);
                }

            }

            foreach (MethodInvocationsInDeclaration invocationsInDeclaration in InvocationsInDeclarations)
            {
                var declarationNode = resultGraph.AddNode(getMethodDeclarationName(invocationsInDeclaration.InDeclaration));
                declarationNode.Attr.FillColor = Color.CadetBlue;
                declarationNode.Attr.Shape = Shape.Ellipse;
                foreach (var invocation in invocationsInDeclaration.MethodInvocations)
                {
                    var invocationName = getInvocationName(invocation);
                    var invocationNode = resultGraph.AddNode(invocationName);
                    invocationNode.Attr.Shape = Shape.Ellipse;
                    invocationNode.Attr.FillColor = Color.AliceBlue;
                    var invocationInDeclarationEdge = resultGraph.AddEdge(declarationNode.LabelText, invocationName);
                    var thisInvocationsCount = invocationsInDeclaration.MethodInvocations.Where(e => e.Expression.ToString() == invocation.Expression.ToString()).Count();
                    invocationInDeclarationEdge.LabelText = thisInvocationsCount.ToString();
                }
            }

            return resultGraph;
        }
        #endregion

        #region MEthods in physical files graph
        private Graph generateStory6Graph(List<string> VMFoundFiles)
        {
            Graph resultGraph = new Graph("MethodsInPhysicalFilesGraph");
            var declarationFinder = new MethodDeclarationsFinder();

            foreach (var path in VMFoundFiles)
            {
                var foundDeclarations = declarationFinder.GetMethodsDecalarationsInFIle(path);
                var fileNode = resultGraph.AddNode(getFileName(path));
                fileNode.Attr.Shape = Shape.Trapezium;
                fileNode.Attr.FillColor = Color.AliceBlue;
                foreach (var declaration in foundDeclarations)
                {
                    //TODO: Implement for new nodes with same name as previous. i.e: two functions with same identifier with two different files:
                    var declarationNode = resultGraph.AddNode("." + getMethodDeclarationName(declaration));
                    var fileToMethodEdge = resultGraph.AddEdge(fileNode.LabelText, declarationNode.LabelText);
                }
            }

            return resultGraph;
        }
        #endregion


        private Graph generateStory3Graph(List<string> VMFoundFiles)
        {
            Graph resultGraph = new Graph("MethodsNamespacesGraph");
            var declarationFinder = new MethodDeclarationsFinder();
            var methodsInAllFiles = new List<MethodDeclarationSyntax>();
            var methodNamespaceGetter = new MethodNamespaceFinder();

            //TODO: implementation for same namespace in multiple files: change dictionary!
            var methodsNamespaceDictionary = new Dictionary<NamespaceDeclarationSyntax, List<MethodDeclarationSyntax>>();

            foreach (var path in VMFoundFiles)
            {
                var foundDeclarations = declarationFinder.GetMethodsDecalarationsInFIle(path);
                methodsInAllFiles.AddRange(foundDeclarations);
            }

            foreach (var methodDeclaration in methodsInAllFiles)
            {
                var Namespace = methodNamespaceGetter.GetNamespaceOfMethodDeclaration(methodDeclaration);

                if (methodsNamespaceDictionary.ContainsKey(Namespace))
                {
                    methodsNamespaceDictionary[Namespace].Add(methodDeclaration);
                }
                else
                {
                    methodsNamespaceDictionary.Add(Namespace, new List<MethodDeclarationSyntax>() { methodDeclaration });
                }
            }

            foreach (var _namespace in methodsNamespaceDictionary.Keys)
            {
                var namespaceNode = resultGraph.AddNode("Namespace:" + getNamespaceName(_namespace));
                namespaceNode.Attr.Shape = Shape.Octagon;
                namespaceNode.Attr.FillColor = Color.AliceBlue;
                foreach (var method in methodsNamespaceDictionary[_namespace])
                {
                    var methodNode = resultGraph.AddNode("DiN:" + getMethodDeclarationName(method));
                    var namespaceMethodEdge = resultGraph.AddEdge(namespaceNode.LabelText, methodNode.LabelText);
                }
            }

            return resultGraph;
        }

        

        private string getFileName(string path)
        {
            var fileName = path.Split('\\').Last();
            return "\\..\\" + fileName;
        }

        private string getNamespaceName(NamespaceDeclarationSyntax _namespace)
        {
            var name = _namespace.Name.ToString();
            return name;
        }

        private string getMethodDeclarationName(MethodDeclarationSyntax methodDeclaration)
        {
            var name = methodDeclaration.Identifier.Text + "(){}";
            return name;
        }

        private string getInvocationName(InvocationExpressionSyntax _invocation)
        {
            var expression = _invocation.Expression;
            string result = expression.ToString() + "()";

            return result;
        }

        private string getFileSize(string path)
        {
            var lines = 0;
            var characters = File.ReadAllLines(path).Sum(s => s.Length);
            using (var reader = File.OpenText(path))
            {
                while (reader.ReadLine() != null)
                {
                    lines++;
                }
            }

            string result = $"{lines.ToString()} LOC\n({characters.ToString()})";
            return result;
        }


        private string splitPath(string pathToSplit)
        {
            string result = string.Empty;
            char[] pathSeparator = { '\\' };
            var splits = pathToSplit.Split(pathSeparator);
            if (splits.Last() == "Program.cs")
            {
                result = splits[splits.Length - 2] + "\\" + splits[splits.Length - 1];
                return result;
            }
            else
            {
                return splits.Last();
            }
        }
    }
}