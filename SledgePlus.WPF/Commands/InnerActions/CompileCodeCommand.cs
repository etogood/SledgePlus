using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.InnerActions;

public class CompileCodeCommand : Command
{
    private readonly IFactory<ViewModel> _viewModelFactory;

    public CompileCodeCommand(IHost host)
    {
        _viewModelFactory = host.Services.GetRequiredService<IFactory<ViewModel>>();
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        var text = ((IDEViewModel)_viewModelFactory.Get(typeof(IDEViewModel))).CodeDocument.Text;

        using (var fs = File.Create(Directory.GetCurrentDirectory() + @"\__temp_code.cpp"))
        {
            var info = new UTF8Encoding(true).GetBytes(text);
            fs.Write(info, 0, info.Length);
        }

        System.Diagnostics.Process.Start("cmd.exe", "/C" + @"MinGW\bin\g++.exe __temp_code.cpp");
    }
}