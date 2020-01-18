using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VisualRepresentation.Models;

namespace Tests.NetFilesFinder
{
    [TestFixture]
    class WhenUserSearchesForFilesInValidFolder
    {
        [Test]
        public void ThenProperAmountOfFilesIsReturned()
        {
            var path = Assembly.GetExecutingAssembly().Location;
            string resourcesPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\")) + "ResourcesFolder";

            FilesPathsModels pathModel = new FilesPathsModels(resourcesPath);
            var testFiles = pathModel.GetFilesInCurrentDirectoryOnly().Count;

            Assert.AreEqual(2, testFiles);
        }
    }
}
