using System;
using System.Collections.Generic;

namespace Labyrinth
{
    class Program
    {
        static void Main()
        {
            try
            {
                int l, r, c;
                var labyrinths = new List<Labyrinth>();
               
                while(true)
                {
                    Console.WriteLine("L R C");
                    string? inputString = Console.ReadLine();
                    var parameters = inputString!.Split(' ');

                    if (parameters.Length == 3)
                    {
                        l = Convert.ToInt32(parameters[0]);
                        r = Convert.ToInt32(parameters[1]);
                        c = Convert.ToInt32(parameters[2]);
                    }
                    else
                    {
                        throw new FormatException("Wrong Labyrinth Parameters");
                    }

                    if (l == 0 && r == 0 && c == 0)
                    {
                        Console.WriteLine();
                        break;
                    }
                    
                    var labyrinth = new Labyrinth(l, r, c);
                    labyrinth.CreateLabyrinth();
                    labyrinths.Add(labyrinth);
                    Console.WriteLine();
                }

                if (labyrinths.Count == 0)
                {
                    Console.WriteLine("Exit!");
                    return;
                }

                Console.WriteLine("Ausgabe:\n");
                
                foreach (var labyrinth in labyrinths)
                {
                    labyrinth.BreadthFirstSearch();
                    
                    int minTime = labyrinth.FindShortestPath();
                    
                    if ( minTime == Int32.MaxValue)
                    {
                        Console.WriteLine("Gefangen :-(\n");
                    }
                    else
                    {
                        Console.WriteLine($"Entkommen in {minTime} Minute(n)!)\n");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("Exit!");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Input Error: {ex.Message}" );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }
        
    }
}
