using System;
using System.Collections.Generic;
using System.IO;
using IOServices;
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
               
                var outputServiceFeFactory = serviceProvider.GetRequiredService<IOutputServiceFactory>();
                _outputService = outputServiceFeFactory.GetService();
                var taskSolution = serviceProvider.GetRequiredService<ITaskSolution>();
                
                var labyrinthList = new List<ILabyrinth>();

                taskSolution.Input(labyrinthList);

                taskSolution.Output(labyrinthList);
            }
            catch (FormatException ex)
            {
                _outputService ??= new OutputToConsoleService(); 
                _outputService.Output($"\nInput Error: {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                _outputService ??= new OutputToConsoleService();
                _outputService.Output($"\nFile Not Found Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _outputService ??= new OutputToConsoleService();
                _outputService.Output($"\nUnexpected Error: {ex.Message}");
            }
            finally
            {
                _outputService ??= new OutputToConsoleService();
                _outputService.Output("\nExit!");
            }
        }
    }
}
