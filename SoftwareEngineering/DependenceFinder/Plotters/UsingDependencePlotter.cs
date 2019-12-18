using DependenceFinderAndPlotter.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenceFinder.Plotters
{
    class UsingDependencePlotter
    {
        public List<string> CsFiles { get; set; }
        public List<UsingDirectivesInFile> UsingInFiles { get; set; }


    }
}
