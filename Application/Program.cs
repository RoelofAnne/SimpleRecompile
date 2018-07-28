using System;
using Repro;
//Using Tag

namespace Application
{
	public class AppRe
    {
        public static void Main(string[] args)
        {
			var CleanProgramString = Reprogrammer.DecompileCode("Application.exe");
			string UserName = "";
			string ComputerName = "";

            if(string.IsNullOrEmpty(UserName))
			{
				Console.WriteLine("Hi, what is your name?");
				UserName = Console.ReadLine();
				CleanProgramString = CleanProgramString.Replace("string text2 = \"\";", "string text2 = \"" + UserName + "\";");
				Console.WriteLine(Reprogrammer.CompileExecutable("Application", CleanProgramString));
				return;//TODO restart
			}
			else if (string.IsNullOrEmpty(ComputerName))
			{
				Console.WriteLine("Hi {0}, how would you like to call me?",UserName);
				ComputerName = Console.ReadLine();                               
				ComputerName = "\\\"" + ComputerName + "\\\"";
				CleanProgramString = CleanProgramString.Replace("string empty = string.Empty;", "string empty = \"" + ComputerName + "\";");
				CleanProgramString = CleanProgramString.Replace("\\\"\";\"", "\"");
				CleanProgramString = CleanProgramString.Replace("\"\\\"","\"");
				CleanProgramString = CleanProgramString.Replace("Replace(\"string empty = \"", "Replace(\"string empty = \\\"");
				CleanProgramString = CleanProgramString.Replace("\\\"\";", "\";");
				//Console.WriteLine(CleanProgramString);
				Console.WriteLine(Reprogrammer.CompileExecutable("Application", CleanProgramString));
                return;//TODO restart
			}

			Console.WriteLine("Hi {0}. What can I, {1}, do for you today?", UserName, ComputerName);  
        }
    }
}
