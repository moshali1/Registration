using RegistrationPortal.Data.Models.ViewModels;

namespace RegistrationPortal.Services;

public class SchedulingService : ISchedulingService
{
    private readonly IScheduleSlotData _slotData;
    private readonly IScheduleSettingsData _settingsData;
    private readonly IFormData _formData;

    public SchedulingService(
        IScheduleSlotData slotData,
        IScheduleSettingsData settingsData,
        IFormData formData)
    {
        _slotData = slotData;
        _settingsData = settingsData;
        _formData = formData;
    }

    public async Task<ScheduleViewModel> GetScheduleViewModelAsync(string formId, string division, string category)
    {
        var settings = await _settingsData.GetSettingsAsync();
        var form = await _formData.GetForm(formId);

        if (form == null)
        {
            return new ScheduleViewModel
            {
                Error = "Form not found."
            };
        }

        // If division and category are not provided, use the form's values
        division = string.IsNullOrEmpty(division) ? form.CompetitionInfo.Division : division;
        category = string.IsNullOrEmpty(category) ? form.CompetitionInfo.Category : category;

        var slots = await _slotData.GetAvailableSlotsAsync(division, category);

        var viewModel = new ScheduleViewModel
        {
            Settings = settings,
            FormId = formId,
            Division = division,
            Category = category,
            SelectedSlotId = form.EventScheduleInfo.ScheduleSlotId,
            AvailableSlots = slots.Select(s => new ScheduleSlotViewModel
            {
                Id = s.Id,
                Date = s.Date,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                MaxParticipants = s.MaxParticipants,
                CurrentParticipants = s.CurrentParticipants,
                IsEnabled = s.IsEnabled
                // IsAvailable is a read-only property that will be calculated automatically
            }).ToList()
        };

        return viewModel;
    }

    public async Task<bool> ReserveSlotAsync(string formId, string slotId)
    {
        var form = await _formData.GetForm(formId);
        if (form == null) return false;

        var result = await _slotData.ReserveSlotAsync(slotId, formId);

        return result;
    }

    public async Task<bool> CancelReservationAsync(string formId)
    {
        var form = await _formData.GetForm(formId);
        if (form == null || string.IsNullOrEmpty(form.EventScheduleInfo.ScheduleSlotId))
        {
            return false;
        }

        var slotId = form.EventScheduleInfo.ScheduleSlotId;
        var division = form.CompetitionInfo.Division;
        var category = form.CompetitionInfo.Category;

        var result = await _slotData.CancelReservationAsync(slotId, formId);

        return result;
    }

    public async Task<List<ScheduleSlotViewModel>> GetAvailableSlotsForFormAsync(string formId)
    {
        var form = await _formData.GetForm(formId);
        if (form == null)
        {
            return new List<ScheduleSlotViewModel>();
        }

        var slots = await _slotData.GetAvailableSlotsAsync(
            form.CompetitionInfo.Division, form.CompetitionInfo.Category);

        return slots.Select(s => new ScheduleSlotViewModel
        {
            Id = s.Id,
            Date = s.Date,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            MaxParticipants = s.MaxParticipants,
            CurrentParticipants = s.CurrentParticipants,
            IsEnabled = s.IsEnabled
            // IsAvailable is a read-only property that will be calculated automatically
        }).ToList();
    }

    public async Task<bool> IsPrelimScheduleSelectedAsync(string formId)
    {
        var form = await _formData.GetForm(formId);
        return form != null &&
               !string.IsNullOrEmpty(form.EventScheduleInfo.ScheduleSlotId) &&
               !string.IsNullOrEmpty(form.EventScheduleInfo.PrelimScheduledDate) &&
               !string.IsNullOrEmpty(form.EventScheduleInfo.PrelimScheduledTime);
    }

    public async Task<List<ScheduleSlot>> GetAllSlotsAsync()
    {
        return await _slotData.GetAllSlotsAsync();
    }

    public async Task<ScheduleSlot> GetSlotByIdAsync(string id)
    {
        return await _slotData.GetSlotByIdAsync(id);
    }

    public async Task CreateSlotAsync(ScheduleSlot slot)
    {
        await _slotData.CreateSlotAsync(slot);
    }

    public async Task UpdateSlotAsync(ScheduleSlot slot)
    {
        await _slotData.UpdateSlotAsync(slot);

    }

