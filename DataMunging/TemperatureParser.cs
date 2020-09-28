using System;
using System.Collections.Generic;
using System.Text;

namespace DataMunging
{
    public class TemperatureParser
    {
        public TemperatureParser(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        private IDataProvider dataProvider;

        public DailyWeatherData ParseRow(string rowValue)
        {
            //DailyWeatherData
            var fields = rowValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new DailyWeatherData();
        }
    }
}
