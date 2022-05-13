using System.Windows;
using Tamaricky.MVVM.ViewModel;

namespace Tamaricky
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CloseButton.Click += (s, e) => { Close(); };
            this.DataContext = new MainViewModel();
        }
    }
}
