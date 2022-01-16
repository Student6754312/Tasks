using System;
using System.Collections.Generic;
using System.IO;
using Fibonacci.Domain;
using Fibonacci.Services;
using IOServices;
using Labyrinth;
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
                var serviceProvider = DependencyContainer.GetContainer();
                
                var taskSolution = serviceProvider.GetRequiredService<ITaskSolution>();

                var numberList = new List<int>();

                taskSolution.Input(numberList);

                taskSolution.Output(numberList);

            }
            catch (FormatException ex)
            {
                outputService?.Output($"\nInput Error: {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                outputService?.Output($"\nFile Not Found Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                outputService?.Output($"\nUnexpected Error: {ex.Message}");
            }
            finally
            {
                outputService?.Output("\nExit!");
            }
        }

        static int GenerateDigit(Random rng)
        {
            return (short)rng.Next(5000);
        }
    }
}
