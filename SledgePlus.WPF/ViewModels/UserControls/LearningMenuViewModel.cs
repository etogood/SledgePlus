using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Commands.InnerActions;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.ViewModels.UserControls.Custom;

namespace SledgePlus.WPF.ViewModels.UserControls;



public class LearningMenuViewModel : ViewModel
{
    private readonly AppDbContext _appDbContext;
    private readonly ILoginStore _loginStore;

    private ObservableCollection<ExpanderLessonItemViewModel> _sections;

    public ObservableCollection<ExpanderLessonItemViewModel> Sections
    {
        get => _sections;
        set => Set(ref _sections, value);
    }

    public MessageViewModel ErrorMessageViewModel { get; }

    public string ErrorMessage
    {
        set => ErrorMessageViewModel.Message = value;
    }


    public LearningMenuViewModel(IHost host)
    {
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();
        _loginStore = host.Services.GetRequiredService<ILoginStore>();

        ErrorMessageViewModel = host.Services.GetRequiredService<MessageViewModel>();
        Sections = new ObservableCollection<ExpanderLessonItemViewModel>();

        LabelBuilder labelBuilder = new();

        foreach (var section in _appDbContext.Sections.ToList())
        {
            ObservableCollection<LessonItemViewModel> innerItems = new();

            labelBuilder.FirstNumber += 1;
            labelBuilder.SecondNumber = 0;

            foreach (var lesson in _appDbContext.Lessons.Where(x => x.SectionId == section.SectionId).ToList())
            {
                var label = $"{labelBuilder.FirstNumber}.{labelBuilder.SecondNumber+=1} {GetLabelName(lesson)}";
                innerItems.Add(new LessonItemViewModel(host)
                {
                    Id = lesson.LessonId,
                    Label = label,
                    BackgroundColor = GetItemColor(lesson),
                    Description = lesson.LessonDescription,
                    Command = new OpenLessonDocument(host, lesson.LessonDocumentName)
                });
            }

            Sections.Add(new ExpanderLessonItemViewModel(host)
            {
                Header = section.SectionHeader,
                InnerItems = innerItems
            });
        }
    }

    private struct LabelBuilder
    {
        public int FirstNumber;
        public int SecondNumber;
        public string Name;
    }

    private string GetLabelName(Lesson lesson) => lesson.IsPractice ? "Практика" : "Лекция";

    private SolidColorBrush GetItemColor(Lesson lesson) =>
        _appDbContext.LessonUsers.FirstOrDefault(x =>
            _loginStore.CurrentUser != null && lesson.LessonId == x.LessonId &&
            _loginStore.CurrentUser.UserId == x.UserId) != null ? new SolidColorBrush(Color.FromRgb(174, 234, 0)) :
        lesson.IsPractice ? new SolidColorBrush(Color.FromRgb(255, 255, 114)) : new SolidColorBrush(Color.FromRgb(255, 235, 59));
}