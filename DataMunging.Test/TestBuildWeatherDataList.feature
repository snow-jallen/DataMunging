Feature: TestBuildWeatherDataList

Given a file, return a list of weather data objects

Scenario: Basic File
	Given A basic file containing weather info
	When the file is parsed
	Then we get the following WeatherData list
	| Day | MaxTemp | MinTemp |
	| 1   | 88      | 59      |
	| 2   | 79      | 63      |
	| 3   | 77      | 55      |

Scenario: Lowest Day Delta
	Given A basic file containing weather info
	When the file is parsed
	Then the smallest temperature spread is on day 2

	
