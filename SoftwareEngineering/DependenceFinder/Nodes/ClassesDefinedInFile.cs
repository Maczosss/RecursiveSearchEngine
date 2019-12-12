using System.Collections.Generic;

namespace DependenceFinder.Nodes
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
