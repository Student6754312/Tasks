using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Fibonacci.Services;
using IOServices;

namespace Fibonacci.Domain
{
    public class TaskSolution : ITaskSolution
    {
        private IOutputService _outputService;
        private IInputService _inputService;
        private IFibonacciService _fibonacciService;

        public TaskSolution(IOutputServiceFactory outputServiceFactory, IInputServiceFactory inputServiceFactory,
            IFibonacciService fibonacciService)
        {
            _fibonacciService = fibonacciService;
            _inputService = inputServiceFactory.GetService(); ;
            _outputService = outputServiceFactory.GetService();
        }
        
        public void  Input(List<int> numberList)
        {
            _outputService!.Output("Geben Sie, bitte Anzahl von Zahlen ein:");
            
            int n = Convert.ToInt32(_inputService.Input());

            for (int i = 0; i < n; i++)
            {
                _outputService.Output(Environment.NewLine);
                numberList.Add(Convert.ToInt32(_inputService.Input()));
            }
        }

        public void Output(List<int> numberList)
        {
            _outputService.Output(Environment.NewLine);

            foreach (var number in numberList)
            {
                _outputService.Output(
                    $"Die Fibonacci Zahl für {number} ist: {_fibonacciService!.Fib(number)}");
                _outputService.Output(Environment.NewLine);
            }
        }
    }
}