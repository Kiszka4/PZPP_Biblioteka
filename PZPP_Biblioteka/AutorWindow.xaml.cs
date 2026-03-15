using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PZPP_Biblioteka
{
    /// <summary>
    /// Logika interakcji dla klasy AutorWindow.xaml
    /// </summary>
    public partial class AutorWindow : Window
    {
        public AutorWindow(AutorViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
