namespace RegistrationPortal.Data.Models;
public class Form
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public PersonalInfo PersonalInfo { get; set; }
    public AddressInfo AddressInfo { get; set; }
    public CompetitionInfo CompetitionInfo { get; set; }
    public ParentInfo ParentInfo { get; set; }
    public TeacherInfo TeacherInfo { get; set; }
    public FileUploadInfo FileUploadInfo { get; set; }
    public bool AgreedToTerms { get; set; }
    public StatusInfo StatusInfo { get; set; }
    public List<Tracking> FormHistory { get; set; } = new();
    public string Creator { get; set; }
    public List<string> SharedUsers { get; set; } = new();
    public EventScheduleInfo EventScheduleInfo { get; set; }

    public Form()
    {
        PersonalInfo = new();
        AddressInfo = new();
        CompetitionInfo = new();
        ParentInfo = new();
        TeacherInfo = new();
        FileUploadInfo = new();
        StatusInfo = new();
        EventScheduleInfo = new();
    }
}
