Feature: TestBuildWeatherDataList

Given a file, return a list of weather data objects

Scenario: Basic File
	Given A basic file containing weather info
	When the file is parsed
	Then we get the following WeatherData list
	| Key | MaxTemp | MinTemp |
	| 1   | 88      | 59      |
	| 2   | 79      | 63      |
	| 3   | 77      | 55      |

Scenario: Football data
	Given A basic file containing football info
	When the file is parsed
	Then we get the following WeatherData list
	| Key          | MaxTemp | MinTemp |
	| Arsenal      | 79      | 36      |
	| Liverpool    | 67      | 30      |
	| Manchester_U | 87      | 45      |

Scenario: Lowest Day Delta
	Given A basic file containing weather info
	When the file is parsed
	Then the smallest temperature spread is on day 2


