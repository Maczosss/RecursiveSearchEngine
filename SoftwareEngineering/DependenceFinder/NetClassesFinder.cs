using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DependenceFinder
{
    class NetClassesFinder
    {
        public List<string> CsFiles { get; set; }
        public NetClassesFinder(List<string> csFiles)
        {
            this.CsFiles = csFiles;
        }

        public List<ClassesDefinedInFile> findDefinitions()
        {
            List<ClassesDefinedInFile> definitionsInFile = new List<ClassesDefinedInFile>();
            foreach (var path in CsFiles)
            {
                ClassesDefinedInFile classesDefinitionsFoundInThisFile = new ClassesDefinedInFile();
                classesDefinitionsFoundInThisFile.InFile = path;

                Regex matchForClass = new Regex(@"(?<partialDefinition>\w*)\s*class[\s*]{1,}(?<className>\w*)\s*");

                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Match checkForClassInLine = matchForClass.Match(line);
                        if (checkForClassInLine.Success)
                        {
                            string foundClassName = checkForClassInLine.Groups["className"].Value;
                            classesDefinitionsFoundInThisFile.DefinedClassesNames.Add(foundClassName);
                            definitionsInFile.Add(classesDefinitionsFoundInThisFile);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

            }
            return definitionsInFile;
        }

        public List<ClassesUsagesFoundInFile> findClassesUsages(List<ClassesDefinedInFile> classNamesToSearchFor)
        {
            foreach (var classDefinitionsInFile in classNamesToSearchFor)
            {
                foreach (var className in classDefinitionsInFile.DefinedClassesNames)
                {
                    //find that class name being used as a type in files different than classDefinitionsInFile.InFile
                }
            }
        }


	

    }
}
