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
    /// Logika interakcji dla klasy AutorEdytujWindow.xaml
    /// </summary>
    public partial class AutorEdytujWindow : Window
    {
        public AutorEdytujWindow(Biblioteka context, Autor autor)
        {
            InitializeComponent();
            var viewModel = new AutorZapiszViewModel(context, autor);
            DataContext = viewModel;
            viewModel.ZamknijOkno += () => this.Close();
        }
    }
}
