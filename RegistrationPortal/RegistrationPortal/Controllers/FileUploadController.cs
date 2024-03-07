using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using MongoDB.Driver.Core.Configuration;
using System.Text;
using System.Text.Json;

namespace RegistrationPortal.Controllers;

[Authorize]
[Route("api/[controller]")]
[RequestSizeLimit(500 * 1024 * 1024)]
public class FileUploadController : ControllerBase
{
    private readonly string _storageConnectionString;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileUploadController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
    {
        _storageConnectionString = config.GetConnectionString("Storage");
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> Save(IFormFile uploadFile, [FromBody] string containerInfoJson)
    {
        if (string.IsNullOrWhiteSpace(containerInfoJson))
        {
            Console.WriteLine("ContainerInfoJson is null or empty.");
            return BadRequest("Container info JSON is required.");
        }
        else
        {
            Console.WriteLine("ContainerInfoJson is Ok in Controller.");
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

    public static byte[] content = [];

    [HttpPost("[action]")]
    public async Task<ActionResult> SaveVideo(IList<IFormFile> chunkFile, IFormFile uploadFile, [FromForm] string containerInfoJson)
    {
        if (string.IsNullOrWhiteSpace(containerInfoJson))
        {
            Console.WriteLine("ContainerInfoJson is null or empty.");
            return BadRequest("Invalid ContainerInfoJson info.");
        }

        try
        {
            var containerInfo = JsonSerializer.Deserialize<ContainerInfo>(containerInfoJson);

            foreach (var file in chunkFile)
            {
                var httpPostedChunkFile = HttpContext.Request.Form.Files["chunkFile"];

                var chunkIndex = HttpContext.Request.Form["chunk-index"];

                var totalChunk = HttpContext.Request.Form["total-chunk"];

                Console.WriteLine("info: " + httpPostedChunkFile + " " + chunkIndex + " " + totalChunk);

                using (var fileStream = file.OpenReadStream())
                {
                    if (Convert.ToInt32(chunkIndex) <= Convert.ToInt32(totalChunk))
                    {
                        var streamReader = new MemoryStream();

                        fileStream.CopyTo(streamReader);

                        var byteArr = streamReader.ToArray();

                        if (content.Length > 0)
                        {
                            content = content.Concat(byteArr).ToArray();
                        }
                        else
                        {
                            content = byteArr;
                        }
                    }

                    // If it's the last chunk, begin the upload process
                    if (Convert.ToInt32(chunkIndex) == Convert.ToInt32(totalChunk) - 1)
                    {
                        await Console.Out.WriteLineAsync("Upload to Azure Storage Starting");

                        var connectionString = _storageConnectionString;
                        var blobName = containerInfo.UniqueFileName;
                        //var blobName = httpPostedChunkFile.FileName;
                        var containerName = $"{containerInfo.Year.Substring(containerInfo.Year.Length - 2)}-{NormalizeDivision(containerInfo.Division)}-{containerInfo.UploadType}";

                        BlockBlobClient blockBlobClient = new BlockBlobClient(connectionString, containerName, blobName);

                        List<string> blockList = new List<string>();

                        // Use MemoryStream for uploading to avoid writing to the file system
                        using (var memoryStream = new MemoryStream(content))
                        {
                            byte[] buffer = new byte[10 * 1024 * 1024]; // Adjust block size as needed
                            int bytesRead;
                            while ((bytesRead = await memoryStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                            {
                                string blockId = Guid.NewGuid().ToString();
                                string base64BlockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(blockId));
                                await blockBlobClient.StageBlockAsync(base64BlockId, new MemoryStream(buffer, 0, bytesRead, writable: false));
                                blockList.Add(base64BlockId);
                            }
                        }

                        // Commit the block list to finalize the blob
                        await blockBlobClient.CommitBlockListAsync(blockList);
                        content = []; // Clear the content to free memory

                        Console.WriteLine("File uploaded successfully to Azure Storage.");
                        return Ok();
                    }
                }
            }
            return Ok();
        }
        catch (Exception e)
        {
            content = [];
            Response.Clear();
            Response.StatusCode = 204;
            Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File failed to upload";
            Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;
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
