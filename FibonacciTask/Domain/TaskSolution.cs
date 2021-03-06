using System;
using System.Collections.Generic;
using System.Numerics;
using FibonacciTask.Services;
using IOServices.Interfaces;
using IOServices.ServiceFactory;

namespace FibonacciTask.Domain
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

        public void Input(List<int> numberList)
        {
            _outputService.Output("Geben Sie, bitte Anzahl von Zahlen ein:");
            int n = Convert.ToInt32(_inputService.Input());

            _outputService.Output($"Eingabe {n} Zahl(en):");
            
            _outputService.Output("");

            for (int i = 0; i < n; i++)
            {
                numberList.Add(Convert.ToInt32(_inputService.Input()));
                _outputService.Output("");
            }
        }

        public void Output(List<int> numberList)
        {
            _outputService.Output("\nAusgabe:\n");

            foreach (var number in numberList)
            {
                BigInteger fib = _fibonacciService.Fib(number);
                var fibStr = fib > UInt64.MaxValue ? fib.ToString("E25") : fib.ToString("D");

                _outputService.Output(
                    $"Die Fibonacci Zahl für {number} ist: {fibStr}");

                _outputService.Output("");
            }
        }
    }
}