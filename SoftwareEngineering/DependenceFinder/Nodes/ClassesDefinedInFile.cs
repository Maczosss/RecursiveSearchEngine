using System.Collections.Generic;

namespace DependenceFinderAndPlotter.Nodes
{

    public class ClassesDefinedInFile 
    {
        public List<string> DefinedClassesNames { get; set; }
        public string InFile { get; set; }

        public ClassesDefinedInFile()
        {
            DefinedClassesNames = new List<string>();
        }
    }
}
