using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

using Microsoft.EntityFrameworkCore;
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
    private readonly IHost _host;

    private static readonly SolidColorBrush PassedLessonBrush = new(Color.FromRgb(174, 234, 0));
    private static readonly SolidColorBrush LessonBrush = new(Color.FromRgb(255, 235, 59));
    private static readonly SolidColorBrush PracticeBrush = new(Color.FromRgb(255, 255, 114));

    private object _sectionsLock;
    private object _innerSectionsItemsLock;

    private ObservableCollection<ExpanderLessonItemViewModel> _sections;

    public ObservableCollection<ExpanderLessonItemViewModel> Sections
    {
        get => _sections;
        set => Set(ref _sections, value);
    }

    private ObservableCollection<LessonItemViewModel> _innerSectionsItems;

    public ObservableCollection<LessonItemViewModel> InnerSectionsItems
    {
        get => _innerSectionsItems;
        set => Set(ref _innerSectionsItems, value);
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

        Sections = new ObservableCollection<ExpanderLessonItemViewModel>();
        InnerSectionsItems = new ObservableCollection<LessonItemViewModel>();

        _sectionsLock = new object();
        _innerSectionsItemsLock = new object();

        BindingOperations.EnableCollectionSynchronization(Sections, _sectionsLock);
        BindingOperations.EnableCollectionSynchronization(InnerSectionsItems, _innerSectionsItemsLock);
    }

    public async Task Build()
    {
        if (Sections.Any()) Sections.Clear();

        Label label = new();

        var dbSections = await _appDbContext.Sections.ToListAsync();
        var dbLessons = await _appDbContext.Lessons.ToListAsync();

        foreach (var section in dbSections)
        {
            InnerSectionsItems = new ObservableCollection<LessonItemViewModel>();

            label.FirstNumber += 1;
            label.SecondNumber = 0;

            foreach (var lesson in dbLessons.Where(x => x.SectionId == section.SectionId))
            {
                var newLabel = $"{label.FirstNumber}.{label.SecondNumber += 1} {GetLabelName(lesson)}";
                var item1 = new LessonItemViewModel(_host);
                await Task.Run(async () => item1.Build(lesson.LessonId, newLabel, lesson.LessonDescription, new OpenLessonDocument(_host, lesson.LessonDocumentName, lesson.IsPractice), await GetItemColor(lesson)));
                
                lock (_innerSectionsItemsLock)
                {
                    InnerSectionsItems.Add(item1);
                }
            }
            var item = new ExpanderLessonItemViewModel(_host);
            await Task.Run(() => item.Build(section.SectionHeader, InnerSectionsItems));

            lock (_sectionsLock)
            {
                Sections.Add(item);
            }
        }
    }

    private struct Label
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