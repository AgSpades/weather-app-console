using Newtonsoft.Json.Linq;
using dotenv.net;
using dotenv.net.Utilities;


class Program
{
    private static readonly HttpClient client = new HttpClient();


    private static readonly string apiKey = ""; // Replace with your OpenWeatherMap API key

    static async Task Main(string[] args)
    {
        Console.Write("Enter city name: ");
        string? city = Console.ReadLine();

        if (city != null)
        {
            string? weatherData = await GetWeatherDataAsync(city);
            if (weatherData != null)
            {
                DisplayWeather(weatherData);
            }
            else
            {
                Console.WriteLine("Could not fetch weather data.");
            }
        }
        else
        {
            Console.WriteLine("City name cannot be null.");
        }
    }

    private static async Task<string?> GetWeatherDataAsync(string city)
    {
        string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return null;
        }
    }

    private static void DisplayWeather(string weatherData)
    {
        JObject weatherJson = JObject.Parse(weatherData);

        string cityName = weatherJson["name"]?.ToString() ?? "Unknown City";
        string temperature = weatherJson["main"]?["temp"]?.ToString() ?? "Unknown Temperature";
        string weatherDescription = weatherJson["weather"]?[0]?["description"]?.ToString() ?? "Unknown Weather";

        Console.WriteLine($"City: {cityName}");
        Console.WriteLine($"Temperature: {temperature}°C");
        Console.WriteLine($"Weather: {weatherDescription}");
    }
}
