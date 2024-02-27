/// <summary>
/// The FormDataValidator class provides methods for validating the fields in a 'Form' object.
/// It performs checks on various properties such as country, state, and competition information.
/// For each type of validation, the class returns a boolean indicating the validity of the data
/// and an error message string to provide feedback.
/// The primary entry point for validation is the 'Validate' method, which aggregates validations
/// for individual sections of the form and returns an overall validity status.
/// </summary>
namespace RegistrationPortal.Helpers;
public static class FormDataValidator
{
    public static (bool, string) Validate(Form form)
    {
        string errorMessage;

        if (!ValidateCountry(form.AddressInfo.Country, out errorMessage))
        {
            return (false, errorMessage);
        }

        if (!ValidateState(form.AddressInfo.Country, form.AddressInfo.StateProvince, out errorMessage))
        {
            return (false, errorMessage);
        }

        if (!ValidateCompetitionInfo(form.CompetitionInfo, out errorMessage))
        {
            return (false, errorMessage);
        }

        if (!form.AgreedToTerms)
        {
            errorMessage = "You must agree to the terms.";
            return (false, errorMessage);
        }

        return (true, string.Empty);
    }

    private static bool ValidateCountry(string country, out string errorMessage)
    {
        var countryOption = CountryOptions.Countries.FirstOrDefault(c => c.Value == country);
        if (countryOption == null)
        {
            errorMessage = "Please choose a valid Country";
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }

    private static bool ValidateState(string country, string state, out string errorMessage)
    {
        if (country == "United States")
        {
            var stateOption = StateOptions.States.FirstOrDefault(s => s.Value == state);
            if (stateOption == null)
            {
                errorMessage = "Please choose a valid State";
                return false;
            }
        }

        errorMessage = string.Empty;
        return true;
    }

    private static bool ValidateCompetitionInfo(CompetitionInfo competitionInfo, out string errorMessage)
    {
        var division = competitionInfo.Division;
        var category = competitionInfo.Category;
        var portion = competitionInfo.Portion;

        var divisionOption = CompetitionOptions.Divisions.FirstOrDefault(d => d.DivisionName == division);
        if (divisionOption == null)
        {
            errorMessage = "Please choose a valid Division.";
            return false;
        }

        var categoryOption = divisionOption.Categories.FirstOrDefault(c => c.CategoryName == category);
        if (categoryOption == null)
        {
            errorMessage = $"Please choose a valid Category for {division}.";
            return false;
        }

        if (!categoryOption.Portions.Contains(portion))
        {
            errorMessage = $"Please choose a valid Portion for {category}.";
            return false;
        }

        errorMessage = string.Empty;
        return true;
    }
}
