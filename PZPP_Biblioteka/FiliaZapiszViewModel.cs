using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PZPP_Biblioteka
{
    public class FiliaZapiszViewModel : INotifyPropertyChanged
    {
        private readonly Biblioteka _context;
        private readonly Filia _filia;

        private string? _nazwa;
        public string? Nazwa
        {
            get => _nazwa;
            set { _nazwa = value; OnPropertyChanged(); }
        }

        private string? _lokalizacja;
        public string? Lokalizacja
        {
            get => _lokalizacja;
            set { _lokalizacja = value; OnPropertyChanged(); }
        }

        public ICommand ZapiszCommand { get; }
        public event Action? ZamknijOkno;

        public FiliaZapiszViewModel(Biblioteka context, Filia? filia = null)
        {
            _context = context;
            _filia = filia ?? new Filia();
            Nazwa = _filia.Nazwa;
            Lokalizacja = _filia.Lokalizacja;
            ZapiszCommand = new RelayCommand(Zapisz);
        }

        private void Zapisz(object obj)
        {
            if (string.IsNullOrWhiteSpace(Nazwa))
            {
                MessageBox.Show("Nazwa filii nie może być pusta.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Lokalizacja))
            {
                MessageBox.Show("Lokalizacja filii nie może być pusta.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _filia.Nazwa = Nazwa;
            _filia.Lokalizacja = Lokalizacja;

            try
            {
                if (_filia.ID == 0)
                    _context.Filie.Add(_filia);
                else
                    _context.Filie.Update(_filia);

                _context.SaveChanges();

                MessageBox.Show("Filia została zapisana.", "Sukces",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                ZamknijOkno?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas zapisu:\n{ex.Message}", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}