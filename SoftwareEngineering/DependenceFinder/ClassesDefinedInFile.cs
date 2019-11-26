using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenceFinder
{
    public class ClassesDefinedInFile
    {
        public List<string> DefinedClassesNames { get; set; }
        public string InFile { get; set; }
        //TODO: add logic to treat partial classes only on first occurance in specific file.
        public ClassesDefinedInFile()
        {
            DefinedClassesNames = new List<string>();
        }
    }
}
