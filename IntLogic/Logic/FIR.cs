using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegerCalculations.Logic
{
    [Serializable]
    public class FIR
    {
        public const short BitsDenum = 16;
        public int[] Buffer { get; set; }

        /// <summary>
        /// Размер ядра
        /// </summary>
        protected int _kernelSize;

        /// <summary>
        /// Текущее смещение в линии задержки
        /// </summary>
        protected int _delayLineOffset;

        /// <summary>
        /// Внутренний буфер для линии задержки
        /// </summary>
        protected int[] _delayLine;

        public FIR(IEnumerable<int> kernel) 
        {
            _kernelSize = kernel.Count();

            Buffer = new int[_kernelSize * 2];

            for (int i = 0; i < _kernelSize; i++)
            {
                Buffer[i] = Buffer[_kernelSize + i] 
                    = (int)(kernel.ElementAt(i));
            }

            _delayLine = new int[_kernelSize];
            _delayLineOffset = _kernelSize - 1;
        }

         /// <summary>
        /// КИХ-фильтрация (отсчет за отсчетом)
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public int Process(int sample)
        {
            _delayLine[_delayLineOffset] = sample;

            int output = 0;

            for (int i = 0, j = _kernelSize - _delayLineOffset; i < _kernelSize; i++, j++)  
                output += FixedPoint.Mul(_delayLine[i], Buffer[j]);

            if (--_delayLineOffset < 0) _delayLineOffset = _kernelSize - 1;

            return output;
        }


        /// <summary>
        /// Перезапуск фильтра
        /// </summary>
        public void Reset()
        {
            _delayLineOffset = _kernelSize - 1;
            Array.Clear(_delayLine, 0, _kernelSize);
        }
    }
}
