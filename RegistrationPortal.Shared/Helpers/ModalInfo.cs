namespace RegistrationPortal.Shared.Helpers;

public static class ModalInfo
{
    public static NotificationText DuplicateEntry()
    {
        string title = "Duplicate Entry Detected";
        string message = "We're sorry, but it appears that the information you have entered matches an existing form. " +
            "Please contact us if you continue to face this issue.";

        return new NotificationText(title, message);
    }

    public static NotificationText AgeEligibilityError()
    {
        string title = "Age Requirement Not Met";
        string message = "We're sorry, but you do not meet the age requirements for the category you have registered for. " +
            "Please review the eligibility criteria for all categories and select one that you are eligible for.";

        return new NotificationText(title, message);
    }
}
