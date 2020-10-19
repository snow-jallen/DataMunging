using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace DataMunging.Test
{
    public class Tests
    {
        ParsingProfile weatherProfile = new ParsingProfile
        {
            KeyIndex = 0,
            MaxIndex = 1,
            MinIndex = 2
        };
        string [] data = new[]{
                " Dy MxT   MnT   AvT   HDDay  AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP",
                "",
                "   8  75    54    65          50.0       0.00 FH      160  4.2 150  10  2.6  93 41 1026.3",
                "   9  86    32*   59       6  61.5       0.00         240  7.6 220  12  6.0  78 46 1018.6",
                "  12  88*   73    81          68.7       0.00 RTH     250  8.1 270  21  7.9  94 51 1007.0",
                "  13  70    59    65          55.0       0.00 H       150  3.0 150   8 10.0  83 59 1012.6",
                "  14  61    59    60       5  55.9       0.00 RF      060  6.7 080   9 10.0  93 87 1008.6",
                "mo  82.9  60.5  71.7    16  58.8       0.00              6.9          5.3"
                };

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGetLine()
        {
            var actualResult = TemperatureParser.ParseRow(weatherProfile, data[2]);
            var expectedResult = makeWeatherData(8, 75, 54);
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void ParsingProperlyCleansLine()
        {
            var actualResult = TemperatureParser.ParseRow(weatherProfile, data[3]);
            var expectedResult = makeWeatherData(9, 86, 32);
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void TestGetThirdLine()
        {
            var actualResult = TemperatureParser.ParseRow(weatherProfile, data[4]);
            var expectedResult = makeWeatherData(12, 88, 73);
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void TestLastLine()
        {
            try
            {
                var actualResult = TemperatureParser.ParseRow(weatherProfile, data.Last());
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }

        [TestCase(8, 75, ExpectedResult = 75-8 )]
        [TestCase(-1, 75, ExpectedResult = 75-(-1) )]
        [TestCase(5, 75, ExpectedResult = 75-5 )]
        [TestCase(-17, -7, ExpectedResult = -7-(-17) )]
        public int TemperatureDeltaTest(int minTemp, int maxTemp) =>
            makeWeatherData(0, maxTemp, minTemp).DeltaTemp;

        [TestCase(75, 1000, ExpectedResult = 75)]
        [TestCase(10, 100, ExpectedResult = 10)]
        [TestCase(66, 66, ExpectedResult = 66)]
        public int TestReturnLowestDelta(int deltaLow, int deltaHigh)
        {
            return DailyWeatherData.FindLowestDelta(deltaLow, deltaHigh);
        }

        static private DailyWeatherData makeWeatherData(int day, int maxTemp, int minTemp)
        {
            return new DailyWeatherData()
            {
                Key = day.ToString(),
                MaxTemp = maxTemp,
                MinTemp = minTemp
            };
        }

        [Test]
        public void EnsureProperParsingProfileForWeatherData()
        {
            ParsingProfile Actual = new ParsingProfile()
            {
                KeyIndex = 0,
                MaxIndex = 1,
                MinIndex = 2,
                RowContent = data.Skip(2)
            };
        }
    }
}