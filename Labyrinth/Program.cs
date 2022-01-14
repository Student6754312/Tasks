using System;
using System.Collections.Generic;
using Labyrinth.Domain;
using Labyrinth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Labyrinth
{
    class Program
    {
        static void Main(string[] args)
        {

            IOutputStringService outputService = null!;

            try
            {
                //Setup DI
                var serviceProvider = new ServiceCollection()
                          .AddSingleton<IInputStringService, InputStringService>()
                          .AddSingleton<ILabyrinthService, LabyrinthService>()
                          .AddSingleton<IOutputStringService, OutputStringService>()
                          .BuildServiceProvider();

                outputService = serviceProvider.GetService<IOutputStringService>()!;

                int l, r, c;
                var labyrinths = new List<ILabyrinth>();

                var labyrinthService = serviceProvider.GetService<ILabyrinthService>();
                var inputStringService = serviceProvider.GetService<IInputStringService>();


                while (true)
                {
                    outputService.ConsoleOutuptLine("L R C");
                    string? inputString = inputStringService!.GetStringFromUser();

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
                        outputService.ConsoleOutupt(Environment.NewLine);
                        break;
                    }

                    var labyrinth = new Domain.Labyrinth(l, r, c);
                    
                    labyrinthService!.CreateLabyrinth(labyrinth);

                    labyrinths.Add(labyrinth);
                    outputService.ConsoleOutupt(Environment.NewLine);
                }

                if (labyrinths.Count == 0)
                {
                    outputService.ConsoleOutuptLine("Exit!");
                    return;
                }

                outputService.ConsoleOutuptLine("Ausgabe:\n");

                foreach (var labyrinth in labyrinths)
                {
                    labyrinthService!.BreadthFirstSearch(labyrinth);

                    int minTime = labyrinthService.FindShortestPath(labyrinth);

                    if (minTime == Int32.MaxValue)
                    {
                        outputService.ConsoleOutuptLine("Gefangen :-(\n");
                    }
                    else
                    {
                        outputService.ConsoleOutuptLine($"Entkommen in {minTime} Minute(n)!)\n");
                    }
                }

            }
            catch (FormatException ex)
            {
                if (outputService != null) outputService.ConsoleOutuptLine($"\nInput Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                if (outputService != null) outputService.ConsoleOutuptLine($"\nUnexpected Error: {ex.Message}");
            }
            finally
            {
                if (outputService != null) outputService.ConsoleOutuptLine("\nExit!");
            }
        }

    }
}
