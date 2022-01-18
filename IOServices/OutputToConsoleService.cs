using System;
using IOServices.Interfaces;

namespace IOServices 
{
    public class OutputToConsoleService :IOutputService
    {
        public void Output(string str)
        {
            Console.WriteLine(str);
        }
    }
}
