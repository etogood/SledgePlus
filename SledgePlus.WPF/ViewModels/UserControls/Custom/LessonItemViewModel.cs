using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;

namespace SledgePlus.WPF.ViewModels.UserControls.Custom;

public class LessonItemViewModel : ViewModel
{
    public int Id { get; set; }

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

    private Brush _backgroundColor;
    public Brush BackgroundColor
    {
        get => _backgroundColor;
        set => Set(ref _backgroundColor, value);
    }

    
    public LessonItemViewModel(IHost host)
    {
        Label = "NO_LABEL";
        Description = "NO_DESCRIPTION";
        BackgroundColor = new SolidColorBrush(Color.FromRgb(255, 235, 59));
        Command = null;
    }
}