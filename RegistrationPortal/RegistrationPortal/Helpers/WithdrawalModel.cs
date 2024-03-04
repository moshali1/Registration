using System.ComponentModel.DataAnnotations;

namespace RegistrationPortal.Helpers;

public class WithdrawalModel
{
    [Required(ErrorMessage = "Comment is required.")]
    [StringLength(100, ErrorMessage = "Comment must not exceed 100 characters.")]
    public string Comment { get; set; }
}
