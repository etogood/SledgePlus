using System.Diagnostics;
using System.IO;

namespace SledgePlus.WPF.Commands.InnerActions;

public class RunCodeCommand : Command
{
    private const string _programPath = @".\MinGW\bin\__temp_program.o";

    private readonly IHost _host;

    public RunCodeCommand(IHost host)
    {
        _host = host;
    }

    public override bool CanExecute(object? parameter) => File.Exists(_programPath);

    public override void Execute(object? parameter)
    {
        var process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/K" + _programPath;
        process.Start();
    }
}