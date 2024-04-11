using System;

namespace IntegerCalculations.Logic
{

    public struct FixedPoint
    {
        int _value;
        public const int Denum = 65536; // 16 бит
        public const int BitsDenum = 16;

        public FixedPoint(int value = 0) { _value = value; }

        public double ToDouble()
        {
            return _value / (double)Denum;
        }

        public static FixedPoint FromDouble(double num)
        {
            return new FixedPoint((int)(num * Denum));
        }

        public static FixedPoint operator +(FixedPoint fp1, FixedPoint fp2)
        {
            return new FixedPoint(fp1._value + fp2._value);
        }

        public static FixedPoint operator -(FixedPoint fp1, FixedPoint fp2)
        {
            return new FixedPoint(fp1._value - fp2._value);
        }

        public static FixedPoint operator *(FixedPoint fp1, FixedPoint fp2)
        {
            return new FixedPoint(Mul(fp1._value, fp2._value));
        }

        public static FixedPoint operator /(FixedPoint fp1, FixedPoint fp2)
        {
            // Корректировка масштаба деления
            int temp = fp1._value << BitsDenum;
            return new FixedPoint(temp / fp2._value);
        }

        /// <summary>
        /// Целочисленное умножение
        /// </summary>
        /// <param name="fp1"></param>
        /// <param name="fp2"></param>
        /// <returns></returns>
        public static int Mul(int fp1, int fp2) 
        {
            int i1 = fp1 >> BitsDenum, i2 = fp2 >> BitsDenum; // Верхние биты
            int f1 = fp1 & 0x0000ffff, f2 = fp2 & 0x0000ffff; // Нижние биты

            int i = (i1*i2) << BitsDenum;
            int f = (f1*f2) >> BitsDenum;
            int fi = (i1 * f2) + (i2 * f1);
            return i + f + fi;
        }

        public static int[] ToFixedPointAsInt(IEnumerable<double> doubles)
        {
            double[] s = doubles.ToArray();
            int[] sInt = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
                sInt[i] = (int)(s[i] * Denum+0.5);

            return sInt;
        }


        public static double[] FixedPointToDouble(IEnumerable<int> fp)
        {
            int[] s = fp.ToArray();
            double[] sD = new double[s.Length];

            for (int i = 0; i < s.Length; i++)
                sD[i] = s[i] / (double)Denum;

            return sD;
        }

    }
}
