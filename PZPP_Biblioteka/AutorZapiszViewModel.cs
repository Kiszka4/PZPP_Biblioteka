using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PZPP_Biblioteka
{
    public class AutorZapiszViewModel : INotifyPropertyChanged
    {
        private readonly Biblioteka _context;
        private readonly Autor _autor;

        private string _imię;
        public string Imię
        {
            get => _imię;
            set { _imię = value; OnPropertyChanged(); }
        }

        private string _nazwisko;
        public string Nazwisko
        {
            get => _nazwisko;
            set { _nazwisko = value; OnPropertyChanged(); }
        }

        public ICommand ZapiszCommand { get; }
        public event Action ZamknijOkno;

        public AutorZapiszViewModel(Biblioteka context, Autor autor = null)
        {
            _context = context;
            _autor = autor ?? new Autor();
            Imię = _autor.Imię;
            Nazwisko = _autor.Nazwisko;
            ZapiszCommand = new RelayCommand(Zapisz);
        }

        private void Zapisz(object obj)
        {
            // WALIDACJA

            if (string.IsNullOrWhiteSpace(Imię))
            {
                MessageBox.Show(
                    "Imię autora nie może być puste.",
                    "Błąd",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(Nazwisko))
            {
                MessageBox.Show(
                    "Nazwisko autora nie może być puste.",
                    "Błąd",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            // PRZYPISANIE DANYCH

            _autor.Imię = Imię;
            _autor.Nazwisko = Nazwisko;

            try
            {
                if (_autor.ID == 0)
                    _context.Autorzy.Add(_autor);
                else
                    _context.Autorzy.Update(_autor);

                _context.SaveChanges();

                MessageBox.Show(
                    "Autor został zapisany.",
                    "Sukces",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                ZamknijOkno?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Wystąpił błąd podczas zapisu:\n{ex.Message}",
                    "Błąd",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
