using System;

namespace Labyrinth.Domain
{
     public class Quader : IQuader
    {
        public char View { get; }
        public QuaderTypes Type { get; set; }
        public int Value { get; set; }
        public QuaderLocation Location { get; }

        public Quader(char view, QuaderLocation location)
        {
            View = view;
            Type = view switch
            {
                '#' => QuaderTypes.Stone,
                '.' => QuaderTypes.Air,
                'S' => QuaderTypes.Start,
                'E' => QuaderTypes.Exit,
                _ => throw new FormatException($"Incorrect Quader Symbol - '{view}'")
            };
            Value = (int)Type;
            Location = location;
        }
    }
}
