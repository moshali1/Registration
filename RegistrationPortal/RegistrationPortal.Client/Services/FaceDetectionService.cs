using System.Net.Http.Headers;

namespace RegistrationPortal.Client.Services;

public class FaceDetectionService : IFaceDetectionService
{
    private readonly HttpClient _httpClient;

    public FaceDetectionService(HttpClient client)
    {
        _httpClient = client;
    }

    public async Task<bool> DetectFaceAsync(Stream imageStream)
    {
        using (var streamContent = new StreamContent(imageStream))
        {
            var response = await _httpClient.PostAsync("api/FaceDetection/detect-face", streamContent);
            response.EnsureSuccessStatusCode(); // Ensure a successful response

            var responseString = await response.Content.ReadAsStringAsync();
            return bool.Parse(responseString);
        }
    }
}
