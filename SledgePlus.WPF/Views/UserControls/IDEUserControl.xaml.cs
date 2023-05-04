using System.IO;
using System.Windows.Controls;

namespace SledgePlus.WPF.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для IDEUserControl.xaml
    /// </summary>
    public partial class IDEUserControl : UserControl
    {
        public IDEUserControl()
        {
            InitializeComponent();
        }

        private void TextEditor_OnTextChanged(object? sender, EventArgs e)
        {
            TextEditor.Save(Directory.GetCurrentDirectory() + @"\__temp_code.cpp");
        }
    }
}