using System.Collections.Generic;

namespace Fibonacci.Domain
{
    public interface ITaskSolution
    {
        void Input(List<int> numberList);
        void Output(List<int> numbeList);
    }
}