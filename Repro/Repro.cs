using System;
using System.CodeDom.Compiler;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;

namespace Repro
{
	public static class Reprogrammer
    {
        public static bool CompileExecutable(string filename, String sourceCode)//sourceName)
        {
            CodeDomProvider provider = null;
            bool compileOk = false;

            provider = CodeDomProvider.CreateProvider("CSharp");

            if (provider != null)
            {
                String exeName = String.Format(@"{0}.exe", filename);

                CompilerParameters cp = new CompilerParameters(new[] { "System.dll", "Repro.dll" });

                // Generate an executable instead of 
                // a class library.
                cp.GenerateExecutable = true;

                // Specify the assembly file name to generate.
                cp.OutputAssembly = exeName;

                // Save the assembly as a physical file.
                cp.GenerateInMemory = false;

                // Set whether to treat all warnings as errors.
                cp.TreatWarningsAsErrors = false;

                CompilerResults cr = provider.CompileAssemblyFromSource(cp, sourceCode);

                if (cr.Errors.Count > 0)
                {
                    // Display compilation errors.
                    Console.WriteLine("Errors building {0} into {1}",
                                      filename, cr.PathToAssembly);
                    foreach (CompilerError ce in cr.Errors)
                    {
                        Console.WriteLine("  {0}", ce.ToString());
                        Console.WriteLine();
                    }
                }
                else
                {
                    // Display a successful compilation message.
                    Console.WriteLine("Source {0} built into {1} successfully.",
                                      filename, cr.PathToAssembly);
                }

                // Return the results of the compilation.
                if (cr.Errors.Count > 0)
                {
                    compileOk = false;
                }
                else
                {
                    compileOk = true;
                }
            }
            return compileOk;
        }

        public static string DecompileCode(string applicationName)
        {
			CSharpDecompiler decompiler = new CSharpDecompiler(applicationName, new DecompilerSettings() { ThrowOnAssemblyResolveErrors = false, RemoveDeadCode = true });
            return decompiler.DecompileWholeModuleAsString();
        }
    }
}
