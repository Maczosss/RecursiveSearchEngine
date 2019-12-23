using DependenceFinderAndPlotter;
using DependenceFinderAndPlotter.Nodes;
using Microsoft.Msagl.Drawing;
using System.Collections.Generic;
using System.Linq;


namespace VisualRepresentation.Models
{
    class GraphModels
    {

        public Graph GenerateGraph(bool story1, bool story2, bool story3, List<string> VMFoundFiles)
        {
            var story1Graph = generateStory1Graph(VMFoundFiles);
            return story1Graph;
        }

        //Usings graph
        private Graph generateStory1Graph(List<string> VMFoundFiles)
        {
            Graph resultGraph = new Graph("UsingsGraph");
            var usingsInFilesResult = new List<UsingDirectivesInFile>();
            var usingDirectiveFinder = new UsingDirectiveFinder();
            char[] pathSeparator = { '\\' };
            foreach (var path in VMFoundFiles)
            {
                var singleFileUsings = new UsingDirectivesInFile()
                {
                    InFile = path.Split(pathSeparator).Last(),
                    UsingDirectives = usingDirectiveFinder.GetUsingDirecitvesInFileRoslyn(path)
                };
                usingsInFilesResult.Add(singleFileUsings);
            }

            foreach (var usingNode in usingsInFilesResult)
            {
                resultGraph.AddNode(usingNode.InFile).Attr.FillColor = Color.AliceBlue;
                resultGraph.FindNode(usingNode.InFile).Attr.Shape = Shape.Diamond;
                foreach (var usingInFile in usingNode.UsingDirectives)
                {
                    var node = resultGraph.FindNode(usingInFile.Name.ToString());
                    if (node is null)
                    {
                        resultGraph.AddNode(usingInFile.Name.ToString());
                        
                    }
                    var createdNode = resultGraph.FindNode(usingInFile.Name.ToString());
                    resultGraph.AddEdge(usingNode.InFile, usingInFile.Name.ToString());
                }
            }
            resultGraph.LayoutAlgorithmSettings.NodeSeparation = 10;
            return resultGraph;
        }
    }
}