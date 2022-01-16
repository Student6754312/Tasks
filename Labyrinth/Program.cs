using System;
using System.Collections.Generic;
using System.IO;
using IOServices;
using IOServices.ServicesFactory;
using IOServices.ServicesFactory.Base;
using Labyrinth.Domain;
using Labyrinth.Services;
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
                //Setup DI
                var serviceProvider = DependencyContainer.GetContainer();
               
                var taskSolution = serviceProvider.GetRequiredService<ITaskSolution>();
                
                var labyrinthList = new List<ILabyrinth>();

                taskSolution.Input(labyrinthList);

                taskSolution.Output(labyrinthList);
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
    }
}
