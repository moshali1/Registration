namespace RegistrationPortal.Shared.InputSelectionOptions;

public class CompetitionOptions
{
    public static List<DivisionOption> Divisions { get; private set; }

    static CompetitionOptions()
    {
        Divisions = new List<DivisionOption>
            {
                new DivisionOption
                {
                    DivisionName = "Memorization",
                    Categories = new List<CategoryOption>
                    {
                        new CategoryOption { CategoryName = "1 Juz", Portions = new List<string> { "NA" } },
                        new CategoryOption { CategoryName = "5 Juz", Portions = new List<string> { "Top", "Bottom" } },
                        new CategoryOption { CategoryName = "10 Juz", Portions = new List<string> { "Top", "Bottom" } },
                        new CategoryOption { CategoryName = "15 Juz", Portions = new List<string> { "Top", "Bottom" } },
                        new CategoryOption { CategoryName = "20 Juz", Portions = new List<string> { "Top", "Bottom" } },
                        new CategoryOption { CategoryName = "30 Juz", Portions = new List<string> { "NA" } }
                    }
                },
                new DivisionOption
                {
                    DivisionName = "Ten Qira'at",
                    Categories = new List<CategoryOption>
                    {
                        new CategoryOption { CategoryName = "5 Juz", Portions = new List<string> { "Top" } },
                        new CategoryOption { CategoryName = "15 Juz", Portions = new List<string> { "Top" } },
                        new CategoryOption { CategoryName = "30 Juz", Portions = new List<string> { "Top" } }
                    }
                },
                new DivisionOption
                {
                    DivisionName = "Best Voice",
                    Categories = new List<CategoryOption>
                    {
                        new CategoryOption { CategoryName = "13 Years Old & Older", Portions = new List<string> { "NA" } },
                        new CategoryOption { CategoryName = "12 Years Old & Younger", Portions = new List<string> { "NA" } },
                    }
                },
                new DivisionOption
                {
                    DivisionName = "Islamic Studies",
                    Categories = new List<CategoryOption>
                    {
                        new CategoryOption { CategoryName = "Islamic Studies Book - 5 Subject", Portions = new List<string> { "NA" } }
                    }
                }
            };
    }
}

public class DivisionOption
{
    public string DivisionName { get; set; }
    public List<CategoryOption> Categories { get; set; }
}

public class CategoryOption
{
    public string CategoryName { get; set; }
    public List<string> Portions { get; set; }
}