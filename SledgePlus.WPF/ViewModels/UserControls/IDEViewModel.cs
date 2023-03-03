using System.IO;
using System.Windows.Shapes;

using ICSharpCode.AvalonEdit.Document;

namespace SledgePlus.WPF.ViewModels.UserControls;

public class IDEViewModel : ViewModel
{

    private TextDocument _codeDocument;
    public TextDocument CodeDocument
    {
        get => _codeDocument;
        set => Set(ref _codeDocument, value);
    }

    public IDEViewModel(IHost host)
    {
        CodeDocument = new TextDocument
        {
            FileName = "temp_code",
            Text = ReadTemplate()
        };
    }

    private static string? ReadTemplate()
    {
        var sr = new StreamReader(Directory.GetCurrentDirectory() + "/Templates/cpp_template.txt");
        var text = string.Empty;
        string? line;

        do
        {
            line = sr.ReadLine();
            text += "\n" + line;
        } while (line != null);

        sr.Close();
        return text;
    }
}