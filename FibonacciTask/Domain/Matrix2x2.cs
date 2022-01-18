using System.Numerics;

namespace FibonacciTask.Domain
{
    public class Matrix2x2
    {
        public BigInteger  X11 { get; set; }
        public BigInteger  X12 { get; set; }
        public BigInteger  X21 { get; set; }
        public BigInteger  X22 { get; set; }

        public static Matrix2x2 operator *(Matrix2x2 m1, Matrix2x2 m2)
        {
            return new Matrix2x2
            {
                X11 = m1.X11 * m2.X11 + m1.X12 * m2.X21,
                X12 = m1.X11 * m2.X12 + m1.X12 * m2.X22,
                X21 = m1.X21 * m2.X11 + m1.X22 * m2.X21,
                X22 = m1.X21 * m2.X12 + m1.X22 * m2.X22
            };
        }
    }
}