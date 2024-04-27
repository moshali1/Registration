namespace RegistrationPortal.Data.DataAccess;
public interface IFormData
{
    Task<List<Form>> GetForms();
    Task<List<Form>> GetCachedForms();
    Task<List<Form>> GetPendingForms();
    Task<List<BasicForm>> GetFormsSummary(bool useCache = true);
    Task<List<Form>> GetFormsByCreator(string id);
    Task<Form> GetForm(string id);
    Task CreateForm(Form form);
    Task UpdateForm(Form form);
    Task DeleteForm(string id);
    Task<bool> DoesDuplicateExist(Form form);
}
