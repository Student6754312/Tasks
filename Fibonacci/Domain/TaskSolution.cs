using System;
using System.Collections.Generic;
using Fibonacci.Services;
using IOServices.Base;
using IOServices.ServiceFactory;

namespace Fibonacci.Domain
{
    public class TaskSolution : ITaskSolution
    {
        private readonly IOutputService _outputService;
        private readonly IInputService _inputService;
        private readonly IFibonacciService _fibonacciService;

        public TaskSolution(IOutputServiceFactory outputServiceFactory, IInputServiceFactory inputServiceFactory,
            IFibonacciService fibonacciService)
        {
            _fibonacciService = fibonacciService;
            _inputService = inputServiceFactory.GetService(); 
            _outputService = outputServiceFactory.GetService();
        }
        
        public void  Input(List<int> numberList)
        {
            _outputService.Output("Geben Sie, bitte Anzahl von Zahlen ein:");
            int n = Convert.ToInt32(_inputService.Input());

            _outputService.Output($"Eingabe {n} Zahlen:");

            for (int i = 0; i < n; i++)
            {
                _outputService.Output(Environment.NewLine);
                numberList.Add(Convert.ToInt32(_inputService.Input()));
            }
        }

        public void Output(List<int> numberList)
        {
            _outputService.Output(Environment.NewLine);
            _outputService.Output("Ausgabe:\n");

            foreach (var number in numberList)
            {
                _outputService.Output(
                    $"Die Fibonacci Zahl für {number} ist: {_fibonacciService.Fib(number)}");
                _outputService.Output(Environment.NewLine);
            }
        }
    }
}