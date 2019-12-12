using System.Collections.Generic;
using System.IO;

namespace FilesFinder
{
    public class NetFilesFinder
    {
        //TODO: Add strategies for different types of languages. i.e. ex. .net, Java.

        public string TopMostDirectory { get; set; } = @"D:\Repositories\RecursiveSearchEngine";

        public NetFilesFinder()
        {
            
        }

        public NetFilesFinder(string topMostDirectory)
        {
            this.TopMostDirectory = topMostDirectory;
        }

        /// <summary>
        /// Findes files with proper extension recursively, searching downward from provided location.
        /// </summary>
        /// <returns></returns>
        public List<string> FindCsFiles()
        {
            List<string> result = new List<string>();
            result.AddRange(Directory.GetFiles(TopMostDirectory, "*.cs", SearchOption.AllDirectories));
            return result;
        }

        public List<string> FindCsProjFiles()
        {
            List<string> result = new List<string>();
            result.AddRange(Directory.GetFiles(TopMostDirectory, "*.csproj", SearchOption.AllDirectories));
            return result;
        }
    }

    public enum ProjectType
    {
        DotNet = 1,
        Java = 2,
    }
}
