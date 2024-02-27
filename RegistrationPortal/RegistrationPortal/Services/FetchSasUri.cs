using Azure.Storage;
using Azure.Storage.Sas;

namespace RegistrationPortal.Services;

// To revoke SAS tokens, modify the storage account/container access policy or change account keys, invalidating existing tokens.
public class FetchSasUri
{
    private readonly string _storageAccountName;
    private readonly string _storageAccountKey;

    public FetchSasUri(IConfiguration config)
    {
        _storageAccountName = config.GetValue<string>("AzureStorage:AccountName");
        _storageAccountKey = config.GetValue<string>("AzureStorage:AccountKey");
    }

    public string GetSasUri(string fileName)
    {
        try
        {
            var storageAccountName = _storageAccountName;
            var storageAccountKey = _storageAccountKey;

            var blobName = fileName;
            var containerName = DetermineContainerName(fileName);

            //  Defines the resource being accessed and for how long the access is allowed.
            BlobSasBuilder blobSasBuilder = new()
            {
                StartsOn = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromSeconds(5)),
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5),
                BlobContainerName = containerName,
                BlobName = blobName,
            };

            //  Defines the type of permission.
            blobSasBuilder.SetPermissions(BlobSasPermissions.Read);
            
            //  Builds an instance of StorageSharedKeyCredential that we can use to sign the SAS token     
            var storageSharedKeyCredential = new StorageSharedKeyCredential(storageAccountName, storageAccountKey);

            //  Builds the Sas URI.
            BlobSasQueryParameters sasQueryParameters = blobSasBuilder.ToSasQueryParameters(storageSharedKeyCredential);

            UriBuilder sasUri = new()
            {
                Scheme = "https",
                Host = string.Format("{0}.blob.core.windows.net", storageAccountName),
                Path = string.Format("{0}/{1}", containerName, blobName),
                Query = sasQueryParameters.ToString()
            };

            return sasUri.Uri.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return string.Empty;
        }
    }

    public string DetermineContainerName(string identifier)
    {
        string containerPrefix = "24-"; // Based on the current year
        int underscoreIndex = identifier.IndexOf('_');
        if (underscoreIndex == -1 || underscoreIndex == identifier.Length - 1) throw new ArgumentException("Invalid identifier format");

        char divisionLetter = identifier[underscoreIndex + 1];
        string division = divisionLetter switch
        {
            'B' => "bestvoice",
            'M' => "memorization",
            'T' => "tenqiraat",
            _ => throw new ArgumentException("Invalid identifier division")
        };

        string fileType = identifier.StartsWith("ID") ? "-id" :
                          identifier.Contains("Photo") ? "-photo" :
                          identifier.Contains("Video") ? "-video" : throw new ArgumentException("Invalid file type");

        return containerPrefix + division + fileType;
    }
}
