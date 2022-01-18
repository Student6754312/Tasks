using System;
using System.Collections.Generic;
using System.IO;
using FibonacciTask.Domain;
using IOServices.Interfaces;
using IOServices.ServiceFactory;
using Microsoft.Extensions.DependencyInjection;

namespace FibonacciTask
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

                var outputServiceFactory = serviceProvider.GetRequiredService<IOutputServiceFactory>();
                
                _outputService = outputServiceFactory.GetService();

                var taskSolution = serviceProvider.GetRequiredService<ITaskSolution>();

                var numberList = new List<int>();

                taskSolution.Input(numberList);

                taskSolution.Output(numberList);

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
