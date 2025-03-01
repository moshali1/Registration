namespace RegistrationPortal.Data.DataAccess;
public class DatabaseArchivedFormData : IDatabaseArchivedFormData
{
    private readonly IMongoCollection<Form> _forms;

    public DatabaseArchivedFormData(IArchivedDbConnection db) =>
        _forms = db.FormCollection;

    public async Task<List<Form>> GetFormsAsync(string creatorId)
    {
        var forms = await GetFormsByCreator(creatorId);

        var mappedForms = forms.Select(f => MapToNewForm(f)).ToList();

        return mappedForms;
    }

    public async Task<List<Form>> GetFormsByCreator(string creatordId) =>
            await _forms.Find(f => f.Creator == creatordId && (f.StatusInfo.Status == "Reviewed" || f.StatusInfo.Status == "Verified")).ToListAsync();

    private Form MapToNewForm(Form archivedForm)
    {
        // Implement your mapping logic here
        return new Form
        {
            PersonalInfo =
            {
                FirstName = archivedForm.PersonalInfo.FirstName,
                MiddleName = archivedForm.PersonalInfo.MiddleName,
                LastName = archivedForm.PersonalInfo.LastName,
                Gender = archivedForm.PersonalInfo.Gender,
                DOB = archivedForm.PersonalInfo.DOB
            },
            AddressInfo =
            {
                Country = archivedForm.AddressInfo.Country,
                StateProvince = archivedForm.AddressInfo.StateProvince,
                City = archivedForm.AddressInfo.City,
            },
            ParentInfo =
            {
                ParentFirstName = archivedForm.ParentInfo.ParentFirstName,
                ParentLastName = archivedForm.ParentInfo.ParentLastName,
                ParentPhoneNumber = archivedForm.ParentInfo.ParentPhoneNumber,
            },
            TeacherInfo =
            {
                TeacherFirstName = archivedForm.TeacherInfo.TeacherFirstName,
                TeacherLastName = archivedForm.TeacherInfo.TeacherLastName,
                TeacherPhoneNumber = archivedForm.TeacherInfo.TeacherPhoneNumber,
                Institution = archivedForm.TeacherInfo.Institution
            },
            CompetitionInfo =
            {
                Division = archivedForm.CompetitionInfo.Division,
                Category = archivedForm.CompetitionInfo.Category,
                Portion = archivedForm.CompetitionInfo.Portion
            },
            FileUploadInfo =
            {
                IDFileName = archivedForm.FileUploadInfo.IDFileName,
                PhotoFileName = archivedForm.FileUploadInfo.PhotoFileName,
                IDFileSize = archivedForm.FileUploadInfo.IDFileSize,
                PhotoFileSize = archivedForm.FileUploadInfo.PhotoFileSize,
                IDFileExtension = archivedForm.FileUploadInfo.IDFileExtension,
                PhotoFileExtension = archivedForm.FileUploadInfo.PhotoFileExtension
            },
        };
    }
}
