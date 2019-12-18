using FilesFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (finder.HasFolderAnyCsFiles()) 
            {
                result.AddRange(finder.FindCsFiles());
            } else 
            {
                result.Add("Selected folder contains no *.cs files.");
            }
            return result;
        }
        
    }
}
