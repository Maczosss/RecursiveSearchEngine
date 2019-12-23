using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FilesFinder
{
    public class NetFilesFinder
    {
        //TODO: Add strategies for different types of languages. i.e. ex. .net, Java.

        public string TopMostDirectory { get; set; }
        private List<string> foundCsFiles { get; set; }

        public NetFilesFinder()
        {
            this.foundCsFiles = new List<string>();
        }

        public NetFilesFinder(string topMostDirectory)
        {
            foundCsFiles = new List<string>();
            this.TopMostDirectory = topMostDirectory;
        }

        /// <summary>
        /// Finds files with proper extension recursively, searching downward from provided location.
        /// </summary>
        /// <returns></returns>
        public List<string> GetCsFilesFromFolder()
        {
            foundCsFiles.Clear();
            List<string> result = new List<string>();
            result.AddRange(Directory.GetFiles(TopMostDirectory, "*.cs", SearchOption.AllDirectories));
            foundCsFiles.AddRange(result);

            //removeTestFolders();

            if (HasFolderAnyCsFiles())
            {
                return foundCsFiles;
            }
            else
            {
                List<string> emptyResult = new List<string>();
                emptyResult.Add("Folder contains no .cs files. \nChoose different folder");
                return emptyResult;
            }
        }

        public List<string> FindCsProjFiles()
        {
            List<string> result = new List<string>();
            result.AddRange(Directory.GetFiles(TopMostDirectory, "*.csproj", SearchOption.AllDirectories));
            this.foundCsFiles = result;
            return result;
        }

        public bool HasFolderAnyCsFiles()
        {
            var result = this.foundCsFiles.Any() ? true : false;
            return result;
        }

        //private void removeTestFolders()
        //{
        //    this.foundCsFiles = this.foundCsFiles.Where(e => e.Contains(@"\Tests\")).ToList();
        //}
    }

    public enum ProjectType
    {
        DotNet = 1,
        Java = 2,
    }
}
