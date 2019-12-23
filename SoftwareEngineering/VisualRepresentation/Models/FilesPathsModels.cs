using FilesFinder;
using System.Collections.Generic;

namespace VisualRepresentation.Models
{
    class FilesPathsModels
    {
        public string PathToFolder { get; set; }

        public FilesPathsModels(string folderPath)
        {
            this.PathToFolder = folderPath;
        }
        public  List<string> GetFiles()
        {
            var finder = new NetFilesFinder(PathToFolder);
            List<string> result = new List<string>();
            result = finder.GetCsFilesFromFolder();
            return result;
        }
    }
}
