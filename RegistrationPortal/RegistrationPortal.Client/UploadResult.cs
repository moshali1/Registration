namespace RegistrationPortal.Client;

public class UploadResult
{
    // Property to store the name of the file attempted to be uploaded
    public string FileName { get; set; }

    // Boolean indicating whether the file was successfully uploaded
    public bool Uploaded { get; set; }

    // An error code representing the reason for a failed upload
    public int ErrorCode { get; set; }

    // The name of the file as stored on the server or destination storage
    public string StoredFileName { get; set; }

    // Default constructor
    public UploadResult()
    {
        // Initialize properties with default values
        FileName = string.Empty;
        Uploaded = false;
        ErrorCode = 0;
        StoredFileName = string.Empty;
    }

    // Additional constructors, methods, or logic can be added as needed
}