using System;

namespace Labyrinth.Services
{
    public class InputStringService : IInputStringService
    {
        public string? GetStringFromUser()
        {
            return Console.ReadLine();
        }
    }
}