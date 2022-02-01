using System;
using System.Collections.Generic;
using System.IO;
using LabyrinthTask.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace LabyrinthTask
{
    class Program
    {
        static void Main()
        {

            try
            {
                //Setup DI
                var serviceProvider = DependencyContainer.GetContainer();
               
                var taskSolution = serviceProvider.GetRequiredService<ITaskSolution>();
                
                var labyrinthList = new List<ILabyrinth>();

                taskSolution.Input(labyrinthList);

                taskSolution.Output(labyrinthList);
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
