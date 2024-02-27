namespace RegistrationPortal.Shared.Models;
public class CompetitionInfoDto
{
    [Required]
    public string Category { get; set; }

    [Required]
    public string Portion { get; set; }

    [Required]
    public string Division { get; set; }
}
