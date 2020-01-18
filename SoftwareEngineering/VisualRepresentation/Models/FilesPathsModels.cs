using FilesFinder;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VisualRepresentation.Models
{
    public class FilesPathsModels
    {
        public string PathToFolder { get; set; }
        private List<string> foundCsFiles { get; set; }

        public FilesPathsModels(string folderPath)
        {
            this.PathToFolder = folderPath;
            this.foundCsFiles = new List<string>();
        }

        public List<string> GetFiles(bool ignoreTestsFolders = true)
        {
            foundCsFiles.Clear();
            List<string> result = new List<string>();
            result.AddRange(Directory.GetFiles(PathToFolder, "*.cs", SearchOption.AllDirectories));

            if (ignoreTestsFolders)
            {
                List<string> newResult = new List<string>();
                foreach (var path in result)
                {
                    if (path.Split('\\').Last() == "FilesPathsModels.cs")
                    {
                        continue;
                    }
                    var fileLines = File.ReadAllLines(path);
                    if (fileLines.Where(l => l.Contains("[TestFixture]") || l.Contains("[Test]")).Any())
                    {

                    }
                    else
                    {
                        newResult.Add(path);
                    }
                }
                result = newResult;
            }

            foundCsFiles.AddRange(result);

            return foundCsFiles;
        }

        public List<string> GetFilesInCurrentDirectoryOnly()
        {
            foundCsFiles.Clear();
            List<string> result = new List<string>();
            result.AddRange(Directory.GetFiles(PathToFolder, "*.cs", SearchOption.TopDirectoryOnly));

            foundCsFiles.AddRange(result);

            return foundCsFiles;
        }

        public bool HasFolderAnyCsFiles()
        {
            var result = this.foundCsFiles.Any() ? true : false;
            return result;
        }
    }
}
