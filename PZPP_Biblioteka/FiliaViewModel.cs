using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PZPP_Biblioteka
{
    public class FiliaViewModel : INotifyPropertyChanged
    {
        private readonly Biblioteka _context;
        public ObservableCollection<Filia> Filie { get; set; }

        private Filia? _selectedFilia;
        public Filia? SelectedFilia
        {
            get => _selectedFilia;
            set { _selectedFilia = value; OnPropertyChanged(); }
        }

        public ICommand PokazDodajFiliaCommand { get; }
        public ICommand EdytujFiliaCommand { get; }
        public ICommand UsunFiliaCommand { get; }

        public FiliaViewModel(Biblioteka context)
        {
            _context = context;
            Filie = new ObservableCollection<Filia>(_context.Filie.ToList());
            PokazDodajFiliaCommand = new RelayCommand(PokazDodajFilia);
            EdytujFiliaCommand = new RelayCommand(EdytujFilia, _ => SelectedFilia != null);
            UsunFiliaCommand = new RelayCommand(UsunFilia, _ => SelectedFilia != null);
        }

        private void PokazDodajFilia(object obj)
        {
            var okno = new FilialDodajWindow(_context);
            okno.ShowDialog();
            OdswiezFilie();
        }

        private void EdytujFilia(object obj)
        {
            if (SelectedFilia == null) return;
            var okno = new FiliaEdytujWindow(_context, SelectedFilia);
            okno.ShowDialog();
            OdswiezFilie();
        }

        private void UsunFilia(object obj)
        {
            if (SelectedFilia == null) return;
            var wynik = MessageBox.Show(
                $"Czy na pewno chcesz usunąć filię:\n{SelectedFilia.Nazwa}?",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (wynik == MessageBoxResult.Yes)
            {
                _context.Filie.Remove(SelectedFilia);
                _context.SaveChanges();
                OdswiezFilie();
            }
        }

        public void OdswiezFilie()
        {
            Filie.Clear();
            foreach (var filia in _context.Filie.ToList())
                Filie.Add(filia);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}