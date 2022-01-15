using System;
using System.Collections.Generic;
using System.Linq;
using Fibonacci.Factory;
using Fibonacci.Services;
using IOServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fibonacci
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
                    .AddTransient<IFibonacciService, FibonacciService>()
                    .AddSingleton<IInputService, InputFromFileService>()
                    .AddSingleton<IOutputService, OutputService>()
                    .Configure<ApplicationSettings>(configSection)
                    .BuildServiceProvider();

                outputService = serviceProvider.GetService<IOutputService>();
                var fibonacciService = serviceProvider.GetService<IFibonacciService>();
                var inputSelectionFactory = serviceProvider.GetRequiredService<IInputSelectionFactory>();
                var inputService = inputSelectionFactory.GetInputService();


                var numberList = new List<int>();

                outputService!.ConsoleOutput("Geben Sie, bitte Anzahl von Zahlen ein: ");
                
                int n = Convert.ToInt32(inputService.Input());
                
                for (int i = 0; i < n; i++)
                {
                    numberList.Add(Convert.ToInt32(inputService.Input()));
                }
                
                foreach (var number in numberList)
                {
                    outputService.ConsoleOutputLine(
                        $"Die Fibonacci Zahl für {number} ist: {fibonacciService!.Fib(number)}\n");
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

        static int GenerateDigit(Random rng)
        {
           return (short)rng.Next(5000);
        }
    }
}
                //Random rng = new Random();
                //for (int i = 1; i < 523; i++)
                //{
                //    numberList.Add(GenerateDigit(rng));
                //}