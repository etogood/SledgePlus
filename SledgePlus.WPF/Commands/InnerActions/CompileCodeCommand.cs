using Microsoft.Extensions.DependencyInjection;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.ViewModels.UserControls;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SledgePlus.WPF.Commands.InnerActions;

public class CompileCodeCommand : Command
{
    private readonly IFactory<ViewModel> _viewModelFactory;

    private const string _programPath = @".\MinGW\bin\__temp_program.o";

    public CompileCodeCommand(IHost host)
    {
        _viewModelFactory = host.Services.GetRequiredService<IFactory<ViewModel>>();
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        var vm = (IDEViewModel)_viewModelFactory.Get(typeof(IDEViewModel));

        try
        {
            File.Delete(Directory.GetCurrentDirectory() + _programPath);
        }
        catch (Exception)
        {
            // ignored
        }

        using (var fs = File.Create(Directory.GetCurrentDirectory() + @"\__temp_code.cpp"))
        {
            var info = new UTF8Encoding(true).GetBytes(vm.CodeDocument.Text);
            fs.Write(info, 0, info.Length);
        }

        var compiling = new Process();
        compiling.StartInfo.FileName = "cmd.exe";
        compiling.StartInfo.Arguments = "/K" + @"MinGW\bin\compile.bat";
        compiling.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        compiling.StartInfo.CreateNoWindow = true;
        compiling.Start();
    }
}