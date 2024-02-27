using System.ComponentModel.DataAnnotations;

namespace RegistrationPortal.Helpers;
public class GraphUser
{
    public string Id { get; set; } // Object Identifier
    [Required]
    public string DisplayName { get; set; }
    public string Surname { get; set; }
    public string GivenName { get; set; }
    [Required]
    public string UserTypeCustom { get; set; }


    public string Issuer { get; set; }
    public string SignInType { get; set; }
    public string IssuerAssignedId { get; set; }
}
