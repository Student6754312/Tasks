using System;
using IOServices.Base;

namespace IOServices
{
    public class InputFromConsoleService : IInputService
    {
        public string? Input()
        {
            return Console.ReadLine();
        }
    }
}