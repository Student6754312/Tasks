using System.Numerics;

namespace Fibonacci.Domain
{
    public class Matrix2x2
    {
        public BigInteger  x11, x12, x21, x22;
        public static Matrix2x2 operator *(Matrix2x2 m1, Matrix2x2 m2)
        {
            return new Matrix2x2
            {
                x11 = m1.x11 * m2.x11 + m1.x12 * m2.x21,
                x12 = m1.x11 * m2.x12 + m1.x12 * m2.x22,
                x21 = m1.x21 * m2.x11 + m1.x22 * m2.x21,
                x22 = m1.x21 * m2.x12 + m1.x22 * m2.x22
            };
        }
    }
}