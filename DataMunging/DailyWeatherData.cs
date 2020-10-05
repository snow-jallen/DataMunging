using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataMunging
{
    public class DailyWeatherData
    {
        public int Day { get; set; }
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
        public int DeltaTemp => MaxTemp - MinTemp;

        public static int FindLowestDelta(int delta1, int delta2)
        {
            if (delta1 < delta2)
            {
                return delta1;
            }
            else
            {
                return delta2;
            }
        }
    }
}
