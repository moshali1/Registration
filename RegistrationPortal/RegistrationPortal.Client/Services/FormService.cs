using Newtonsoft.Json;
using System.Net.Http.Json;

namespace RegistrationPortal.Client.Services;

public class FormService : IFormService
{
    private readonly HttpClient _httpClient;

    public FormService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FormDto> GetForm(string id)
    {
        return await _httpClient.GetFromJsonAsync<FormDto>($"api/form/{id}");
    }

    public async Task CreateForm(FormDto form)
    {
        var response = await _httpClient.PostAsJsonAsync("api/form", form);
        if (!response.IsSuccessStatusCode)
        {
            //var errorContent = await response.Content.ReadAsStringAsync();
            //var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
            //throw new Exception(errorResponse.Error);

            var errorContent = await response.Content.ReadAsStringAsync();
            ErrorResponse errorResponse = null;
            try
            {
                errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                if (errorResponse == null || string.IsNullOrEmpty(errorResponse.Error))
                {
                    Console.WriteLine("The error response from the API was null or empty.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occurred while deserializing the error response: ", ex.Message);
                throw; // Re-throwing the exception here keeps the exception propagation intact.
            }
        }
    }
    public async Task UpdateForm(FormDto form)
    {
        await _httpClient.PutAsJsonAsync($"api/form", form);
    }
}

public class ErrorResponse
{
    public string Error { get; set; }
}