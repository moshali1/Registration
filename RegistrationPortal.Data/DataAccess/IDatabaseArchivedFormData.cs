
namespace RegistrationPortal.Data.DataAccess;

public interface IDatabaseArchivedFormData
{
    Task<List<Form>> GetFormsAsync(string creatorId);
    Task<List<Form>> GetFormsByCreator(string creatordId);
}