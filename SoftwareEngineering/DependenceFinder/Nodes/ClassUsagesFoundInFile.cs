using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenceFinder.Nodes
{
    class ClassUsagesFoundInFile
    {
        public string ClassName { get; set; }
        public string WasUsedInFile { get; set; }
        public int ThatManyTimes { get; set; }

        public ClassUsagesFoundInFile()
        {
            
        }
    }
}
