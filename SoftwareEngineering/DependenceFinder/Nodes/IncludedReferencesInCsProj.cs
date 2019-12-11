using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenceFinder.Nodes
{
    class IncludedReferencesInCsProj
    {
        public List<string> IncludedReferences { get; set; }
        public string InFile { get; set; }


        public IncludedReferencesInCsProj()
        {

        }
    }
}
