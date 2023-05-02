using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;
using SledgePlus.WPF.ViewModels.UserControls.Custom;

namespace SledgePlus.WPF.Commands.InnerActions;

public class OpenLessonDocument : Command
{
    private readonly IHost _host;
    private readonly string _document;
    private readonly bool _isPractice;

    public OpenLessonDocument(IHost host, string document, bool isPractice)
    {
        _host = host;
        _document = document;
        _isPractice = isPractice;
    }

    public override bool CanExecute(object? parameter) => true;

    public override async void Execute(object? parameter)
    {
        var vm = _host.Services.GetRequiredService<LearningMenuViewModel>();
        vm.ErrorMessage = string.Empty;
        try
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo(_document)
                {
                    UseShellExecute = true
                }
            };
            p.Start();

            if (_isPractice)
                _host.Services.GetRequiredService<INavigationStore>().CurrentViewModel =
                    _host.Services.GetRequiredService<IDEViewModel>();


            var item = parameter as LessonItemViewModel;
            var currentUser = _host.Services.GetRequiredService<ILoginStore>().CurrentUser;

            if (currentUser == null) return;
            if (item == null) return;

            var lessonUser = new LessonUser
            {
                LessonId = item.Id,
                UserId = currentUser.UserId
            };

            var appDbContext = _host.Services.GetRequiredService<AppDbContext>();

            if (appDbContext.LessonUsers.FirstOrDefault(x => x == lessonUser) != null) return;

            appDbContext.LessonUsers.Add(lessonUser);
            await appDbContext.SaveChangesAsync();
        }
        catch (Win32Exception)
        {
            vm.ErrorMessage = "Файл не обнаружен, обратитесь к администратору";
        }
        catch (Exception)
        {
            vm.ErrorMessage = "Неизвестная ошибка";
        }
    }
}