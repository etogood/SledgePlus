using System.Collections.ObjectModel;
using SledgePlus.WPF.ViewModels.UserControls.Custom;

namespace SledgePlus.WPF.ViewModels.UserControls;

public class LearningMenuViewModel : ViewModel
{
    private ObservableCollection<LessonItemViewModel> _basicConceptsItems;
    public ObservableCollection<LessonItemViewModel> BasicConceptsItems
    {
        get => _basicConceptsItems;
        set => Set(ref _basicConceptsItems, value);
    }

    public LearningMenuViewModel(IHost host)
    {
        BasicConceptsItems = new ObservableCollection<LessonItemViewModel>
        {
            new(host)
            {
                Label = "1.1 Лекция",
                Description = "Добро пожаловать в C++"
            },

            new(host)
            {
                Label = "1.2 Практика",
                Description = "Не добро пожаловать в C++"
            },
            new(host)
            {
                Label = "1.3 Лекция",
                Description = "АААААААААА Не добро пожаловать в C++"
            },
            new(host)
            {
                Label = "1.3 Лекция",
                Description = "АААААААААА Не добро пожаловать в C++"
            },
            new(host)
            {
                Label = "1.3 Лекция",
                Description = "АААААААААА Не добро пожаловать в C++"
            },
            new(host)
            {
                Label = "1.3 Лекция",
                Description = "АААААААААА Не добро пожаловать в C++"
            },
            new(host)
            {
                Label = "1.3 Лекция",
                Description = "АААААААААА Не добро пожаловать в C++"
            },
        };
    }
}