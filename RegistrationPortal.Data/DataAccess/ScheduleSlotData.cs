namespace RegistrationPortal.Data.DataAccess;
public class ScheduleSlotData : IScheduleSlotData
{
    private readonly IMongoCollection<ScheduleSlot> _slots;
    private readonly IMongoCollection<Form> _forms;

    public ScheduleSlotData(IDbConnection db)
    {
        _slots = db.SlotCollection;
        _forms = db.FormCollection;
    }

    public async Task<List<ScheduleSlot>> GetAllSlotsAsync()
    {
        return await _slots.Find(_ => true).ToListAsync();
    }

    public async Task<List<ScheduleSlot>> GetAvailableSlotsAsync(string division = null, string category = null)
    {
        var builder = Builders<ScheduleSlot>.Filter;
        var filter = builder.Eq(s => s.IsEnabled, true);

        if (!string.IsNullOrEmpty(division))
        {
            filter &= (builder.Size(s => s.Divisions, 0) | builder.AnyEq(s => s.Divisions, division));
        }

        if (!string.IsNullOrEmpty(category))
        {
            filter &= (builder.Size(s => s.Categories, 0) | builder.AnyEq(s => s.Categories, category));
        }

        return await _slots.Find(filter).ToListAsync();
    }

    public async Task<ScheduleSlot> GetSlotByIdAsync(string id)
    {
        return await _slots.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<bool> IsSlotAvailableAsync(string id)
    {
        var slot = await GetSlotByIdAsync(id);
        return slot != null && slot.IsEnabled && slot.CurrentParticipants < slot.MaxParticipants;
    }

    public async Task<bool> ReserveSlotAsync(string slotId, string formId)
    {
        // Get the slot and update it using optimistic concurrency
        var slot = await GetSlotByIdAsync(slotId);
        if (slot == null || !slot.IsEnabled || slot.CurrentParticipants >= slot.MaxParticipants)
        {
            return false;
        }

        // Get the current version for optimistic concurrency
        long currentVersion = slot.Version;

        // Update the slot's participant count
        var slotUpdate = Builders<ScheduleSlot>.Update
            .Inc(s => s.CurrentParticipants, 1)
            .Inc(s => s.Version, 1)
            .Set(s => s.ModifiedDate, DateTime.UtcNow);

        // Create filter that checks the ID, version, and ensures current participants is less than max
        var slotFilter = Builders<ScheduleSlot>.Filter.And(
            Builders<ScheduleSlot>.Filter.Eq(s => s.Id, slotId),
            Builders<ScheduleSlot>.Filter.Eq(s => s.Version, currentVersion),
            Builders<ScheduleSlot>.Filter.Where(s => s.CurrentParticipants < s.MaxParticipants)
        );

        var slotResult = await _slots.UpdateOneAsync(slotFilter, slotUpdate);

        if (slotResult.ModifiedCount == 0)
        {
            // The slot may have been modified by someone else
            return false;
        }

        // Get the updated slot
        var updatedSlot = await GetSlotByIdAsync(slotId);
        if (updatedSlot == null)
        {
            // Unexpected: slot was deleted right after we updated it
            return false;
        }

        // Update the form to reference this slot
        var form = await _forms.Find(f => f.Id == formId).FirstOrDefaultAsync();
        if (form == null)
        {
            // If the form update fails, undo the slot update
            var undoUpdate = Builders<ScheduleSlot>.Update
                .Inc(s => s.CurrentParticipants, -1)
                .Inc(s => s.Version, 1)
                .Set(s => s.ModifiedDate, DateTime.UtcNow);

            await _slots.UpdateOneAsync(s => s.Id == slotId, undoUpdate);
            return false;
        }

        // Update the form with the schedule information
        var formUpdate = Builders<Form>.Update
            .Set(f => f.EventScheduleInfo.ScheduleSlotId, slotId)
            .Set(f => f.EventScheduleInfo.ScheduleSelectionTime, DateTime.UtcNow)
            .Set(f => f.EventScheduleInfo.PrelimScheduledDate, updatedSlot.Date)
            .Set(f => f.EventScheduleInfo.PrelimScheduledTime, $"{FormatTime(updatedSlot.StartTime)} - {FormatTime(updatedSlot.EndTime)}");
        //.Set(f => f.EventScheduleInfo.CheckedPrelimSchedule, true); // only if not admin - updated in the front-end

        var formResult = await _forms.UpdateOneAsync(f => f.Id == formId, formUpdate);

        return formResult.ModifiedCount > 0;
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

    public async Task<bool> CancelReservationAsync(string slotId, string formId)
    {
        // Get the form and check if it has this slot reserved
        var form = await _forms.Find(f => f.Id == formId).FirstOrDefaultAsync();
        if (form == null || form.EventScheduleInfo.ScheduleSlotId != slotId)
        {
            return false;
        }

        // Update the form to remove the schedule information
        var formUpdate = Builders<Form>.Update
            .Set(f => f.EventScheduleInfo.ScheduleSlotId, (string)null)
            .Set(f => f.EventScheduleInfo.ScheduleSelectionTime, (DateTime?)null)
            .Set(f => f.EventScheduleInfo.PrelimScheduledDate, (string)null)
            .Set(f => f.EventScheduleInfo.PrelimScheduledTime, (string)null)
            .Set(f => f.EventScheduleInfo.CheckedPrelimSchedule, false);

        var formResult = await _forms.UpdateOneAsync(f => f.Id == formId, formUpdate);

        if (formResult.ModifiedCount == 0)
        {
            return false;
        }

        // Update the slot to decrement the participant count
        var slotUpdate = Builders<ScheduleSlot>.Update
            .Inc(s => s.CurrentParticipants, -1)
            .Inc(s => s.Version, 1)
            .Set(s => s.ModifiedDate, DateTime.UtcNow);

        var slotResult = await _slots.UpdateOneAsync(s => s.Id == slotId, slotUpdate);

        return slotResult.ModifiedCount > 0;
    }

    public async Task CreateSlotAsync(ScheduleSlot slot)
    {
        slot.CreatedDate = DateTime.UtcNow;
        slot.ModifiedDate = DateTime.UtcNow;
        await _slots.InsertOneAsync(slot);
    }

    public async Task UpdateSlotAsync(ScheduleSlot slot)
    {
        // Get the current version for optimistic concurrency
        var existingSlot = await GetSlotByIdAsync(slot.Id);
        if (existingSlot == null)
        {
            throw new Exception($"Slot with ID {slot.Id} not found.");
        }

        // Increase the version
        slot.Version = existingSlot.Version + 1;
        slot.ModifiedDate = DateTime.UtcNow;

        var filter = Builders<ScheduleSlot>.Filter
            .Eq(s => s.Id, slot.Id) &
            Builders<ScheduleSlot>.Filter.Eq(s => s.Version, existingSlot.Version);

        var result = await _slots.ReplaceOneAsync(filter, slot);

        if (result.ModifiedCount == 0)
        {
            throw new Exception("The slot was modified by another user. Please refresh and try again.");
        }
    }

    public async Task DeleteSlotAsync(string id)
    {
        // Check if there are any forms using this slot
        var formsUsingSlot = await _forms.CountDocumentsAsync(f => f.EventScheduleInfo.ScheduleSlotId == id);
        if (formsUsingSlot > 0)
        {
            throw new Exception("Cannot delete a slot that has participants scheduled.");
        }

        await _slots.DeleteOneAsync(s => s.Id == id);
    }

    public async Task<int> GetParticipantCountForSlotAsync(string slotId)
    {
        var formsCount = await _forms.CountDocumentsAsync(f => f.EventScheduleInfo.ScheduleSlotId == slotId);
        return (int)formsCount;
    }
}