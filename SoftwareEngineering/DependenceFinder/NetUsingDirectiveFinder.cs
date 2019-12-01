using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DependenceFinder
{
    class NetUsingDirectiveFinder
    {
        public List<string> CsFiles { get; set; }

        public NetUsingDirectiveFinder(List<string> csFinder)
        {
            this.CsFiles = csFinder;
        }

        public List<UsingDirectiveInFile> findUsingDefinitons()
        {
            List<UsingDirectiveInFile> usingDirectivesInFile = new List<UsingDirectiveInFile>();

            foreach (var path in CsFiles)
            {
                UsingDirectiveInFile usingDirectiveFoundInThisFile = new UsingDirectiveInFile();
                usingDirectiveFoundInThisFile.InFile = path;

                //TODO: find matches after dot i.e. System. ....
                Regex matchForUsingDirective = new Regex(@"[\s*]{0,}using[^\(][\s*]{0,}(?<directive>\w*)");

                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null && !line.Trim().StartsWith("//"))
                    {
                        Match checkForUsingDirectiveInThisLine = matchForUsingDirective.Match(line);
                        if (checkForUsingDirectiveInThisLine.Success)
                        {
                            string foundUsingDirective = checkForUsingDirectiveInThisLine.Groups["directive"].Value.Trim();
                            usingDirectiveFoundInThisFile.UsingDirectives.Add(foundUsingDirective);
                            usingDirectivesInFile.Add(usingDirectiveFoundInThisFile);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return UsingDirectiveInFile;
        }

        private List<string> findUsedUsingDirectives();


    }
}
