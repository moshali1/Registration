namespace RegistrationPortal.Data.Models;
public class Settings
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    // Memorization form permissions
    public bool CanCreateMemorization { get; set; }
    public bool CanUpdateMemorization { get; set; }
    public bool CanWithdrawMemorization { get; set; }

    // Ten Qiraat form permissions
    public bool CanCreateTenQiraat { get; set; }
    public bool CanUpdateTenQiraat { get; set; }
    public bool CanWithdrawTenQiraat { get; set; }

    // Best Voice form permissions
    public bool CanCreateBestVoice { get; set; }
    public bool CanUpdateBestVoice { get; set; }
    public bool CanWithdrawBestVoice { get; set; }

    // Islamic Studies form permission
    public bool CanCreateIslamicStudies { get; set; }
    public bool CanUpdateIslamicStudies { get; set; }
    public bool CanWithdrawIslamicStudies { get; set; }
}
