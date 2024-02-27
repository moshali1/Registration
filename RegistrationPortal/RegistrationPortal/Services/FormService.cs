using AutoMapper;
using RegistrationPortal.CustomExceptions;
/// <summary>
/// The FormService class encapsulates business logic related to form operations. It provides functionality for CRUD operations,
/// form validation, age eligibility checking, and duplicate detection. This service relies on the IFormData interface for
/// data manipulation, presumably using MongoDB as the data storage mechanism.
/// </summary>
namespace RegistrationPortal.Services;
public class FormService : IFormService
{
    private readonly IFormData _formData;
    private readonly IMapper _mapper;

    public FormService(IFormData formData, IMapper mapper)
    {
        _formData = formData;
        _mapper = mapper;
    }

    //public async Task<List<FormDto>> GetForms()
    //{
    //    var forms = await _formData.GetForms();
    //    return _mapper.Map<List<FormDto>>(forms);
    //}

    public async Task<List<FormDto>> GetFormsByCreator(string creatorId)
    {
        var forms = await _formData.GetFormsByCreator(creatorId);
        return _mapper.Map<List<FormDto>>(forms);
    }

    public async Task<FormDto> GetForm(string id)
    {
        var form = await _formData.GetForm(id);
        return _mapper.Map<FormDto>(form);
    }

    public async Task CreateForm(FormDto formDto)
    {
        var form = _mapper.Map<Form>(formDto);
        await FormOperations(form);
        await _formData.CreateForm(form);
    }

    public async Task UpdateForm(FormDto formDto)
    {
        var form = _mapper.Map<Form>(formDto);
        await FormOperations(form);
        await _formData.UpdateForm(form);
    }

    public async Task DeleteForm(string id) =>
        await _formData.DeleteForm(id);

    private async Task FormOperations(Form form)
    {
        // Format the form data to adhere to standardized structures.
        // Perform before duplicate check (case sensitive)
        FormatData.FormatFormData(form);

        // Validate the form's data and throw a custom exception if invalid.
        (bool isValid, string validationMessage) = FormDataValidator.Validate(form);
        if (isValid == false)
        {
            throw new FormDataValidationException($"Form validation failed: {validationMessage}");
        }

        // Verify the age eligibility criteria for the form's category.
        bool isEligible = AgeEligibilityChecker.CheckAgeEligibility(form.CompetitionInfo.Division, form.CompetitionInfo.Category, form.PersonalInfo.DOB);
        if (isEligible == false)
        {
            throw new AgeEligibilityException("The applicant does not meet the age requirements for the chosen category.");
        }

        // Check for duplicate forms based on specific criteria.
        bool isDuplicate = await _formData.DoesDuplicateExist(form);
        if (isDuplicate == true)
        {
            throw new DuplicateFormException("A form with the same personal and competition details already exists.");
        }
    }
}