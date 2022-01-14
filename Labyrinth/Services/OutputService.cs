using System;

namespace Labyrinth.Services 
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
