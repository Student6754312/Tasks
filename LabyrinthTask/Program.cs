using System;
using System.Collections.Generic;
using System.IO;
using IOServices.Interfaces;
using IOServices.ServiceFactory;
using LabyrinthTask.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace LabyrinthTask
{
    class Program
    {
        static void Main(string[] args)
        {

            IOutputService? _outputService = null;

            try
            {
                //Setup DI
                var serviceProvider = DependencyContainer.GetContainer();
               
                var taskSolution = serviceProvider.GetRequiredService<ITaskSolution>();
                var outputServiceFeFactory = serviceProvider.GetRequiredService<IOutputServiceFactory>();
                _outputService = outputServiceFeFactory.GetService();
                
                var labyrinthList = new List<ILabyrinth>();

                taskSolution.Input(labyrinthList);

                taskSolution.Output(labyrinthList);
            }
            catch (FormatException ex)
            {
                _outputService?.Output($"\nInput Error: {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                _outputService?.Output($"\nFile Not Found Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _outputService?.Output($"\nUnexpected Error: {ex.Message}");
            }
            finally
            {
                _outputService?.Output("\nExit!");
            }
        }
    }
}
