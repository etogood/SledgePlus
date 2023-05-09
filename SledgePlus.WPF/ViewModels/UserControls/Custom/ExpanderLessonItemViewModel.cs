using System.Collections.ObjectModel;
using System.Threading.Tasks;

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
        Header = "Загрузка...";
    }

    public Task Build(string header, ObservableCollection<LessonItemViewModel> innerItems)
    {
        Header = header;
        InnerItems = innerItems;
        return Task.CompletedTask;
    }
}