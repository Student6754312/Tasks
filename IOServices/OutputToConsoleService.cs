using System;

namespace IOServices 
{
    public class OutputToConsoleService :IOutputService
    {
        public void ConsoleOutputLine(string str)
        {
             Console.WriteLine(str);
        }

        public void ConsoleOutput(string str)
        {
            Console.Write(str);
        }

        public void Output(string str)
        {
            Console.WriteLine(str);
        }
    }
}
