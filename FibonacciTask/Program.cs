using System;
using System.Collections.Generic;
using System.IO;
using FibonacciTask.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace FibonacciTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Setup DI
                var serviceProvider = DependencyContainer.GetContainer();

                var taskSolution = serviceProvider.GetRequiredService<ITaskSolution>();

                var numberList = new List<int>();

                taskSolution.Input(numberList);

                taskSolution.Output(numberList);

            }
            catch (FormatException ex)
            {
                Console.WriteLine($"\nInput Error: {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"\nFile Not Found Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nExit!");
            }
        }
    }
}
