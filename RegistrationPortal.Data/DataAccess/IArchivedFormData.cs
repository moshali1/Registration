
namespace RegistrationPortal.Data.DataAccess;

public interface IArchivedFormData
{
    Task<List<Form>> GetFormsAsync(string emailAddress);
}