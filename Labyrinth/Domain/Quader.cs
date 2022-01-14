using System;

namespace Labyrinth.Domain
{
     public class Quader : IQuader
    {
        public char Type { get; }
        public int Value { get; set; }
        public QuaderLocation Location { get; }

        public Quader(char type, QuaderLocation location)
        {
            Type = type;
            Value = type switch
            {
                '#' => (int)QuaderTypes.Stone,
                '.' => (int)QuaderTypes.Air,
                'S' => (int)QuaderTypes.Start,
                'E' => (int)QuaderTypes.Exit,
                _ => throw new FormatException($"Incorrect Quader Symbol - '{type}'")
            };
            Location = location;
        }
    }
}
