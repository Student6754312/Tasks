using System;

namespace Labyrinth.Services 
{
    public class OutputStringService :IOutputStringService
    {
        public void ConsoleOutuptLine(string str)
        {
             Console.WriteLine(str);
        }

        public void ConsoleOutupt(string str)
        {
            Console.Write(str);
        }
    }
}
