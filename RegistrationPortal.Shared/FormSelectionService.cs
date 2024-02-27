namespace RegistrationPortal.Shared;
public class FormSelectionService
{
    //public FormDto SelectedForm { get; private set; }
    public FormDto SelectedForm { get; set; }

    public event Action OnFormSelected;

    public void SelectForm(FormDto form)
    {
        SelectedForm = form;
        OnFormSelected?.Invoke();
    }
}
