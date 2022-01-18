using System;
using IOServices.Interfaces;

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