using RegistrationPortal.DTOs;

namespace RegistrationPortal.Extensions;

public static class FormApiExtensions
{
    public static WebApplication MapFormApi(this WebApplication app)
    {
        app.MapGet("/api/forms/verified", async (IFormData formData) =>
        {
            var allForms = await formData.GetForms();
            var verified = allForms
                .Where(f => f.StatusInfo.Status == "Verified")
                .Select(f => new VerifiedFormDto
                {
                    RID = f.Id,
                    FirstName = f.PersonalInfo.FirstName,
                    MiddleName = f.PersonalInfo.MiddleName,
                    LastName = f.PersonalInfo.LastName,
                    Gender = f.PersonalInfo.Gender,
                    DOB = f.PersonalInfo.DOB,
                    Country = f.AddressInfo.Country,
                    Region = f.AddressInfo.StateProvince,
                    City = f.AddressInfo.City,
                    Division = f.CompetitionInfo.Division,
                    Category = f.CompetitionInfo.Category,
                    Portion = f.CompetitionInfo.Portion,
                    PrelimScheduledDate = f.EventScheduleInfo.PrelimScheduledDate,
                    PrelimScheduledTime = f.EventScheduleInfo.PrelimScheduledTime
                })
                .ToList();

            return Results.Ok(verified);
        })
        .AllowAnonymous()
        .WithName("GetVerifiedForms")
        .Produces<List<VerifiedFormDto>>(StatusCodes.Status200OK);

        return app;
    }
}