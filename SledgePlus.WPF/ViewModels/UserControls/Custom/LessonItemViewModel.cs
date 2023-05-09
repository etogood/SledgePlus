using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data.Models;
using SledgePlus.WPF.Commands.InnerActions;

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
        Label = "Загрузка...";
        Description = "Загрузка...";
        BackgroundColor = new SolidColorBrush(Color.FromRgb(255, 235, 59));
        Command = null;
    }

    public Task Build(int id, string label, string description, ICommand command, Brush backgroundColor)
    {
        Id = id;
        Label = label;
        BackgroundColor = backgroundColor;
        Description = description;
        Command = command;

        return Task.CompletedTask;
    }
}