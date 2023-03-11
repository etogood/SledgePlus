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


    public LessonItemViewModel(IHost host)
    {
        
    }
}