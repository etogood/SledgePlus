using System.Windows.Input;

namespace SledgePlus.WPF.ViewModels.UserControls.Custom;

public class LessonItemViewModel : ViewModel
{
    private string _label;

    public string Label
    {
        get => _label;
        set => Set(ref _label, value);
    }

    private string _description;

    public string Description
    {
        get => _description;
        set => Set(ref _description, value);
    }

    private ICommand? _command;

    public ICommand? Command
    {
        get { return _command; }
        set { _command = value; }
    }

    public LessonItemViewModel(IHost host)
    {
        Label = "NO_LABEL";
        Description = "NO_DESCRIPTION";
        Command = null;
    }
}