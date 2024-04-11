using System;

namespace IntegerCalculations.Logic
{

    public struct FixedPoint
    {
        int _value;
        public const int Denum = 1024; // 10 бит
        public const int BitsDenum = 10;

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
            int temp = fp1._value * fp2._value;
            // Смещение бит после умножения для корректировки
            return new FixedPoint(temp >> BitsDenum);
        }

        public static FixedPoint operator /(FixedPoint fp1, FixedPoint fp2)
        {
            // Корректировка масштаба деления
            int temp = fp1._value << BitsDenum;
            return new FixedPoint(temp / fp2._value);
        }



        public static int[] ToFixedPointAsInt(IEnumerable<double> doubles)
        {
            double[] s = doubles.ToArray();
            int[] sInt = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
                sInt[i] = (int)(s[i] * Denum);

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