    public async Task DeleteSlotAsync(string id)
    {
        var slot = await _slotData.GetSlotByIdAsync(id);
        if (slot == null) return;

        await _slotData.DeleteSlotAsync(id);
    }

    public async Task<ScheduleSettings> GetScheduleSettingsAsync()
    {
        return await _settingsData.GetSettingsAsync();
    }

    public async Task UpdateScheduleSettingsAsync(ScheduleSettings settings)
    {
        await _settingsData.UpdateSettingsAsync(settings);

        // This affects all schedules, so notify broadly
        // In a real system, you might want to be more targeted with which groups get notified
        var slots = await _slotData.GetAllSlotsAsync();
        var divisions = slots.SelectMany(s => s.Divisions).Distinct();
        var categories = slots.SelectMany(s => s.Categories).Distinct();

    }

    public async Task AssignRandomSlotsToUnscheduledFormsAsync()
    {
        // Get all forms that don't have a schedule yet
        var forms = await _formData.GetForms();
        var unscheduledForms = forms.Where(f =>
            string.IsNullOrEmpty(f.EventScheduleInfo.ScheduleSlotId) &&
            f.StatusInfo.Status != "Withdrawn" &&
            f.StatusInfo.Status != "Disqualified").ToList();

        if (unscheduledForms.Count == 0)
        {
            return;
        }

        // Get all available slots
        var allSlots = await _slotData.GetAllSlotsAsync();
        var availableSlots = allSlots.Where(s =>
            s.IsEnabled && s.CurrentParticipants < s.MaxParticipants).ToList();

        if (availableSlots.Count == 0)
        {
            throw new InvalidOperationException("No available slots for assignment.");
        }

        // Create a dictionary to track slots by division and category
        var slotsByDivisionCategory = new Dictionary<string, List<ScheduleSlot>>();

        foreach (var slot in availableSlots)
        {
            foreach (var division in slot.Divisions.Count > 0 ? slot.Divisions : forms.Select(f => f.CompetitionInfo.Division).Distinct())
            {
                foreach (var category in slot.Categories.Count > 0 ? slot.Categories : forms.Where(f => f.CompetitionInfo.Division == division).Select(f => f.CompetitionInfo.Category).Distinct())
                {
                    var key = $"{division}_{category}";
                    if (!slotsByDivisionCategory.ContainsKey(key))
                    {
                        slotsByDivisionCategory[key] = new List<ScheduleSlot>();
                    }

                    slotsByDivisionCategory[key].Add(slot);
                }
            }
        }

        // Random assignment
        var random = new Random();
        var assignmentResults = new List<(Form Form, bool Success)>();
        var updatedCategories = new HashSet<string>(); // Track which category groups need to be notified

        foreach (var form in unscheduledForms)
        {
            var key = $"{form.CompetitionInfo.Division}_{form.CompetitionInfo.Category}";

            if (!slotsByDivisionCategory.ContainsKey(key) || slotsByDivisionCategory[key].Count == 0)
            {
                // No applicable slots for this form
                assignmentResults.Add((form, false));
                continue;
            }

            // Get applicable slots and shuffle them
            var applicableSlots = slotsByDivisionCategory[key]
                .Where(s => s.CurrentParticipants < s.MaxParticipants)
                .OrderBy(x => random.Next())
                .ToList();

            if (applicableSlots.Count == 0)
            {
                // No slots with available capacity
                assignmentResults.Add((form, false));
                continue;
            }

            // Try to reserve a slot
            var success = false;
            foreach (var slot in applicableSlots)
            {
                if (await _slotData.ReserveSlotAsync(slot.Id, form.Id))
                {
                    // Update the tracking for this slot
                    var updatedSlot = await _slotData.GetSlotByIdAsync(slot.Id);
                    if (updatedSlot.CurrentParticipants >= updatedSlot.MaxParticipants)
                    {
                        // Remove this slot from all division/category lists
                        foreach (var list in slotsByDivisionCategory.Values)
                        {
                            list.RemoveAll(s => s.Id == slot.Id);
                        }
                    }

                    success = true;
                    updatedCategories.Add(key);
                    break;
                }
            }

            assignmentResults.Add((form, success));
        }

        // Return or log results as needed
        var successCount = assignmentResults.Count(r => r.Success);
        var failureCount = assignmentResults.Count - successCount;

        if (failureCount > 0)
        {
            // Log or handle failures
            Console.WriteLine($"Failed to assign slots for {failureCount} forms.");
        }
    }
}