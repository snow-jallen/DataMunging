using System;
using System.Collections.Generic;
using System.Linq;
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

       static public DailyWeatherData ParseRow(string rowValue)
        {
            //DailyWeatherData
            var fields = rowValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int day = int.Parse(fields[0]);
            int maxTemp = int.Parse(fields[1]);
            int minTemp = int.Parse(fields[2].Replace("*", ""));


            return new DailyWeatherData() { Day = day,
                                            MaxTemp = maxTemp,
                                            MinTemp = minTemp};
        }
        static public IEnumerable<DailyWeatherData> ParseFile(string fileText)
        {
            var lines = fileText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<DailyWeatherData>();

            foreach(var line  in lines.Skip(1))
            {
                list.Add(ParseRow(line));
            }
            return list;

        }
    }
}
