using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        // Traccar API URL (Replace 'deviceId=1' with the correct device ID if needed)
        string url = "http://traccar.riseapplications.com:8082/api/positions?deviceId=1";

        try
        {
            // Create HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Send GET request to the Traccar API
                HttpResponseMessage response = await client.GetAsync(url);

                // Ensure successful status code
                response.EnsureSuccessStatusCode();

                // Read the response content
                string responseContent = await response.Content.ReadAsStringAsync();

                // Output the response (for debugging purposes)
                Console.WriteLine("Response: " + responseContent);

                // Parse the JSON response
                var jsonArray = JArray.Parse(responseContent);

                if (jsonArray.Count > 0)
                {
                    // Extract the latitude and longitude from the JSON response
                    var latitude = jsonArray[0]["latitude"].ToString();
                    var longitude = jsonArray[0]["longitude"].ToString();

                    // Output the coordinates
                    Console.WriteLine($"Latitude: {latitude}");
                    Console.WriteLine($"Longitude: {longitude}");
                }
                else
                {
                    Console.WriteLine("No location data found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
