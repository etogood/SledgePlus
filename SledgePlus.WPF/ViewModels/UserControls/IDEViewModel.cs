using System.Collections.ObjectModel;
using System.Windows.Input;

using ICSharpCode.AvalonEdit.Document;

using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.Commands.InnerActions;
using SledgePlus.WPF.Models.Text;

namespace SledgePlus.WPF.ViewModels.UserControls;

public class IDEViewModel : ViewModel
{
    public ICommand CompileCodeCommand { get; }
    public ICommand RunCodeCommand { get; }

    private TextDocument _codeDocument;

    public TextDocument CodeDocument
    {
        get => _codeDocument;
        set => Set(ref _codeDocument, value);
    }

    private string _code;

    public string Code
    {
        get => _code;
        set => Set(ref _code, value);
    }

    public IDEViewModel(IHost host)
    {
        CompileCodeCommand = host.Services.GetRequiredService<CompileCodeCommand>();
        RunCodeCommand = host.Services.GetRequiredService<RunCodeCommand>();
        CodeDocument = new TextDocument
        {
            FileName = "temp_code",
            Text = Text.ReadText()
        };
    }
}