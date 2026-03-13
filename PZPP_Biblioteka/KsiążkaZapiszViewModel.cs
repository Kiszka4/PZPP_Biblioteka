using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PZPP_Biblioteka;

namespace PZPP_Biblioteka
{
    public class KsiążkaZapiszViewModel : INotifyPropertyChanged
    {
        private readonly Biblioteka _context;
        private Książka _książka;

        private string _tytuł;
        public string Tytuł
        {
            get => _tytuł;
            set { _tytuł = value; OnPropertyChanged(); }
        }

        private int _iloscNaStanie;
        public int IloscNaStanie
        {
            get => _iloscNaStanie;
            set { _iloscNaStanie = value; OnPropertyChanged(); }
        }

        

        public ObservableCollection<GatunekKsiążki> Gatunki { get; }
        public GatunekKsiążki WybranyGatunek { get; set; }

        public ObservableCollection<Autor> Autorzy { get; set; }
        public Autor WybranyAutor { get; set; }

        public ICommand ZapiszCommand { get; }
        public event Action ZamknijOkno;

        public KsiążkaZapiszViewModel(Biblioteka context)
        {
            _context = context;
            Gatunki = new ObservableCollection<GatunekKsiążki>(_context.GatunkiKsiążek);
            Autorzy = new ObservableCollection<Autor>(_context.Autorzy);
            ZapiszCommand = new RelayCommand(Zapisz);
            _książka = new Książka();
        }

        private void Zapisz(object obj)
        {

            _książka.Tytuł = Tytuł;
            _książka.IloscNaStanie = IloscNaStanie;
            _książka.GatunekKsiążki = WybranyGatunek;
            _książka.Autor = WybranyAutor;

            if (_książka.ISBN == 0)
                _context.Książki.Add(_książka);
            else
                _context.Książki.Update(_książka);

            _context.SaveChanges();
            ZamknijOkno?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    }
}
