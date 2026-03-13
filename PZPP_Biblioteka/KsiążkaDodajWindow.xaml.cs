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

namespace PZPP_Biblioteka
{
    /// <summary>
    /// Logika interakcji dla klasy ProduktyDodajWindow.xaml
    /// </summary>
    public partial class KsiążkaDodajWindow : Window
    {
        private readonly Biblioteka _context;

        public KsiążkaDodajWindow(Biblioteka context)
        {
            InitializeComponent();
            _context = context;
            var viewModel = new KsiążkaZapiszViewModel(_context);
            DataContext = viewModel;
            viewModel.ZamknijOkno += () => this.Close();

        }

    }
}
