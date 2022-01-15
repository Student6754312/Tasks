using System.Numerics;
using Fibonacci.Domain;

namespace Fibonacci.Services
{
    public class FibonacciService : IFibonacciService

    {
        private static readonly Matrix2x2 FibMatrix = new Matrix2x2 { x11 = 1, x12 = 1, x21 = 1 };
        private static readonly Matrix2x2 IdentityMatrix = new Matrix2x2 { x11 = 1, x22 = 1 };

        public BigInteger Fib(int n)
        {
            return numberPower(FibMatrix, (n - 1)).x11;
        }

        private Matrix2x2 numberPower(Matrix2x2 x, int power)
        {
            if (power == 0)
            {
                return IdentityMatrix;
            }

            if (power == 1)
            {
                return x;
            }

            int n = sizeof(int) * 8 - 1;

            while ((power <<= 1) >= 0) n--;
            
            Matrix2x2 tempMatrix = x;

            while (--n > 0)
            {
                tempMatrix = (tempMatrix * tempMatrix) * (((power <<= 1) < 0) ? x : IdentityMatrix);
            }

            return tempMatrix;
        }
    }
}