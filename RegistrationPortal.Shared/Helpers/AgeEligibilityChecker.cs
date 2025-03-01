namespace RegistrationPortal.Shared.Helpers;

public static class AgeEligibilityChecker
{
    public static bool CheckAgeEligibility(string division, string category, DateOnly? dob)
    {
        if (dob == null)
        {
            throw new AgeEligibilityException("Age Validation Error: Invalid Date of Birth");
        }

        DateOnly referenceDate = new DateOnly(2024, 12, 31);
        int ageInYears = CalculateAge(referenceDate, dob.Value);
        int minAge = GetMinimumAge(division, category);

        return ageInYears < minAge;
    }

    private static int CalculateAge(DateOnly referenceDate, DateOnly dob)
    {
        int ageInYears = referenceDate.Year - dob.Year;

        if (referenceDate < dob.AddYears(ageInYears))
        {
            ageInYears--;
        }

        return ageInYears;
    }

    private static int GetMinimumAge(string division, string category)
    {
        int minAge;

        if (division == "Memorization")
        {
            switch (category)
            {
                case "30 Juz":
                    minAge = 25;
                    break;
                case "20 Juz":
                    minAge = 23;
                    break;
                case "15 Juz":
                    minAge = 20;
                    break;
                case "10 Juz":
                    minAge = 16;
                    break;
                case "5 Juz":
                    minAge = 12;
                    break;
                case "1 Juz":
                    minAge = 8;
                    break;
                default:
                    minAge = int.MaxValue;
                    break;
            }
        }
        else if (division == "Ten Qira'at")
        {
            switch (category)
            {
                case "30 Juz":
                    minAge = 40;
                    break;
                case "15 Juz":
                case "5 Juz":
                    minAge = 35;
                    break;
                default:
                    minAge = int.MaxValue;
                    break;
            }
        }
        else if (division == "Best Voice")
        {
            switch (category)
            {
                case "13 Years Old & Older":
                    minAge = 25;
                    break;
                case "12 Years Old & Younger":
                    minAge = 12;
                    break;
                default:
                    minAge = int.MaxValue;
                    break;
            }
        }
        else if (division == "Islamic Studies")
        {
            switch (category)
            {
                case "Islamic Studies Book - 5 Subject":
                    minAge = 18;
                    break;
                default:
                    minAge = int.MaxValue;
                    break;
            }
        }
        else
        {
            throw new AgeEligibilityException("Age Validation Error: Invalid Division");
        }

        return minAge + 1;
    }
}
