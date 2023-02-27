namespace SledgePlus.WPF.ViewModels.Base;

public class MessageViewModel : ViewModel
{
    private string _message;

    public string Message
    {
        get => _message;
        set
        {
            Set(ref _message, value);
            OnPropertyChanged(nameof(HasMessage));
        }
    }

    public bool HasMessage => !string.IsNullOrEmpty(Message);
}