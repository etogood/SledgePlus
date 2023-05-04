using Microsoft.Extensions.DependencyInjection;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.ViewModels.UserControls;
using System.Diagnostics;
using System.IO;
using System.Text;

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
        //compiling.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //compiling.StartInfo.CreateNoWindow = true;
        compiling.Start();
    }
}