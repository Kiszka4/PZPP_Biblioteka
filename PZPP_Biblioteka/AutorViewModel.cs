using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PZPP_Biblioteka
{
    public class AutorViewModel : INotifyPropertyChanged
    {
        private readonly Biblioteka _context;
        public ObservableCollection<Autor> Autorzy { get; set; }

        private Autor _selectedAutor;
        public Autor SelectedAutor
        {
            get => _selectedAutor;
            set { _selectedAutor = value; OnPropertyChanged(); }
        }

        public ICommand PokazDodajAutorCommand { get; }
        public ICommand EdytujAutorCommand { get; }
        public ICommand UsunAutorCommand { get; }
        public event Action ZamknijOkno;

        public AutorViewModel(Biblioteka context)
        {
            _context = context;
            Autorzy = new ObservableCollection<Autor>(_context.Autorzy.ToList());
            PokazDodajAutorCommand = new RelayCommand(PokazDodajAutora);
            EdytujAutorCommand = new RelayCommand(EdytujAutora, _ => SelectedAutor != null);
            UsunAutorCommand = new RelayCommand(UsunAutora, _ => SelectedAutor != null);
        }

        private void PokazDodajAutora(object obj)
        {
            var okno = new AutorDodajWindow(_context);
            okno.ShowDialog();
            OdswiezAutorzy();
        }

        private void EdytujAutora(object obj)
        {
            if (SelectedAutor == null) return;
            var okno = new AutorEdytujWindow(_context, SelectedAutor);
            okno.ShowDialog();
            OdswiezAutorzy();
        }

        private void UsunAutora(object obj)
        {
            if (SelectedAutor == null) return;
            _context.Autorzy.Remove(SelectedAutor);
            _context.SaveChanges();
            OdswiezAutorzy();
        }

        public void OdswiezAutorzy()
        {
            Autorzy.Clear();
            foreach (var autor in _context.Autorzy.ToList())
                Autorzy.Add(autor);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
