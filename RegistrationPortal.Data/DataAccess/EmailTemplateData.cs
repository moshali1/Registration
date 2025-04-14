namespace RegistrationPortal.Data.DataAccess;
public class EmailTemplateData : IEmailTemplateData
{
    private readonly IMongoCollection<EmailTemplate> _templates;

    public EmailTemplateData(IDbConnection db)
    {
        _templates = db.EmailTemplateCollection;
    }

    public async Task<List<EmailTemplate>> GetEmailTemplates() =>
        await _templates.Find(t => t.IsActive).ToListAsync();

    public async Task<EmailTemplate> GetEmailTemplate(string id) =>
        await _templates.Find(t => t.Id == id).FirstOrDefaultAsync();

    public async Task<List<EmailTemplate>> GetEmailTemplatesByDivision(string division) =>
        await _templates.Find(t => t.IsActive && (t.Division == division || t.Division == "All")).ToListAsync();

    public async Task CreateEmailTemplate(EmailTemplate template) =>
        await _templates.InsertOneAsync(template);

    public async Task UpdateEmailTemplate(EmailTemplate template) =>
        await _templates.ReplaceOneAsync(t => t.Id == template.Id, template);

    public async Task DeleteEmailTemplate(string id)
    {
        var update = Builders<EmailTemplate>.Update.Set(t => t.IsActive, false);
        await _templates.UpdateOneAsync(t => t.Id == id, update);
    }
}

