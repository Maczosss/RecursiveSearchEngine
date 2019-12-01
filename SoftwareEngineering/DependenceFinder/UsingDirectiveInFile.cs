using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenceFinder
{
    class UsingDirectiveInFile
    {
        public List<string> UsingDirectives { get; set; }
        public string InFile { get; set; }

        public UsingDirectiveInFile()
        {
            UsingDirectives = new List<string>();
        }
    }
}
