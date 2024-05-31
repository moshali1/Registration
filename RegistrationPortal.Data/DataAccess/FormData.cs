using Microsoft.Extensions.Caching.Memory;
using RegistrationPortal.Data.Models;

namespace RegistrationPortal.Data.DataAccess;

public class FormData : IFormData
{
    private readonly IMongoCollection<Form> _forms;
    private readonly IMemoryCache _cache;
    private const string CacheName = "FormData";
    private const string CacheNameBasic = "BasicFormData";

    public FormData(IDbConnection db, IMemoryCache cache)
    {
        _cache = cache;
        _forms = db.FormCollection;
    }

    public async Task<List<Form>> GetForms() =>
        await _forms.Find(_ => true).ToListAsync();


    public async Task<List<Form>> GetCachedForms()
    {
        var output = _cache.Get<List<Form>>(CacheName);
        if (output is null)
        {
            var results = await _forms.Find(_ => true).ToListAsync();
            output = results;

            _cache.Set((CacheName), output, TimeSpan.FromHours(1));
        }
        return output;
    }

    public void ClearCache()
    {
        _cache.Remove(CacheName);
    }

    public async Task<List<BasicForm>> GetFormsSummary(bool useCache = true)
    {
        if (useCache)
        {
            var output = _cache.Get<List<BasicForm>>(CacheNameBasic);
            if (output != null)
            {
                return output;
            }
        }

        // If not using cache or cache miss occurs, fetch from database
        // Adjust this projection part according to your database access method
        var results = await _forms.Find(_ => true)
            .Project(form => new BasicForm
            {
                Id = form.Id,
                FirstName = form.PersonalInfo.FirstName,
                MiddleName = form.PersonalInfo.MiddleName,
                LastName = form.PersonalInfo.LastName,
                Gender = form.PersonalInfo.Gender,
                DOB = form.PersonalInfo.DOB,
                Country = form.AddressInfo.Country,
                StateProvince = form.AddressInfo.StateProvince,
                City = form.AddressInfo.City,
                Category = form.CompetitionInfo.Category,
                Portion = form.CompetitionInfo.Portion,
                Division = form.CompetitionInfo.Division,
                Status = form.StatusInfo.Status,
            })
            .ToListAsync();

        // Only set cache if useCache is true
        if (useCache)
        {
            _cache.Set(CacheName, results, TimeSpan.FromDays(1));
        }

        return results;
    }

    public async Task<List<Form>> GetPendingForms() =>
        await _forms.Find(f => f.StatusInfo.Status == "Pending").ToListAsync();

    public async Task<List<Form>> GetFormsByCreator(string id) =>
        await _forms.Find(f => f.Creator == id && f.StatusInfo.Status != "Withdrawn" && f.StatusInfo.Status != "Disqualified").ToListAsync(); 

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
