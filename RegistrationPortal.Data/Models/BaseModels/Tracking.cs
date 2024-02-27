namespace RegistrationPortal.Data.Models;
public class Tracking
{
    public string PerformedById { get; set; }
    public string FullName { get; set; }
    public string DisplayName { get; set; }
    public string UserRole { get; set; }
    public string TrackingAction { get; set; }
    public List<string> Descriptions { get; set; } = new();
    public bool IsVisible { get; set; } = true;
    public DateTime ActionDateTime { get; set; } = DateTime.UtcNow;
}
