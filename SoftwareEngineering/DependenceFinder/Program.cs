using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilesFinder;

namespace DependenceFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            NetFilesFinder netFinder = new NetFilesFinder();
            var foundNetFiles = netFinder.FindFiles();

            NetClassesFinder netClassesFinder = new NetClassesFinder(foundNetFiles);
            var foundClassesDefinitionsInFiles = netClassesFinder.findDefinitions();


            Console.ReadLine();
        }
    }
}
