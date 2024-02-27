using Gender = RegistrationPortal.Data.Models.Gender;

namespace RegistrationPortal.Helpers;

public class MockData
{
    public async Task<List<Form>> GenerateMockForms()
    {
        await Task.Delay(100);

        List<Form> forms = new()
        {
            new Form
            {
                PersonalInfo = new PersonalInfo
                {
                    FirstName = "Khalid",
                    MiddleName = "Abdi",
                    LastName = "Warsame",
                    Gender = Gender.Male,
                    DOB = new DateOnly(2013, 8, 18)
                },
                AddressInfo = new AddressInfo
                {
                    Country = "United States",
                    StateProvince = "CA",
                    City = "Los Angeles"
                },
                CompetitionInfo = new CompetitionInfo
                {
                    Division = "Memorization",
                    Category = "5 Juz",
                    Portion = "Top"
                },
                ParentInfo = new ParentInfo
                {
                    ParentFirstName = "Abdi",
                    ParentLastName = "Warsame",
                    ParentPhoneNumber = "123-456-7890"
                },
                TeacherInfo = new TeacherInfo
                {
                    TeacherFirstName = "Abdisalam",
                    TeacherLastName = "Yusuf",
                    TeacherPhoneNumber = "321-654-0987",
                    Institution = "Al-Hikmah Center"
                },
                StatusInfo = new StatusInfo
                {
                    Status = "Awaiting Review"
                },
                AgreedToTerms = true
            },
            new Form
            {
                PersonalInfo = new PersonalInfo
                {
                    FirstName = "Sarah",
                    MiddleName = "Lynn",
                    LastName = "Smith",
                    Gender = Gender.Female,
                    DOB = new DateOnly(2006, 6, 16)
                },
                AddressInfo = new AddressInfo
                {
                    Country = "Canada",
                    StateProvince = "ON",
                    City = "Toronto"
                },
                CompetitionInfo = new CompetitionInfo
                {
                    Division = "Knowledge",
                    Category = "15 Juz",
                    Portion = "Bottom"
                },
                ParentInfo = new ParentInfo
                {
                    ParentFirstName = "John",
                    ParentLastName = "Smith",
                    ParentPhoneNumber = "111-222-3333"
                },
                TeacherInfo = new TeacherInfo
                {
                    TeacherFirstName = "Emily",
                    TeacherLastName = "Johnson",
                    TeacherPhoneNumber = "444-555-6666",
                    Institution = "Knowledge Academy"
                },
                StatusInfo = new StatusInfo
                {
                    Status = "Awaiting Review"
                },
                AgreedToTerms = true
            },
            new Form
            {
                PersonalInfo = new PersonalInfo
                {
                    FirstName = "Salma",
                    MiddleName = "Ahmed",
                    LastName = "Hirsi",
                    Gender = Gender.Female,
                    DOB = new DateOnly(2008, 4, 4)
                },
                AddressInfo = new AddressInfo
                {
                    Country = "Mexico",
                    StateProvince = "Chihuahua",
                    City = "Ciudad Juarez"
                },
                CompetitionInfo = new CompetitionInfo
                {
                    Division = "Ten Qira'at",
                    Category = "10 Juz",
                    Portion = "Top"
                },
                ParentInfo = new ParentInfo
                {
                    ParentFirstName = "Mark",
                    ParentLastName = "Williams",
                    ParentPhoneNumber = "777-888-9999"
                },
                TeacherInfo = new TeacherInfo
                {
                    TeacherFirstName = "Lucy",
                    TeacherLastName = "Brown",
                    TeacherPhoneNumber = "000-111-2222",
                    Institution = "Skills Center"
                },
                StatusInfo = new StatusInfo
                {
                    Status = "Awaiting Review"
                },
                AgreedToTerms = true
            },
            new Form
            {
                PersonalInfo = new PersonalInfo
                {
                    FirstName = "Ahmed",
                    MiddleName = "Salman",
                    LastName = "Ali",
                    Gender = Gender.Male,
                    DOB = new DateOnly(2002, 2, 2)
                },
                AddressInfo = new AddressInfo
                {
                    Country = "Mexico",
                    StateProvince = "Chihuahua",
                    City = "Ciudad Juarez"
                },
                CompetitionInfo = new CompetitionInfo
                {
                    Division = "Memorization",
                    Category = "10 Juz",
                    Portion = "Top"
                },
                ParentInfo = new ParentInfo
                {
                    ParentFirstName = "Mark",
                    ParentLastName = "Williams",
                    ParentPhoneNumber = "777-888-9999"
                },
                TeacherInfo = new TeacherInfo
                {
                    TeacherFirstName = "Lucy",
                    TeacherLastName = "Brown",
                    TeacherPhoneNumber = "000-111-2222",
                    Institution = "Skills Center"
                },
                StatusInfo = new StatusInfo
                {
                    Status = "Awaiting Review"
                },
                AgreedToTerms = true
            }
        };
        return forms;
    }
}
