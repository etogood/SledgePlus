using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.InnerActions;

public class CompileCodeCommand : Command
{
    private readonly IFactory<ViewModel> _viewModelFactory;
    private readonly IHost _host;

    private readonly Process _compiling;
    private readonly Process _executable;

    public CompileCodeCommand(IHost host)
    {
        _host = host;
        _viewModelFactory = host.Services.GetRequiredService<IFactory<ViewModel>>();
        _compiling = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/K" + @"MinGW\bin\compile.bat",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        _executable = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = @"MinGW\bin\__temp_program.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
    }


    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        var vm = ((IDEViewModel)_viewModelFactory.Get(typeof(IDEViewModel)));

        try
        {
            File.Delete(Directory.GetCurrentDirectory() + @"\MinGW\bin\__temp_program.exe");
            
            using (var fs = File.Create(Directory.GetCurrentDirectory() + @"\__temp_code.cpp"))
            {
                var info = new UTF8Encoding(true).GetBytes(vm.CodeDocument.Text);
                fs.Write(info, 0, info.Length);
            }

            _compiling.Start();
            AddEntries();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private async void AddEntries()
    {
        try
        {
            _executable.Start();

            while (!_compiling.StandardOutput.EndOfStream)
            {
                var line = await _compiling.StandardOutput.ReadLineAsync();
                ((IDEViewModel)_viewModelFactory.Get(typeof(IDEViewModel))).Entries.Add(line);
            }

            while (!_executable.StandardOutput.EndOfStream)
            {
                var line = await _executable.StandardOutput.ReadLineAsync();
                ((IDEViewModel)_viewModelFactory.Get(typeof(IDEViewModel))).Entries.Add(line);
            }
        }
        catch (Exception)
        {

        }
    }
}