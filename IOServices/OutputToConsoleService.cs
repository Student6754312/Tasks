using System;
using IOServices.Base;

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
