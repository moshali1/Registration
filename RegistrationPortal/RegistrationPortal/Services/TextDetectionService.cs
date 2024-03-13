using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.AspNetCore.Components.Forms;

namespace RegistrationPortal.Services;

public class TextDetectionService : ITextDetectionService
{
    private readonly string _imageAnalysisKey;
    private readonly string _imageAnalysisEndpoint;

    public TextDetectionService(IConfiguration config)
    {
        _imageAnalysisEndpoint = config.GetValue<string>("ImageAnalysis:Endpoint");
        _imageAnalysisKey = config.GetValue<string>("ImageAnalysis:Key");

    }
    public async Task<bool> DetectTextAsync(IBrowserFile file)
    {
        Console.WriteLine("Triggered DetectTextAsync");
        await Console.Out.WriteLineAsync(_imageAnalysisEndpoint);
        await Console.Out.WriteLineAsync(_imageAnalysisKey);

        // Assuming these are correctly set up elsewhere in your class
        ImageAnalysisClient client = new ImageAnalysisClient(
            new Uri(_imageAnalysisEndpoint), 
            new AzureKeyCredential(_imageAnalysisKey));

        // Define a list of keywords related to IDs
        var idKeywords = new List<string> { "name", "surname", "date", "state", "license", "passport", "minnesota", "birth", "USA", "Canada", "Mexico", "visa", "drivers", "driver's", "identification", "card", "city", "united", "states", "america", "sex", "certificate" }; // Expand as needed

        bool containsIdRelatedKeywords = false;

        try
        {

            using (var stream = file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 5)) //5 MB limit
            {

                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0; // Reset position after copying

                await Console.Out.WriteLineAsync("Point A");

                //BinaryData imageData = BinaryData.FromStream(imageStream);

                await Console.Out.WriteLineAsync("Point B");

                ImageAnalysisResult result = await client.AnalyzeAsync(BinaryData.FromStream(memoryStream), VisualFeatures.Read);

                await Console.Out.WriteLineAsync("Point C");

                foreach (var line in result.Read.Blocks.SelectMany(block => block.Lines))
                {
                    Console.WriteLine($"   Line: '{line.Text}', Bounding Polygon: [{string.Join(" ", line.BoundingPolygon)}]");
                    foreach (var word in line.Words)
                    {
                        Console.WriteLine($"     Word: '{word.Text}', Confidence {word.Confidence.ToString("#.####")}, Bounding Polygon: [{string.Join(" ", word.BoundingPolygon)}]");

                        // Check if the word is in our list of ID-related keywords (case-insensitive)
                        if (idKeywords.Any(keyword => word.Text.Equals(keyword, StringComparison.OrdinalIgnoreCase)))
                        {
                            containsIdRelatedKeywords = true;
                            break;
                        }
                    }

                    if (containsIdRelatedKeywords) break;
                }
            }

            
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }

        if (containsIdRelatedKeywords)
        {
            Console.WriteLine("The image contains ID-related keywords.");
        }
        else
        {
            Console.WriteLine("No ID-related keywords found in the image.");
        }

        return containsIdRelatedKeywords;
    }
}