using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Input;

namespace PZPP_Biblioteka
{
    /// <summary>
    /// Logika interakcji dla klasy ProduktyWindow.xaml
    /// </summary>
    public partial class KsiążkaWindow : Window
    {
        private KsiążkaViewModel _viewModel;

        public KsiążkaWindow(KsiążkaViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is KsiążkaViewModel vm &&
                vm.SelectedKsiążka != null)
            {
                var okno = new KsiążkaSzczegółyWindow(vm.SelectedKsiążka);

                okno.ShowDialog();
            }
        }

    }
}
