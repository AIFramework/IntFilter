
using IntegerCalculations.Logic;
using System.Diagnostics;

//double a = 12.87, b = 13.56, c = -2.1;


//FixedPoint fa = FixedPoint.FromDouble(a);
//FixedPoint fb = FixedPoint.FromDouble(b);
//FixedPoint fc = FixedPoint.FromDouble(c);

//Console.WriteLine("double: " + (a + b + c));
//Console.WriteLine("fix point: " + (fa + fb + fc).ToDouble() + "\n\n\n");

//Console.WriteLine("double: " + (a * b - c));
//Console.WriteLine("fix point: " + (fa * fb - fc).ToDouble() + "\n\n\n");

//Console.WriteLine("double: " + (a - b / c));
//Console.WriteLine("fix point: " + (fa - fb / fc).ToDouble() + "\n\n\n");


//var s = fa * fb - fc;


//Stopwatch stopwatch = new Stopwatch();

//stopwatch.Start();

//for (long i = 0; i < 100000000; i++)
//    s = fa * fb - fc;
//stopwatch.Stop();

//Console.WriteLine("ms: "+stopwatch.ElapsedMilliseconds);


double[] doubles =
{
    0.01, 0.0, 0.98, 0.0, 0.01
};


double[] signal =
{
    0.1, 0.3, -0.6, 0.3, 0.6, 0.3, -1.1, -2, 0, 0
};


var coef = FixedPoint.ToFixedPointAsInt(doubles);
var s = FixedPoint.ToFixedPointAsInt(signal);
int[] sOut = new int[s.Length];



FIR fir = new(coef);

for (int i = 0; i < s.Length; i++)
    sOut[i] = fir.Process(s[i]);

double[] signalF = FixedPoint.FixedPointToDouble(sOut);

Console.WriteLine(signalF);