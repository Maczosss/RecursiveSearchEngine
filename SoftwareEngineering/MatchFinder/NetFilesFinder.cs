using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualRepresentation;

namespace FilesFinder
{
    public class NetFilesFinder
    {
        //TODO: Add strategies for different types of languages. i.e. ex. .net, Java.

        public string TopMostDirectory { get; set; } = @"C:\Users\Michał\Documents\GitHub\RecursiveSearchEngine";

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
        public List<string> FindFiles()
        {
            List<string> result = new List<string>();
            result.AddRange(Directory.GetFiles(TopMostDirectory, "*.cs", SearchOption.AllDirectories));
            return result;
        }
    }

    public enum ProjectType
    {
        DotNet = 1,
        Java = 2,
    }
}
