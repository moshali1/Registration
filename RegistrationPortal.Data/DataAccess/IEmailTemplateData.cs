namespace RegistrationPortal.Data.DataAccess;
public interface IEmailTemplateData
{
    Task<List<EmailTemplate>> GetEmailTemplates();
    Task<EmailTemplate> GetEmailTemplate(string id);
    Task<List<EmailTemplate>> GetEmailTemplatesByDivision(string division);
    Task CreateEmailTemplate(EmailTemplate template);
    Task UpdateEmailTemplate(EmailTemplate template);
    Task DeleteEmailTemplate(string id);
}
