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
        public string WasUsedInFile { get; set; }
        public int ThatManyTimes { get; set; }

        public ClassesUsagesFoundInFile()
        {
            
        }
    }
}
