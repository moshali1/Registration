namespace RegistrationPortal.Shared.Models;
public class FormDto
{
    public string Id { get; set; }
    [ValidateComplexType]
    public PersonalInfoDto PersonalInfo { get; set; }
    [ValidateComplexType]
    public AddressInfoDto AddressInfo { get; set; }
    [ValidateComplexType]
    public CompetitionInfoDto CompetitionInfo { get; set; }
    [ValidateComplexType]
    public ParentInfoDto ParentInfo { get; set; }
    [ValidateComplexType]
    public TeacherInfoDto TeacherInfo { get; set; }
    [ValidateComplexType]
    public FileUploadInfoDto FileUploadInfo { get; set; }
    public bool AgreedToTerms { get; set; }
    public StatusInfoDto StatusInfo { get; set; } // Should this be exposed to the client?
    public List<TrackingDto> FormHistory { get; set; } = new();
    public string Creator { get; set; }
    public List<string> SharedUsers { get; set; } = new();

    public FormDto()
    {
        PersonalInfo = new();
        AddressInfo = new();
        CompetitionInfo = new();
        ParentInfo = new();
        TeacherInfo = new();
        FileUploadInfo = new();
        StatusInfo = new();
    }
}
