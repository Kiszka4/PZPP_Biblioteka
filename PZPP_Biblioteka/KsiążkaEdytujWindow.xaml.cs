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
    /// Logika interakcji dla klasy KsiążkaEdytujWindow.xaml
    /// </summary>
    public partial class KsiążkaEdytujWindow : Window
    {
        private readonly Biblioteka _context;

        public KsiążkaEdytujWindow(Biblioteka context, Książka książka)
        {
            InitializeComponent();
            _context = context;
            var viewModel = new KsiążkaViewModel(_context)
            {
                SelectedKsiążka = książka
            };
            DataContext = viewModel;
            viewModel.ZamknijOkno += () => this.Close();

        }

    }
}
