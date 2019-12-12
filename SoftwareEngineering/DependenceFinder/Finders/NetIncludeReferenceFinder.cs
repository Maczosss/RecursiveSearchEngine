using DependenceFinder.Nodes;
using System.Collections.Generic;
using System.Xml;

namespace DependenceFinder.Finders
{
    class NetIncludeReferenceFinder
    {
        public List<string> CsPRojFiles { get; set; }

        public NetIncludeReferenceFinder(List<string> csProjFiles)
        {
            this.CsPRojFiles = csProjFiles;
        }

        public List<UsingDirectivesInFile> findReferencesIncludedInCsProj()
        {
            var result = new List<UsingDirectivesInFile>();
            foreach (var file in CsPRojFiles)
            {
                XmlDocument csproj = new XmlDocument();
                csproj.Load(file);
                //csproj.LoadXml(file);
            }

            return result;
            
        }

    }
}
