using System.Diagnostics;
using System.Linq;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.ViewModels.UserControls;
using SledgePlus.WPF.ViewModels.UserControls.Custom;

namespace SledgePlus.WPF.Commands.InnerActions;

public class OpenLessonDocument : Command
{
    private readonly IHost _host;
    private readonly string _document;

    public OpenLessonDocument(IHost host, string document)
    {
        _host = host;
        _document = document;
    }

    public override bool CanExecute(object? parameter) => true;

    public override async void Execute(object? parameter)
    {
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
        catch (Exception)
        {
            //TODO: Add to the error vm
        }
    }
}