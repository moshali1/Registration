using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace RegistrationPortal.Services;

public class FaceDetectionService : IFaceDetectionService
{
    private readonly HttpClient _httpClient;
    private readonly string _visionKey;
    private readonly string _visionEndpoint;

    public FaceDetectionService(IConfiguration config)
    {
        _visionEndpoint = config.GetValue<string>("Vision:Endpoint");
        _visionKey = config.GetValue<string>("Vision:Key");
        _httpClient = new HttpClient();

        // Configure the HttpClient instance with the necessary headers for the Face API
        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _visionKey);
    }

    public async Task<bool> DetectFaceAsync(Stream imageStream)
    {
        var url = $"{_visionEndpoint}face/v1.0/detect";

        // Set the request's content to the image stream directly
        using (var content = new StreamContent(imageStream))
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            // Send the POST request
            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}. {response.ReasonPhrase}");
                return false;
            }

            /// Assuming a successful response, parse it
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);

            // Deserialize JSON response to a list of objects
            var detectedFaces = JsonConvert.DeserializeObject<List<dynamic>>(responseContent);

            // Check if the list is not null and contains items
            bool facesDetected = detectedFaces != null && detectedFaces.Count > 0;
            return facesDetected;
        }
    }
}
