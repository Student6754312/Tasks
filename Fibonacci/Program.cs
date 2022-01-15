using System;
using System.Collections.Generic;
using System.Linq;
using Fibonacci.Services;
using IOServices;
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
                //Setup DI
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<IInputService, InputService>()
                    .AddSingleton<IOutputService, OutputService>()
                    .AddSingleton<IFibonacciService, FibonacciService>()
                    .BuildServiceProvider();

                outputService = serviceProvider.GetService<IOutputService>();
                var inputService = serviceProvider.GetService<IInputService>();
                var fibonacciService = serviceProvider.GetService<IFibonacciService>();


                var s = inputService.GetFromFile(@"D:\Users\Konstantin\Desktop\file.txt"); 
                    

                var numberList = new List<int>();

                s.Split("\r\n").Where(s=>s.Length>0).ToList()
                    .ForEach(x => numberList.Add(Convert.ToInt32(x)));
                
                // outputService!.ConsoleOutput("Geben Sie, bitte Anzahl von Zahlen ein: ");

                //int n = Convert.ToInt32(inputService!.GetStringFromUserConsole());
                //for (int i = 0; i < n; i++)
                //{
                //    numberList.Add(Convert.ToInt32(inputService.GetStringFromUserConsole()));
                //}


                //Random rng = new Random();
                //for (int i = 1; i < 523; i++)
                //{
                //    numberList.Add(GenerateDigit(rng));
                //}


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