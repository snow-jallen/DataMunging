using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace DataMunging.Test
{
    [Binding]
    public class TestBuildWeatherDataListStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;

        [Given(@"A basic file containing weather info")]
        public void GivenABasicFileContainingWeatherInfo()
        {
            var fileText = @"Dy MxT   MnT AvT   HDDay AvDP 1HrP TPcpn WxType PDir AvSp Dir MxS SkyC MxR MnR AvSLP

   1  88    59    74          53.8       0.00 F       280  9.6 270  17  1.6  93 23 1004.5
   2  79    63    71          46.5       0.00         330  8.7 340  23  3.3  70 28 1004.5
   3  77    55    66          39.6       0.00         350  5.0 350   9  2.8  59 24 1016.8
";

            scenarioContext.Add(nameof(fileText), fileText);

        }

        [When(@"the file is parsed")]
        public void WhenTheFileIsParsed()
        {
            var fileText = scenarioContext.Get<string>("fileText");
            var results = TemperatureParser.ParseFile(fileText);
            scenarioContext.Add("ParsedResults", results);
        }

        [Then(@"we get the following WeatherData list")]
        public void ThenWeGetTheFollowingWeatherDataList(Table table)
        {
            var actualResults = scenarioContext.Get<IEnumerable<DailyWeatherData>>("ParsedResults");
            var expectedResults = table.CreateSet<DailyWeatherData>();

            actualResults.Should().BeEquivalentTo(expectedResults);
        }

        [Then(@"the smallest temperature spread is on day (.*)")]
        public void ThenTheSmallestTemperatureSpreadIsOnDay(int expectedDay)
        {
            var parsedResults = scenarioContext.Get<IEnumerable<DailyWeatherData>>("ParsedResults");
            var actualDay = parsedResults.OrderBy(r => r.DeltaTemp).Select(r => r.Day);
        }


        public TestBuildWeatherDataListStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

    }


}
