namespace RegistrationPortal.Shared.InputSelectionOptions;
public static class CountryOptions
{
    public static List<SelectionOption> Countries { get; } = new()
    {
        new SelectionOption { Value = "United States", Text = "United States" },
        new SelectionOption { Value = "Canada", Text = "Canada" },
        new SelectionOption { Value = "Mexico", Text = "Mexico" }
    };
}
