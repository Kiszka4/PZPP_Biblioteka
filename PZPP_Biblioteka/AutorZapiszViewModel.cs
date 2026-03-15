using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
            _autor.Imię = Imię;
            _autor.Nazwisko = Nazwisko;
            if (_autor.ID == 0)
                _context.Autorzy.Add(_autor);
            else
                _context.Autorzy.Update(_autor);
            _context.SaveChanges();
            ZamknijOkno?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
