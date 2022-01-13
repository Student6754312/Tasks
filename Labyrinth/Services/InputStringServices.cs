using System;

namespace Labyrinth.Services
{
    public class InputStringServices
    {
        /// <summary>
        /// Returns string from user
        /// </summary>
        /// <returns></returns>
        public string? GetStringFromUser()
        {
            return Console.ReadLine();
        }
    }
}