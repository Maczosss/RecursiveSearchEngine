using DependenceFinderAndPlotter.Nodes;
using System.Collections.Generic;
using System.Xml;

namespace DependenceFinderAndPlotter.Finders
{
    class IncludeReferenceFinder
    {
        public List<string> CsPRojFiles { get; set; }

        public IncludeReferenceFinder(List<string> csProjFiles)
        {
            this.CsPRojFiles = csProjFiles;
        }

        public List<UsingDirectivesInFile> GetReferencesIncludedInCsProj()
        {
            var result = new List<UsingDirectivesInFile>();
            foreach (var file in CsPRojFiles)
            {
                XmlDocument csproj = new XmlDocument();
                csproj.Load(file);
            }

            return result;
            
        }

    }
}

