using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenceFinder
{
    class ClassesUsagesFoundInFile
    {
        public string ClassName { get; set; }
        public List<string> WasUsedInFiles { get; set; }

        public ClassesUsagesFoundInFile()
        {
            WasUsedInFiles = new List<string>();
        }
    }
}
