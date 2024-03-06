using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Authorization;
using System.IO.IsolatedStorage;
using System.Text;
using System.Text.Json;

namespace RegistrationPortal.Controllers;

[Authorize]
[Route("api/[controller]")]
[RequestSizeLimit(500 * 1024 * 1024)]
public class FileUploadController : ControllerBase
{
    private readonly string _storageConnectionString;

    public FileUploadController(IConfiguration config)
    {
        _storageConnectionString = config.GetConnectionString("Storage");
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> Save(IFormFile uploadFile, [FromForm] string containerInfoJson)
    {
        if (string.IsNullOrWhiteSpace(containerInfoJson))
        {
            Console.WriteLine("ContainerInfoJson is null or empty.");
            return BadRequest("Container info JSON is required.");
        }

        try
        {
            var containerInfo = JsonSerializer.Deserialize<ContainerInfo>(containerInfoJson);

            var connectionString = _storageConnectionString;
            var blobName = containerInfo.UniqueFileName;
            var containerName = $"{containerInfo.Year.Substring(containerInfo.Year.Length - 2)}-{NormalizeDivision(containerInfo.Division)}-{containerInfo.UploadType}";

            if (new[] { "id", "photo" }.Contains(containerInfo.UploadType))
            {
                BlobClient blobClient = new BlobClient(connectionString, containerName, blobName);

                var stream = uploadFile.OpenReadStream();

                await blobClient.UploadAsync(stream);
            }
            else if (containerInfo.UploadType == "video")
            {
                Console.WriteLine("Video Upload to Azure Start");
                BlockBlobClient blockBlobClient = new BlockBlobClient(connectionString, containerName, blobName);

                List<string> blockList = new List<string>();

                var stream = uploadFile.OpenReadStream();
                while (true)
                {
                    byte[] b = new byte[4 * 1024 * 1024];
                    var n = await stream.ReadAsync(b, 0, b.Length);
                    if (n == 0) break;
                    string blockId = Guid.NewGuid().ToString();
                    string base64BlockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(blockId));
                    await blockBlobClient.StageBlockAsync(base64BlockId, new MemoryStream(b, true));
                    blockList.Add(base64BlockId);
                }
                await blockBlobClient.CommitBlockListAsync(blockList);
                Console.WriteLine("Video Upload to Azure End");
            }

            return Ok("File uploaded successfully.");
        }
        catch (RequestFailedException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, $"Request Failed {e}");
        }
        catch (Exception e)
        {
            // What if a container does not exist?

            Console.WriteLine(e.Message);
            return StatusCode(500, $"Internal server error: {e}");
        }
    }


    public static string NormalizeDivision(string division)
    {
        // Assuming division names are directly provided as "Memorization", "Best Voice", "Ten Qira'at"
        // and need to be mapped to 'm', 'b', 't'
        switch (division)
        {
            case "Ten Qira'at":
                return "tenqiraat";
            case "Memorization":
                return "memorization";
            case "Best Voice":
                return "bestvoice";
            default:
                return ""; // Or some default action if division is unrecognized
        }
    }
}
