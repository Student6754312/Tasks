using System;

namespace IOServices 
{
    public class OutputService :IOutputService
    {
        public void ConsoleOutputLine(string str)
        {
             Console.WriteLine(str);
        }

        public void ConsoleOutput(string str)
        {
            Console.Write(str);
        }
    }
}
