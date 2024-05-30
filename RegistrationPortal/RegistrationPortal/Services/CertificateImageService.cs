using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public class CertificateImageService
{
    private readonly IWebHostEnvironment _env;

    public CertificateImageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<byte[]> GenerateCertificateImageAsync(string name, string category, string division, char gender)
    {
        var imagePath = Path.Combine(_env.WebRootPath, "cert", $"certificate-{category}-{division}-{gender}.jpg");
        var fontPath = Path.Combine(_env.WebRootPath, "fonts", "Times-Roman.ttf");

        using (var image = Image.Load<Rgba32>(imagePath))
        {
            // Load the font
            var fontCollection = new FontCollection();
            var fontFamily = fontCollection.Add(fontPath); // Use the path to the Times-Roman.ttf file
            var font = fontFamily.CreateFont(72);

            var textOptions = new TextOptions(font)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Measure the text size to properly center it
            var textSize = TextMeasurer.MeasureSize(name, new TextOptions(font)
            {
                WrappingLength = image.Width - 40 // Adjust this if necessary
            });

            // Calculate the position to center the text horizontally and position it 375 pixels above the center
            var textPosition = new PointF((image.Width - textSize.Width) / 2, (image.Height / 2) - 375);

            // Draw the text on the image
            image.Mutate(ctx => ctx.DrawText(new DrawingOptions(), name, font, Color.Black, textPosition));

            // Save the image to a memory stream
            using (var ms = new MemoryStream())
            {
                await image.SaveAsPngAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
