using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            // D:\Repositories\RecursiveSearchEngine
            string path = @"D:\Repositories\RecursiveSearchEngine";
            FilesFinder netFinder = new FilesFinder(path);
            var foundNetFiles = netFinder.FindFiles();
        }
    }
}
