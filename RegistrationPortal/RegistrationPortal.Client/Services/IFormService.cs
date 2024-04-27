namespace RegistrationPortal;

public interface IFormService
{
    //Task<List<FormDto>> GetForms();
    Task<FormDto> GetForm(string id);
    Task CreateForm(FormDto form);
    Task UpdateForm(FormDto form);
}