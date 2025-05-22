namespace RegistrationPortal.Helpers;

public class SchedulingAuditService
{
    private readonly IFormData _formData;
    private readonly IScheduleSlotData _slotData;

    public SchedulingAuditService(IFormData formData, IScheduleSlotData slotData)
    {
        _formData = formData;
        _slotData = slotData;
    }

    public class SchedulingAuditResult
    {
        public int TotalVerifiedForms { get; set; }
        public int TotalScheduledForms { get; set; }
        public int TotalSlotParticipantCount { get; set; }
        public int Discrepancy => TotalScheduledForms - TotalSlotParticipantCount;
        public List<SlotDiscrepancy> SlotDiscrepancies { get; set; } = new List<SlotDiscrepancy>();
        public List<FormWithoutValidSlot> OrphanedForms { get; set; } = new List<FormWithoutValidSlot>();
        public List<FormWithMismatchedSchedule> MismatchedForms { get; set; } = new List<FormWithMismatchedSchedule>();
    }

    public class SlotDiscrepancy
    {
        public ScheduleSlot Slot { get; set; }
        public int ActualParticipantCount { get; set; }
        public int Discrepancy => Slot.CurrentParticipants - ActualParticipantCount;
        public List<string> ParticipantFormIds { get; set; } = new List<string>();
    }

    public class FormWithoutValidSlot
    {
        public string FormId { get; set; }
        public string PersonName { get; set; }
        public string SlotId { get; set; }
        public string ScheduledDate { get; set; }
        public string ScheduledTime { get; set; }
    }

    public class FormWithMismatchedSchedule
    {
        public string FormId { get; set; }
        public string PersonName { get; set; }
        public string SlotId { get; set; }
        public string FormScheduledDate { get; set; }
        public string FormScheduledTime { get; set; }
        public string SlotScheduledDate { get; set; }
        public string SlotScheduledTime { get; set; }
    }

    public async Task<SchedulingAuditResult> AuditSchedulingAsync(string division = "Memorization")
    {
        var result = new SchedulingAuditResult();

        // Get all forms in the specified division with Verified status
        var allForms = await _formData.GetForms();
        var verifiedForms = allForms.Where(f =>
            f.CompetitionInfo.Division == division &&
            f.StatusInfo.Status == "Verified").ToList();

        result.TotalVerifiedForms = verifiedForms.Count;

        // Get forms that have a schedule slot assigned
        var scheduledForms = verifiedForms.Where(f =>
            !string.IsNullOrEmpty(f.EventScheduleInfo.ScheduleSlotId)).ToList();

        result.TotalScheduledForms = scheduledForms.Count;

        // Get all slots
        var allSlots = await _slotData.GetAllSlotsAsync();
        result.TotalSlotParticipantCount = allSlots.Sum(s => s.CurrentParticipants);

        // Group forms by slot ID
        var formsBySlot = scheduledForms
            .GroupBy(f => f.EventScheduleInfo.ScheduleSlotId)
            .ToDictionary(g => g.Key, g => g.ToList());

        // Check each slot
        foreach (var slot in allSlots)
        {
            // If the slot has participants according to its counter
            if (slot.CurrentParticipants > 0)
            {
                // Get forms that reference this slot
                formsBySlot.TryGetValue(slot.Id, out var slotForms);
                var actualCount = slotForms?.Count ?? 0;

                // If there's a discrepancy between the slot counter and actual forms
                if (slot.CurrentParticipants != actualCount)
                {
                    var discrepancy = new SlotDiscrepancy
                    {
                        Slot = slot,
                        ActualParticipantCount = actualCount,
                        ParticipantFormIds = slotForms?.Select(f => f.Id).ToList() ?? new List<string>()
                    };

                    result.SlotDiscrepancies.Add(discrepancy);
                }
            }
        }

        // Find forms that reference slots that don't exist anymore and forms with mismatched schedule info
        foreach (var form in scheduledForms)
        {
            var slotId = form.EventScheduleInfo.ScheduleSlotId;
            var slot = allSlots.FirstOrDefault(s => s.Id == slotId);

            if (slot == null)
            {
                result.OrphanedForms.Add(new FormWithoutValidSlot
                {
                    FormId = form.Id,
                    PersonName = form.PersonalInfo.GetFullName(),
                    SlotId = slotId,
                    ScheduledDate = form.EventScheduleInfo.PrelimScheduledDate,
                    ScheduledTime = form.EventScheduleInfo.PrelimScheduledTime
                });
            }
            else
            {
                // Check if form's scheduled date/time matches the slot's date/time
                string slotTimeRange = $"{FormatTime(slot.StartTime)} - {FormatTime(slot.EndTime)}";

                if (form.EventScheduleInfo.PrelimScheduledDate != slot.Date ||
                    form.EventScheduleInfo.PrelimScheduledTime != slotTimeRange)
                {
                    result.MismatchedForms.Add(new FormWithMismatchedSchedule
                    {
                        FormId = form.Id,
                        PersonName = form.PersonalInfo.GetFullName(),
                        SlotId = slotId,
                        FormScheduledDate = form.EventScheduleInfo.PrelimScheduledDate,
                        FormScheduledTime = form.EventScheduleInfo.PrelimScheduledTime,
                        SlotScheduledDate = slot.Date,
                        SlotScheduledTime = slotTimeRange
                    });
                }
            }
        }

        return result;
    }

    public async Task FixSlotDiscrepanciesAsync(List<SlotDiscrepancy> discrepancies)
    {
        foreach (var discrepancy in discrepancies)
        {
            var slot = discrepancy.Slot;

            // Update the slot with the actual participant count
            slot.CurrentParticipants = discrepancy.ActualParticipantCount;

            // Update the slot in the database
            await _slotData.UpdateSlotAsync(slot);
        }
    }

    private string FormatTime(string timeString)
    {
        if (DateTime.TryParse(timeString, out DateTime time))
        {
            return time.ToString("h:mm tt"); // Formats as "10:00 AM"
        }

        // If it's already just a time string like "10:00:00"
        if (timeString != null && timeString.Length >= 5)
        {
            // Try to remove seconds portion if it exists
            int colonCount = timeString.Count(c => c == ':');
            if (colonCount == 2)
            {
                // Has seconds - return just hh:mm
                return timeString.Substring(0, timeString.LastIndexOf(':'));
            }
        }

        return timeString;
    }
}
