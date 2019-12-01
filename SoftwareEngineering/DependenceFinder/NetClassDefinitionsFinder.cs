using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DependenceFinder
{
    class NetClassDefinitionsFinder
    {
        public List<string> CsFiles { get; set; }
        public NetClassDefinitionsFinder(List<string> csFiles)
        {
            this.CsFiles = csFiles;
        }

        public List<ClassesDefinedInFile> findClassDefinitions()
        {
            List<ClassesDefinedInFile> definitionsInFile = new List<ClassesDefinedInFile>();
            foreach (var path in CsFiles)
            {
                ClassesDefinedInFile classesDefinitionFoundInThisFile = new ClassesDefinedInFile();
                classesDefinitionFoundInThisFile.InFile = path;

                Regex matchForClass = new Regex(@"(?<partialDefinition>\w*)\s*class[\s*]{1,}(?<className>\w*)\s*");

                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null && !line.Trim().StartsWith("//"))
                    {
                        Match checkForClassInLine = matchForClass.Match(line);
                        if (checkForClassInLine.Success)
                        {
                            string foundClassName = checkForClassInLine.Groups["className"].Value.Trim();
                            classesDefinitionFoundInThisFile.DefinedClassesNames.Add(foundClassName);
                            definitionsInFile.Add(classesDefinitionFoundInThisFile);
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
        
        public List<ClassesUsagesFoundInFile> findClassesUsages(List<ClassesDefinedInFile> classDefinitionsToSearchFor, 
                                                                List<string> csFilesFoundInFolder)
        {
            var result = new List<ClassesUsagesFoundInFile>();
            foreach (var classDefinitionInFile in classDefinitionsToSearchFor)
            {
                foreach (var className in classDefinitionInFile.DefinedClassesNames)
                {
                    //find that class name being used as a type in files different than classDefinitionsInFile.InFile
                    var filesToCheck = csFilesFoundInFolder.Where(e => e != classDefinitionInFile.InFile).ToList();
                    string classNameUsedAsTypeOrCtorPattern = @"((\s*|\()" + className + @"[\s*]{1,})|" + className + "()";
                    Regex matchForClassBeingUsed = new Regex(classNameUsedAsTypeOrCtorPattern);

                    foreach (var path in filesToCheck)
                    {
                        int occurances = 0;
                        using (StreamReader reader = new StreamReader(path))
                        {
                            string line;
                            
                            while ((line = reader.ReadLine()) != null && !line.StartsWith("//"))
                            {
                                Match checkForClassBeingUsed = matchForClassBeingUsed.Match(line);
                                if (checkForClassBeingUsed.Success)
                                {
                                    //found class usage in file different than file with definition.
                                    occurances++;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                        }
                        if (occurances != 0)
                        {
                            result.Add(new ClassesUsagesFoundInFile() { ClassName = className, WasUsedInFile = path, ThatManyTimes = occurances });
                        }
                    }
                }
            }
            return result;
        }
    }
}
