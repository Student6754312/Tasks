using System;
using System.Collections.Generic;
using Fibonacci;
using Fibonacci.Factory;
using IOServices;
using Labyrinth.Domain;
using Labyrinth.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Labyrinth
{
    class Program
    {
        static void Main(string[] args)
        {

            IOutputService? outputService = null;

            try
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                var configSection = configuration.GetSection("ApplicationSettings");
                
                //Setup DI
                var serviceProvider = new ServiceCollection()
                    .AddTransient<IInputService, InputFromConsoleService>()
                    .AddTransient<IInputSelectionFactory, InputSelectionFactory>()
                    .AddSingleton<IInputService, InputFromFileService>()
                    .AddSingleton<IOutputService, OutputService>()
                    .Configure<ApplicationSettings>(configSection)
                    .AddTransient<ILabyrinthService, LabyrinthService>()
                    .BuildServiceProvider();

                outputService = serviceProvider.GetService<IOutputService>();
                var labyrinthService = serviceProvider.GetService<ILabyrinthService>();
                var inputSelectionFactory = serviceProvider.GetRequiredService<IInputSelectionFactory>();
                var inputService = inputSelectionFactory.GetInputService();

                int l, r, c;
                var labyrinths = new List<ILabyrinth>();


                while (true)
                {
                    outputService!.ConsoleOutputLine("L R C");
                    string? inputString = inputService.Input();

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
                        outputService.ConsoleOutput(Environment.NewLine);
                        break;
                    }

                    var labyrinth = new Domain.Labyrinth(l, r, c);

                    labyrinthService!.CreateLabyrinth(labyrinth);

                    labyrinths.Add(labyrinth);
                    outputService?.ConsoleOutput(Environment.NewLine);
                }

                if (labyrinths.Count == 0)
                {
                    outputService.ConsoleOutputLine("Exit!");
                    return;
                }

                outputService.ConsoleOutputLine("Ausgabe:\n");

                foreach (var labyrinth in labyrinths)
                {
                    if (!labyrinthService!.BreadthFirstSearch(labyrinth, out List<IQuader> shortestPathList))
                    {
                        outputService.ConsoleOutputLine("Gefangen :-(\n");
                    }
                    else
                    {
                        var minTime = shortestPathList[1].Value;
                        outputService.ConsoleOutputLine($"Entkommen in {minTime} Minute(n)!)\n");
                    }
                }

            }
            catch (FormatException ex)
            {
                outputService?.ConsoleOutputLine($"\nInput Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                outputService?.ConsoleOutputLine($"\nUnexpected Error: {ex.Message}");
            }
            finally
            {
                outputService?.ConsoleOutputLine("\nExit!");
            }
        }

    }
}
