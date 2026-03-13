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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PZPP_Biblioteka
{
    /// <summary>
    /// Logika interakcji dla klasy KategorieWindow.xaml
    /// </summary>
    public partial class GatunekWindow : Window
    {
        private GatunekViewModel _viewModel;


        public GatunekWindow(GatunekViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }


    }
}
