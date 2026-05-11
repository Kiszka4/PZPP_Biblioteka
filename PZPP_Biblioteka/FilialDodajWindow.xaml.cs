using System.Windows;

namespace PZPP_Biblioteka
{
    public partial class FilialDodajWindow : Window
    {
        public FilialDodajWindow(Biblioteka context)
        {
            InitializeComponent();
            var vm = new FiliaZapiszViewModel(context);
            vm.ZamknijOkno += () => Close();
            DataContext = vm;
        }
    }
}