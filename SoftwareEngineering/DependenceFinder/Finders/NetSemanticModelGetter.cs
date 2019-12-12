//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using System;
//using System.IO;
//using System.Linq;

//namespace DependenceFinder.Finders
//{
//    class NetSemanticModelGetter
//    {
//        public int GetSemanticModelFromFile(string csFilePath)
//        {
//            var file = File.ReadAllText(csFilePath);
//            var tree = CSharpSyntaxTree.ParseText(file);


//            var root = (CompilationUnitSyntax)tree.GetRoot();
//            var compilation = CSharpCompilation.Create("MyCompilation")
//                              .AddReferences(
//                                 MetadataReference.CreateFromFile(
//                                   typeof(object).Assembly.Location))
//                              .AddSyntaxTrees(tree);
//            var model = compilation.GetSemanticModel(tree);

//            var myTypeSyntax = root.DescendantNodes().OfType<TypeDeclarationSyntax>().First();
//            var myTypeInfo = model.GetDeclaredSymbol(myTypeSyntax);
//            Console.WriteLine(myTypeInfo);

//            return 1;
//        }
//    }
//}
