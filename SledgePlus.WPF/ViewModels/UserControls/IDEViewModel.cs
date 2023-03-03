using System.Windows.Input;

using ICSharpCode.AvalonEdit.Document;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.Commands.InnerActions;
using SledgePlus.WPF.Models.Text;

namespace SledgePlus.WPF.ViewModels.UserControls;

public class IDEViewModel : ViewModel
{
    public ICommand CompileCodeCommand { get; }

    private TextDocument _codeDocument;
    public TextDocument CodeDocument
    {
        get => _codeDocument;
        set => Set(ref _codeDocument, value);
    }

    public IDEViewModel(IHost host)
    {
        CompileCodeCommand = host.Services.GetRequiredService<CompileCodeCommand>();

        CodeDocument = new TextDocument
        {
            FileName = "temp_code",
            Text = Text.ReadText()
        };
    }
}