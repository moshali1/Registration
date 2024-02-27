using Newtonsoft.Json;
using System.Reflection;

namespace RegistrationPortal.Data.DataAccess;
public class ArchivedFormData : IArchivedFormData
{
    // Utility method to get the embedded resource stream as a string
    private async Task<string> GetEmbeddedResourceAsync(string resourceName)
    {
        //ListEmbeddedResources();
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }
    public async Task<List<Form>> GetFormsAsync(string emailAddress)
    {
        var userId = await GetUserIdByEmailAsync(emailAddress);
        if (string.IsNullOrEmpty(userId))
        {
            // Handle case where user is not found
            return [];
        }

        var bForms = await GetFormsByUserIdAsync<ArchivedForm>("RegistrationPortal.Data.ArchivedForms._2023.BForms.json", userId);
        var tForms = await GetFormsByUserIdAsync<ArchivedForm>("RegistrationPortal.Data.ArchivedForms._2023.TForms.json", userId);
        var mForms = await GetFormsByUserIdAsync<ArchivedForm>("RegistrationPortal.Data.ArchivedForms._2023.MForms.json", userId);

        // Combine all forms into a single collection, then map to CurrentFormModel
        var allForms = bForms.Concat(tForms).Concat(mForms);
        var mappedForms = allForms.Select(f => MapToCurrentFormModel(f)).ToList();

        return mappedForms;
    }

    private async Task<string> GetUserIdByEmailAsync(string emailAddress)
    {
        var json = await GetEmbeddedResourceAsync("RegistrationPortal.Data.ArchivedForms._2023.Users.json");
        var users = JsonConvert.DeserializeObject<List<ArchivedUser>>(json);
        
        var userId = users?.FirstOrDefault(u => u.Email.Equals(emailAddress, StringComparison.OrdinalIgnoreCase))?.Id;

        return userId;
    }

    private async Task<IEnumerable<T>> GetFormsByUserIdAsync<T>(string resourceName, string userId) where T : ArchivedForm
    {
        var json = await GetEmbeddedResourceAsync(resourceName);
        var forms = JsonConvert.DeserializeObject<List<T>>(json);

        return forms?.Where(f => f.CreatorId == userId) ?? Enumerable.Empty<T>();
    }

    private Form MapToCurrentFormModel(ArchivedForm form)
    {
        // Implement your mapping logic here
        return new Form
        {
            PersonalInfo =
            {
                FirstName = form.FirstName,
                MiddleName = form.MiddleName,
                LastName = form.LastName,
                Gender = form.Gender,
                DOB = DateOnly.FromDateTime(form.DOB)
            },
            AddressInfo =
            {
                Country = form.Country,
                StateProvince = form.State,
                City = form.City,
            },
            ParentInfo =
            {
                ParentFirstName = form.ParentFirstName,
                ParentLastName = form.ParentLastName,
                ParentPhoneNumber = form.ParentPhoneNumber,
            },
            TeacherInfo =
            {
                TeacherFirstName = form.TeacherFirstName,
                TeacherLastName = form.TeacherLastName,
                TeacherPhoneNumber = form.TeacherPhoneNumber,
                Institution = form.Institution
            },
            CompetitionInfo =
            {
                Division = form.Division,
                Category = form.Category,
                Portion = form.CategoryType
            }
        };
    }

    //public void ListEmbeddedResources()
    //{
    //    var assembly = Assembly.GetExecutingAssembly();
    //    foreach (var resourceName in assembly.GetManifestResourceNames())
    //    {
    //        Console.WriteLine(resourceName);
    //    }
    //}
}
