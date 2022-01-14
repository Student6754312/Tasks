using System;

namespace Labyrinth.Services
{
    public class InputService : IInputService
    {
        public string? GetStringFromUser()
        {
            return Console.ReadLine()?.Trim();
        }
    }
}