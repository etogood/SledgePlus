using System.Collections.ObjectModel;

namespace SledgePlus.WPF.ViewModels.UserControls.Custom;

public class ExpanderLessonItemViewModel : ViewModel
{
    private string _header;
    public string Header
    {
        get => _header;
        set => Set(ref _header, value);
    }

    private ObservableCollection<LessonItemViewModel> _innerItems;
    public ObservableCollection<LessonItemViewModel> InnerItems
    {
        get => _innerItems;
        set => Set(ref _innerItems, value);
    }


    public ExpanderLessonItemViewModel(IHost host)
    {
        Header = "NO_HEADER";
    }
}