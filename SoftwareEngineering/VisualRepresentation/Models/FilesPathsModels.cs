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

        public List<string> GetFiles(bool ignoreTestsFolders = false)
        {
            foundCsFiles.Clear();
            List<string> result = new List<string>();
            result.AddRange(Directory.GetFiles(PathToFolder, "*.cs", SearchOption.AllDirectories));

            if (ignoreTestsFolders)
            {
                this.removeTestFolders(result);
            }

            foundCsFiles.AddRange(result);

            return foundCsFiles;
        }

        private void removeTestFolders(List<string> paths)
        {
            paths.RemoveAll(p => p.Contains("Test"));
        }

        public bool HasFolderAnyCsFiles()
        {
            var result = this.foundCsFiles.Any() ? true : false;
            return result;
        }
    }
}
