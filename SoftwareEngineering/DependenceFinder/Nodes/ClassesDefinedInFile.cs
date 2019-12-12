using System.Collections.Generic;

namespace DependenceFinder.Nodes
{

    public class ClassesDefinedInFile //: IDependable
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
