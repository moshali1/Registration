namespace RegistrationPortal.Shared;
public class DefaultListService
{
    public char ActiveTab { get; set; } = 'M';

    public event Action OnTabSelected;
    public void LastTab(char activeTab)
    {
        ActiveTab = activeTab;
        OnTabSelected?.Invoke();
    }

    public bool ShowSuccessDialog { get; set; } = false;

    public event Action OnChange;
    public void SetShowDialog(bool show)
    {
        ShowSuccessDialog = show;
        OnChange?.Invoke();
    }
}
