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
    /// Logika interakcji dla klasy KategorieDodajWindow.xaml
    /// </summary>
    public partial class GatunekDodajWindow : Window
    {
        private readonly Biblioteka _context;

        public GatunekDodajWindow(Biblioteka context)
        {
            InitializeComponent();
            _context = context;
            var viewModel = new GatunekZapiszViewModel(_context);
            DataContext = viewModel;
            viewModel.ZamknijOkno += () => this.Close();
        }
    }
}
