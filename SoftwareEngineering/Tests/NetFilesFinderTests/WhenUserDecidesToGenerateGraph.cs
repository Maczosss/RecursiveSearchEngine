using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VisualRepresentation.Models;

namespace Tests.GraphsTests
{
    [TestFixture]
    class WhenUserDecidesToGenerateGraph
    {
        [TestCase("\\TestFile1.cs", true, false, false, false, 5)]
        [TestCase("\\TestFile2.cs", true, false, false, false, 5)]
        public void ThenProperAmmountOfNodesIsGeneratedForGraph(string TestFileName, bool story1, bool story2, bool story3, bool story6, int expectedNodesCount)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            string testFilePath = Path.GetFullPath(Path.Combine(path, @"..\..\..\")) + "ResourcesFolder" + TestFileName;

            GraphModels graphGenerator = new GraphModels();
            var pathToTestFile = new List<string>();
            pathToTestFile.Add(testFilePath);
            var graph = graphGenerator.GenerateGraph(story1, story2, story3, story6, pathToTestFile);
            var graphNodesCount = graph.NodeCount;

            Assert.AreEqual(expectedNodesCount, graphNodesCount);
        }

        [TestCase("\\TestFile01.cs", true, false, false, false, 0)]
        public void ThenProperAmmountOfEdgesIsGeneratedForGraph(string TestFileName, bool story1, bool story2, bool story3, bool story6, int expectedEdgesCount)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            string testFilePath = Path.GetFullPath(Path.Combine(path, @"..\..\..\")) + "ResourcesFolder" + TestFileName;

            GraphModels graphGenerator = new GraphModels();
            var pathToTestFile = new List<string>();
            pathToTestFile.Add(testFilePath);
            var graph = graphGenerator.GenerateGraph(story1, story2, story3, story6, pathToTestFile);
            var graphEdgesCount = graph.Edges.Count();

            Assert.AreEqual(expectedEdgesCount, graphEdgesCount);
        }
    }
}
