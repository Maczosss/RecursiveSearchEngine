using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchFinder
{
    public class FilesFinder
    {
        //Add strategies for different types of languages. i.e. ex. .net, Java.
        public string TopMostDirectory { get; set; }
        public FilesFinder(string topMostDirectory)
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
}
