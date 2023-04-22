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

    private TextDocument _codeDocument;

    public TextDocument CodeDocument
    {
        get => _codeDocument;
        set => Set(ref _codeDocument, value);
    }

    private ObservableCollection<string?> _entries;

    public ObservableCollection<string?> Entries
    {
        get => _entries;
        set => Set(ref _entries, value);
    }

    public IDEViewModel(IHost host)
    {
        CompileCodeCommand = host.Services.GetRequiredService<CompileCodeCommand>();
        Entries = new ObservableCollection<string?>();
        CodeDocument = new TextDocument
        {
            FileName = "temp_code",
            Text = Text.ReadText()
        };
    }
}