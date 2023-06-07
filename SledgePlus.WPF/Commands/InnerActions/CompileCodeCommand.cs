using Microsoft.Extensions.DependencyInjection;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.ViewModels.UserControls;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using SledgePlus.WPF.Models.Processes;

namespace SledgePlus.WPF.Commands.InnerActions;

public class CompileCodeCommand : Command
{
    private readonly IHost _host;

    private const string _programPath = @".\MinGW\bin\__temp_program.o";

    public CompileCodeCommand(IHost host)
    {
        _host = host;
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {

        var compiling = new Process();
        compiling.StartInfo.FileName = "cmd.exe";
        compiling.StartInfo.Arguments = "/K" + @"MinGW\bin\compile.bat";
        compiling.StartInfo.RedirectStandardError = true;
        compiling.StartInfo.CreateNoWindow = true;
        compiling.Start();

        Thread.Sleep(3000);

        ProcessesManagement.KillProcessAndChildren(compiling.Id);

        var error = compiling.StandardError.ReadToEnd();

        MessageBox.Show(string.IsNullOrEmpty(error) ? "Компиляция завершена без ошибок!" : $"Компиляция завершена с ошибкой:\n\n{error}");


    }
}