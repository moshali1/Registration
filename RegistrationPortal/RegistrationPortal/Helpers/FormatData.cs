using Microsoft.Graph.Models;
using System.Text.RegularExpressions;

/// <summary>
/// The FormatData class provides utility methods for formatting various pieces of form-related data.
/// This includes capitalizing the first letters of names, formatting phone numbers, and other text transformation tasks.
/// It is designed to be used for cleaning and standardizing data in 'Form' objects, primarily for storage or display purposes.
/// </summary>
namespace RegistrationPortal.Shared.Helpers;
public static class FormatData
{
    public static void FormatFormData(Form form)
    {
        form.PersonalInfo.FirstName = RemoveLeadingAndTrailingSpaces(form.PersonalInfo.FirstName);
        form.PersonalInfo.MiddleName = RemoveLeadingAndTrailingSpaces(form.PersonalInfo.MiddleName);
        form.PersonalInfo.LastName = RemoveLeadingAndTrailingSpaces(form.PersonalInfo.LastName);

        form.PersonalInfo.FirstName = CapitalizeFirstLetterOnly(form.PersonalInfo.FirstName);
        form.PersonalInfo.MiddleName = CapitalizeFirstLetterOnly(form.PersonalInfo.MiddleName);
        form.PersonalInfo.LastName = CapitalizeFirstLetterOnly(form.PersonalInfo.LastName);

        form.ParentInfo.ParentFirstName = CapitalizeFirstLetterOnly(form.ParentInfo.ParentFirstName);
        form.ParentInfo.ParentLastName = CapitalizeFirstLetterOnly(form.ParentInfo.ParentLastName);
        form.TeacherInfo.TeacherFirstName = CapitalizeFirstLetterOnly(form.TeacherInfo.TeacherFirstName);
        form.TeacherInfo.TeacherLastName = CapitalizeFirstLetterOnly(form.TeacherInfo.TeacherLastName);

        form.AddressInfo.StateProvince = CapitalizeFirstLetter(form.AddressInfo.StateProvince);
        form.AddressInfo.City = CapitalizeFirstLetter(form.AddressInfo.City);
        form.TeacherInfo.Institution = CapitalizeFirstLetter(form.TeacherInfo.Institution);

        form.ParentInfo.ParentPhoneNumber = FormatPhoneNumber(form.ParentInfo.ParentPhoneNumber);
        form.TeacherInfo.TeacherPhoneNumber = FormatPhoneNumber(form.TeacherInfo.TeacherPhoneNumber);
    }

    public static string CapitalizeFirstLetterOnly(string inputString)
    {
        if (string.IsNullOrEmpty(inputString))
        {
            return inputString;
        }

        return char.ToUpper(inputString[0]) + inputString.Substring(1).ToLower();
    }

    public static string CapitalizeFirstLetter(string inputString)
    {
        if (string.IsNullOrEmpty(inputString))
        {
            return inputString;
        }

        return char.ToUpper(inputString[0]) + inputString.Substring(1);
    }

    public static string FormatPhoneNumber(string phoneNumber)
    {
        string formattedNumber = string.Empty;
        if (!string.IsNullOrWhiteSpace(phoneNumber))
        {
            string cleanedPhoneNumber = Regex.Replace(phoneNumber, @"[^\d]", "");
            if (cleanedPhoneNumber.Length == 10)
            {
                formattedNumber = "(" + cleanedPhoneNumber.Substring(0, 3) + ") " + cleanedPhoneNumber.Substring(3, 3) + "-" + cleanedPhoneNumber.Substring(6, 4);
            }
            else
            {
                Console.WriteLine("Invalid phone number");
                formattedNumber = phoneNumber + " " + "Format Error";
            }
        }
        return formattedNumber;
    }

    public static string RemoveLeadingAndTrailingSpaces(string inputString)
    {
        if (string.IsNullOrEmpty(inputString))
        {
            return inputString;
        }
        return inputString.Trim();
    }
}
