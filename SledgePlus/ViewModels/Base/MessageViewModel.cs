using System.Windows.Media;

namespace SledgePlus.WPF.ViewModels.Base;

public class MessageViewModel : ViewModel
{
    private string _message;
    public string Message
    {
        get => _message;
        set => Set(ref _message, value);
    }

    private SolidColorBrush _color = Brushes.Black;
    public SolidColorBrush Color
    {
        get => _color;
        set => Set(ref _color, value);
    }

    public bool HasMessage => !string.IsNullOrEmpty(_message);
}