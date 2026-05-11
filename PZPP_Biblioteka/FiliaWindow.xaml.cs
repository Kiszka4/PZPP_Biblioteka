using System.Windows;

namespace PZPP_Biblioteka
{
    public partial class FiliaWindow : Window
    {
        public FiliaWindow(FiliaViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}