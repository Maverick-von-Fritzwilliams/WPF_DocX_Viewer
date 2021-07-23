using System.Windows;

namespace WPF_DOCX_Viewer   
{
    public partial class MainWindow : Window
    {
        ApplicationViewModel viewModel = new ApplicationViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
