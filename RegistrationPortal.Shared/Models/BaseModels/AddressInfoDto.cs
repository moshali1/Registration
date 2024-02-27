namespace RegistrationPortal.Shared.Models;
public class AddressInfoDto
{
    [Required]
    public string Country { get; set; }

    [Required, MaxLength(50, ErrorMessage = "State / Province must be a maximum of 50 characters.")]
    [Display(Name = "State / Province")]
    public string StateProvince { get; set; }

    [Required]
    public string City { get; set; }

    public string GetRegionalAddress()
    {
        return $"{City}, {StateProvince}, {Country}";
    }
}
