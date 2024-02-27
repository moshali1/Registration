/// <summary>
/// The DataPrefiller class provides a utility method for pre-filling a 'Form' object
/// with sample data. This is particularly useful for testing and debugging purposes.
/// The class contains a static list of sample 'Form' objects. The 'PrefillFields' method 
/// rotates through this list to populate a provided 'Form' object with the next set of sample data.
/// </summary>
namespace RegistrationPortal.Shared.Helpers;
public class DataPrefiller
{
    private static int currentIndex = -1;

    private static readonly List<FormDto> FakeData = new()
    {
        new FormDto
        {
            PersonalInfo = new PersonalInfoDto
            {
                FirstName = "Khalid",
                MiddleName = "Abdi",
                LastName = "Warsame",
                Gender = Gender.Male,
                DOB = new DateOnly(2018, 8, 18)
            },
            AddressInfo = new AddressInfoDto
            {
                Country = "United States",
                StateProvince = "CA",
                City = "Los Angeles"
            },
            CompetitionInfo = new CompetitionInfoDto
            {
                Division = "Memorization",
                Category = "5 Juz",
                Portion = "Top"
            },
            ParentInfo = new ParentInfoDto
            {
                ParentFirstName = "Abdi",
                ParentLastName = "Warsame",
                ParentPhoneNumber = "123-456-7890"
            },
            TeacherInfo = new TeacherInfoDto
            {
                TeacherFirstName = "Abdisalam",
                TeacherLastName = "Yusuf",
                TeacherPhoneNumber = "321-654-0987",
                Institution = "Al-Hikmah Center"
            },
            AgreedToTerms = true
        },
        new FormDto
        {
            PersonalInfo = new PersonalInfoDto
            {
                FirstName = "Fartun",
                MiddleName = "Ali",
                LastName = "Walid",
                Gender = Gender.Female,
                DOB = new DateOnly(2016, 6, 16)
            },
            AddressInfo = new AddressInfoDto
            {
                Country = "Canada",
                StateProvince = "Ontario",
                City = "Toronto"
            },
            CompetitionInfo = new CompetitionInfoDto
            {
                Division = "Best Voice",
                Category = "13 Years Old & Older",
                Portion = "NA"
            },
            ParentInfo = new ParentInfoDto
            {
                ParentFirstName = "John",
                ParentLastName = "Smith",
                ParentPhoneNumber = "111-222-3333"
            },
            TeacherInfo = new TeacherInfoDto
            {
                TeacherFirstName = "Farah",
                TeacherLastName = "Mohamed",
                TeacherPhoneNumber = "444-555-6666",
                Institution = "Knowledge Academy"
            },
            AgreedToTerms = true
        },
        new FormDto
        {
            PersonalInfo = new PersonalInfoDto
            {
                FirstName = "Salma",
                MiddleName = "Ahmed",
                LastName = "Hirsi",
                Gender = Gender.Female,
                DOB = new DateOnly(2015, 4, 4)
            },
            AddressInfo = new AddressInfoDto
            {
                Country = "Mexico",
                StateProvince = "Chihuahua",
                City = "Ciudad Juarez"
            },
            CompetitionInfo = new CompetitionInfoDto
            {
                Division = "Ten Qira'at",
                Category = "10 Juz",
                Portion = "Top"
            },
            ParentInfo = new ParentInfoDto
            {
                ParentFirstName = "Jabir",
                ParentLastName = "Hassan",
                ParentPhoneNumber = "777-888-9999"
            },
            TeacherInfo = new TeacherInfoDto
            {
                TeacherFirstName = "Suleykha",
                TeacherLastName = "Tariq",
                TeacherPhoneNumber = "000-111-2222",
                Institution = "Skills Center"
            },
            AgreedToTerms = true
        },
        new FormDto
        {
            PersonalInfo = new PersonalInfoDto
            {
                FirstName = "Ahmed",
                MiddleName = "Salman",
                LastName = "Ali",
                Gender = Gender.Male,
                DOB = new DateOnly(2017, 2, 2)
            },
            AddressInfo = new AddressInfoDto
            {
                Country = "Mexico",
                StateProvince = "Chihuahua",
                City = "Ciudad Juarez"
            },
            CompetitionInfo = new CompetitionInfoDto
            {
                Division = "Best Voice",
                Category = "12 Years Old & Younger",
                Portion = "NA"
            },
            ParentInfo = new ParentInfoDto
            {
                ParentFirstName = "Wahid",
                ParentLastName = "Abdi",
                ParentPhoneNumber = "777-888-9999"
            },
            TeacherInfo = new TeacherInfoDto
            {
                TeacherFirstName = "Khalid",
                TeacherLastName = "Isse",
                TeacherPhoneNumber = "000-111-2222",
                Institution = "Skills Center"
            },
            AgreedToTerms = true
        }
    };
    public static void PrefillFields(FormDto form)
    {
        currentIndex = (currentIndex + 1) % FakeData.Count;

        var nextData = FakeData[currentIndex];

        form.PersonalInfo = nextData.PersonalInfo;
        form.AddressInfo = nextData.AddressInfo;
        form.CompetitionInfo = nextData.CompetitionInfo;
        form.ParentInfo = nextData.ParentInfo;
        form.TeacherInfo = nextData.TeacherInfo;
        form.AgreedToTerms = nextData.AgreedToTerms;
    }
}
