using System.Collections.ObjectModel;
using SledgePlus.WPF.ViewModels.UserControls.Custom;

namespace SledgePlus.WPF.ViewModels.UserControls;

public class LearningMenuViewModel : ViewModel
{
    private ObservableCollection<ExpanderLessonItemViewModel> _sections;
    public ObservableCollection<ExpanderLessonItemViewModel> Sections
    {
        get => _sections;
        set => Set(ref _sections, value);
    }

    public LearningMenuViewModel(IHost host)
    {
        Sections = new ObservableCollection<ExpanderLessonItemViewModel>();

        Sections.Add(new ExpanderLessonItemViewModel(host)
        {
            Header = "Базовые концепты",
            InnerItems = new ObservableCollection<LessonItemViewModel>
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
            }
        });

    }
}