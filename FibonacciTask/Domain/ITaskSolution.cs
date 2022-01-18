using System.Collections.Generic;

namespace FibonacciTask.Domain
{
    public interface ITaskSolution
    {
        void Input(List<int> numberList);
        void Output(List<int> numbeList);
    }
}