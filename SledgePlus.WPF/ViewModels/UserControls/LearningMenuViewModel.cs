using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Commands.InnerActions;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.ViewModels.UserControls.Custom;
using SledgePlus.WPF.Views.Windows;

namespace SledgePlus.WPF.ViewModels.UserControls;



public class LearningMenuViewModel : ViewModel
{
    private readonly AppDbContext _appDbContext;
    private readonly ILoginStore _loginStore;
    private readonly IHost _host;

    private static readonly SolidColorBrush PassedLessonBrush = new(Color.FromRgb(174, 234, 0));
    private static readonly SolidColorBrush LessonBrush = new(Color.FromRgb(255, 235, 59));
    private static readonly SolidColorBrush PracticeBrush = new(Color.FromRgb(255, 255, 114));

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
        _host = host;
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();
        _loginStore = host.Services.GetRequiredService<ILoginStore>();

        ErrorMessageViewModel = host.Services.GetRequiredService<MessageViewModel>();
        
    }

    public async Task Build()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            Sections = new ObservableCollection<ExpanderLessonItemViewModel>();
        });

        LabelBuilder labelBuilder = new();

        foreach (var section in _appDbContext.Sections.ToList())
        {
            ObservableCollection<LessonItemViewModel> innerItems = new();

            labelBuilder.FirstNumber += 1;
            labelBuilder.SecondNumber = 0;

            foreach (var lesson in _appDbContext.Lessons.Where(x => x.SectionId == section.SectionId).ToList())
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var label = $"{labelBuilder.FirstNumber}.{labelBuilder.SecondNumber += 1} {GetLabelName(lesson)}";
                    var item = new LessonItemViewModel(_host);
                    Task.Run(() => item.Build(lesson.LessonId, label, lesson.LessonDescription, new OpenLessonDocument(_host, lesson.LessonDocumentName, lesson.IsPractice), Task.Run(() => GetItemColor(lesson)).Result));
                    innerItems.Add(item);
                });
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                var item = new ExpanderLessonItemViewModel(_host);
                Task.Run(() => item.Build(section.SectionHeader, innerItems));
                Sections.Add(item);
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

    private async Task<SolidColorBrush> GetItemColor(Lesson lesson)
    {
        await using var context = _host.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<AppDbContext>();
        var list = await context.LessonUsers.ToListAsync();
        var check = list.FirstOrDefault(x =>
                _loginStore.CurrentUser != null && lesson.LessonId == x.LessonId &&
                _loginStore.CurrentUser.UserId == x.UserId);

        return check != null ? PassedLessonBrush :
            lesson.IsPractice ? PracticeBrush :
            LessonBrush;
    }
}