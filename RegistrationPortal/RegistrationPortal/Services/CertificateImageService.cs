//using SixLabors.Fonts;
//using SixLabors.ImageSharp;
//using SixLabors.ImageSharp.Drawing.Processing;
//using SixLabors.ImageSharp.PixelFormats;
//using SixLabors.ImageSharp.Processing;

//public class CertificateImageService
//{
//    private readonly IWebHostEnvironment _env;

//    public CertificateImageService(IWebHostEnvironment env)
//    {
//        _env = env;
//    }

//    public async Task<byte[]> GenerateCertificateImageAsync(string name, string category, string division)
//    {
//        var imagePath = Path.Combine(_env.WebRootPath, "cert", $"participation-certificate.jpg");
//        var fontPath = Path.Combine(_env.WebRootPath, "fonts", "Times-Roman.ttf");

//        using (var image = Image.Load<Rgba32>(imagePath))
//        {
//            // Load the font
//            var fontCollection = new FontCollection();
//            var fontFamily = fontCollection.Add(fontPath); // Use the path to the Times-Roman.ttf file
//            var font = fontFamily.CreateFont(72);

//            var textOptions = new TextOptions(font)
//            {
//                HorizontalAlignment = HorizontalAlignment.Center,
//                VerticalAlignment = VerticalAlignment.Center
//            };

//            // Measure the text size to properly center it
//            var textSize = TextMeasurer.MeasureSize(name, new TextOptions(font)
//            {
//                WrappingLength = image.Width - 40 // Adjust this if necessary
//            });

//            // Calculate the position to center the text horizontally and position it 375 pixels above the center
//            var textPosition = new PointF((image.Width - textSize.Width) / 2, (image.Height / 2) - 225);

//            // Draw the text on the image
//            image.Mutate(ctx => ctx.DrawText(new DrawingOptions(), name, font, Color.Black, textPosition));

//            // Save the image to a memory stream
//            using (var ms = new MemoryStream())
//            {
//                await image.SaveAsPngAsync(ms);
//                return ms.ToArray();
//            }
//        }
//    }
//}

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

    public async Task<byte[]> GenerateCertificateImageAsync(string name, string category, string division)
    {
        var imagePath = Path.Combine(_env.WebRootPath, "cert", $"participation-certificate.jpg");
        var fontPath = Path.Combine(_env.WebRootPath, "fonts", "Times-Roman.ttf");

        using (var image = Image.Load<Rgba32>(imagePath))
        {
            // Load the font
            var fontCollection = new FontCollection();
            var fontFamily = fontCollection.Add(fontPath);
            var nameFont = fontFamily.CreateFont(72);
            var divisionCategoryFont = fontFamily.CreateFont(48);

            // Combine division and category into one string
            var divisionCategoryText = $"{division} division - {category} category";

            var nameTextOptions = new TextOptions(nameFont)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var divisionCategoryTextOptions = new TextOptions(divisionCategoryFont)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Measure the text size to properly center it
            var textSize = TextMeasurer.MeasureSize(name, new TextOptions(nameFont)
            {
                WrappingLength = image.Width - 40 // Adjust this if necessary
            });

            // Calculate the position to center the text horizontally and position it 375 pixels above the center
            var namePosition = new PointF((image.Width - textSize.Width) / 2, (image.Height / 2) - 225);

            // Measure the division-category text size to properly center it
            var divisionCategoryTextSize = TextMeasurer.MeasureSize(divisionCategoryText, new TextOptions(divisionCategoryFont)
            {
                WrappingLength = image.Width - 40 // Adjust this if necessary
            });

            // Position the division-category text below the name
            var divisionCategoryPosition = new PointF((image.Width - divisionCategoryTextSize.Width) / 2, (image.Height / 2) - 125);

            // Draw the text on the image
            image.Mutate(ctx =>
            {
                ctx.DrawText(new DrawingOptions(), name, nameFont, Color.Black, namePosition);
                ctx.DrawText(new DrawingOptions(), divisionCategoryText, divisionCategoryFont, Color.Black, divisionCategoryPosition);
            });

            // Save the image to a memory stream
            using (var ms = new MemoryStream())
            {
                await image.SaveAsPngAsync(ms);
                return ms.ToArray();
            }
        }
    }
}