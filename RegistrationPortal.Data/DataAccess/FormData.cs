namespace RegistrationPortal.Data.DataAccess;

public class FormData : IFormData
{
    private readonly IMongoCollection<Form> _forms;

    public FormData(IDbConnection db) =>
        _forms = db.FormCollection;

    public async Task<List<Form>> GetForms() => 
        await _forms.Find(_ => true).ToListAsync();

    public async Task<List<Form>> GetFormsByCreator(string id) =>
        await _forms.Find(f => f.Creator == id).ToListAsync(); 

    public async Task<Form> GetForm(string id) =>
        await _forms.Find(c => c.Id == id).FirstOrDefaultAsync();

    public async Task CreateForm(Form form) =>
        await _forms.InsertOneAsync(form);

    public async Task UpdateForm(Form form) =>
        await _forms.ReplaceOneAsync(f => f.Id == form.Id, form);

    public async Task DeleteForm(string id) =>
        await _forms.DeleteOneAsync(f => f.Id == id);

    // This method is placed in the FormData class for two main reasons: 
    // 1) Efficiency: Being closer to the database layer allows the application 
    //    to leverage MongoDB's query mechanisms, including indexing, for faster duplicate checks.
    // 2) Atomicity: Checking for duplicates and performing CRUD operations can be closer in sequence, 
    //    reducing the risk of a duplicate being inserted between the check and the actual insertion.
    public async Task<bool> DoesDuplicateExist(Form form)
    {
        var filterBuilder = Builders<Form>.Filter;
        var filter = filterBuilder.Eq("PersonalInfo.FirstName", form.PersonalInfo.FirstName) &
                     filterBuilder.Eq("PersonalInfo.LastName", form.PersonalInfo.LastName) &
                     filterBuilder.Eq("PersonalInfo.DOB", form.PersonalInfo.DOB) &
                     filterBuilder.Eq("CompetitionInfo.Division", form.CompetitionInfo.Division);

        if (form.Id != null)
        {
            filter &= filterBuilder.Ne("Id", form.Id);
        }

        var duplicateCount = await _forms.CountDocumentsAsync(filter);
        return duplicateCount > 0;
    }
}
