using System.Numerics;
using Fibonacci.Domain;

namespace Fibonacci.Services
{
    public class FibonacciService : IFibonacciService

    {
        private readonly Matrix2x2 _fibMatrix = new () { X11 = 1, X12 = 1, X21 = 1 };
        private readonly Matrix2x2 _identityMatrix = new () { X11 = 1, X22 = 1 };

        public BigInteger Fib(int n)
        {
            return NumberPower(_fibMatrix, (n - 1)).X11;
        }

        private Matrix2x2 NumberPower(Matrix2x2 x, int power)
        {
            if (power == 0)
            {
                return _identityMatrix;
            }

            if (power == 1)
            {
                return x;
            }

            int n = sizeof(int) * 8 - 1;

            while ((power <<= 1) >= 0)
            {
                n--;
            }
            
            Matrix2x2 tempMatrix = x;

            while (--n > 0)
            {
                tempMatrix = (tempMatrix * tempMatrix) * (((power <<= 1) < 0) ? x : _identityMatrix);
            }

            return tempMatrix;
        }
    }
}