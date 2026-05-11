using System.Windows;

namespace PZPP_Biblioteka
{
    public partial class FiliaEdytujWindow : Window
    {
        public FiliaEdytujWindow(Biblioteka context, Filia filia)
        {
            InitializeComponent();
            var vm = new FiliaZapiszViewModel(context, filia);
            vm.ZamknijOkno += () => Close();
            DataContext = vm;
        }
    }
}