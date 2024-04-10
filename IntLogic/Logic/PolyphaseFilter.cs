//Код частично взят отсюда https://github.com/ar1st0crat/NWaves 

namespace IntegerCalculations.Logic
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PolyphaseFilter
    {
        /// <summary>
        /// Полифазный фильтр с передаточной функцией E(z^k).
        /// 
        /// Пример:
        /// h = [1, 2, 3, 4, 3, 2, 1],  k = 3
        /// 
        /// e0 = [1, 0, 0, 4, 0, 0, 1]
        /// e1 = [0, 2, 0, 0, 3, 0, 0]
        /// e2 = [0, 0, 3, 0, 0, 2, 0]
        /// </summary>
        public FIR[] Filters { get; private set; }

        /// <summary>
        /// Полифазный фильтр с передаточной функцией E(z) используется для многоскоростной обработки.
        /// h = [1, 2, 3, 4, 3, 2, 1],  k = 3
        /// 
        /// e0 = [1, 4, 1]
        /// e1 = [2, 3, 0]
        /// e2 = [3, 2, 0]
        /// </summary>
        public FIR[] MultirateFilters { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="filterCount"></param>
        /// <param name="type">UP или DOWN</param>
        public PolyphaseFilter(int[] kernel, int filterCount, FilterType type = FilterType.DOWN)
        {
            Filters = new FIR[filterCount];
            MultirateFilters = new FIR[filterCount];

            int len = (kernel.Length + 1) / filterCount;

            for (int i = 0; i < Filters.Length; i++)
            {
                int[] filterKernel = new int[kernel.Length];
                int[] mrFilterKernel = new int[len];

                for (int j = 0; j < len; j++)
                {
                    int kernelPos = i + (filterCount * j);

                    if (kernelPos < kernel.Length)
                    {
                        filterKernel[kernelPos] = kernel[kernelPos];
                        mrFilterKernel[j] = kernel[kernelPos];
                    }
                }

                Filters[i] = new FIR(filterKernel);
                MultirateFilters[i] = new FIR(mrFilterKernel);
            }


            if (type == FilterType.UP)
            {
                for (int i = 0; i < Filters.Length / 2; i++)
                {
                    FIR tmp = Filters[i];
                    Filters[i] = Filters[filterCount - 1 - i];
                    Filters[filterCount - 1 - i] = tmp;

                    tmp = MultirateFilters[i];
                    MultirateFilters[i] = MultirateFilters[filterCount - 1 - i];
                    MultirateFilters[filterCount - 1 - i] = tmp;
                }
            }
        }

        public float Process(int sample)
        {
            int output = 0;

            foreach (var filter in Filters)
                output += filter.Process(sample);

            return output;
        }

        public void Reset()
        {
            foreach (var filter in Filters)
                filter.Reset();

            foreach (var filter in MultirateFilters)
                filter.Reset();
        }
    }
}
