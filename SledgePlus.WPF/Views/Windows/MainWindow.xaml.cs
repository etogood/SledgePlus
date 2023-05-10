using System.ComponentModel;

namespace SledgePlus.WPF.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}