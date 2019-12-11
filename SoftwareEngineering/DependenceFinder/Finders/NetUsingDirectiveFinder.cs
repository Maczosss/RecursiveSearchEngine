using DependenceFinder.Nodes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        //public List<string> CsFiles { get; set; }

        //public NetUsingDirectiveFinder(List<string> csFilesToSearchIn)
        //{
        //    this.CsFiles = csFilesToSearchIn;
        //}

        //public List<UsingDirectivesInFile> findUsingDefinitonsInSource()
        //{
        //    var a = this.GetType().GetMethods();
        //    List<UsingDirectivesInFile> result = new List<UsingDirectivesInFile>();

        //    foreach (var path in CsFiles)
        //    {
        //        UsingDirectivesInFile usingDirectiveFoundInThisFile = new UsingDirectivesInFile();
        //        usingDirectiveFoundInThisFile.InFile = path;

        //        Regex matchForUsingDirective = new Regex(@"using[\s*{0,}](?<directive>[^\(][^\s]+(?=;))");

        //        using (StreamReader reader = new StreamReader(path))
        //        {
        //            string line;
        //            while ((line = reader.ReadLine()) != null)
        //            {
        //                Match checkForUsingDirectiveInThisLine = matchForUsingDirective.Match(line);
        //                if (checkForUsingDirectiveInThisLine.Success && !line.Trim().StartsWith("//"))
        //                {
        //                    string foundUsingDirective = checkForUsingDirectiveInThisLine.Groups["directive"].Value.Trim();
        //                    usingDirectiveFoundInThisFile.UsingDirectives.Add(foundUsingDirective);
                            
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //            }
        //            result.Add(usingDirectiveFoundInThisFile);
        //        }
        //    }
        //    return result;
        //}

        public List<UsingDirectiveSyntax> findUsingDirecitvesInFileRoslyn(string csFilePath)
        {
            var result = new List<UsingDirectiveSyntax>();

            var file = File.ReadAllText(csFilePath);
            var fileSyntaxTree = CSharpSyntaxTree.ParseText(csFilePath);
            //CompilationUnitSyntax compilationUnitSyntax = fileSyntaxTree.GetCompilationUnitRoot();
            var root = fileSyntaxTree.GetRoot();
            
            var usingDirectives = root.DescendantNodesAndSelf().OfType<UsingDirectiveSyntax>();
            foreach (var @using in usingDirectives)
            {
                result.Add(@using);
            }

            return result;
        }
    }
}
