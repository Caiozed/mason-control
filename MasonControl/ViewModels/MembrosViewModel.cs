using LiteDB;
using MasonControl.Models;
using MasonControl.ViewModels;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MasonControl.ViewModels
{
    public class MembrosViewModel : BindableBase
    {
        private ObservableCollection<Loja> _lojasSelecionadas;
        public ObservableCollection<Loja> LojasSelecionadas
        {
            get { return _lojasSelecionadas; }
            set { SetProperty(ref _lojasSelecionadas, value); }
        }

        private LiteCollection<Loja> _lojasDb;
        public LiteCollection<Loja> LojasDb
        {
            get { return _lojasDb; }
            set { SetProperty(ref _lojasDb, value); }
        }

        private LiteCollection<StatusMembro> _statusDb;
        public LiteCollection<StatusMembro> StatusDb
        {
            get { return _statusDb; }
            set { SetProperty(ref _statusDb, value); }
        }

        private ObservableCollection<StatusMembro> _statusSelecionados;
        public ObservableCollection<StatusMembro> StatusSelecionados
        {
            get { return _statusSelecionados; }
            set { SetProperty(ref _statusSelecionados, value); }
        }

        private List<Loja> _lojas;
        public List<Loja> Lojas
        {
            get { return _lojas; }
            set { SetProperty(ref _lojas, value); }
        }

        private LiteCollection<Membro> _membrosDb;
        public LiteCollection<Membro> MembrosDb
        {
            get { return _membrosDb; }
            set { SetProperty(ref _membrosDb, value); }
        }

        private List<Membro> _membros;
        public List<Membro> Membros
        {
            get { return _membros; }
            set { SetProperty(ref _membros, value); }
        }

        private Membro _membroSelecionado;
        public Membro MembroSelecionado
        {
            get { return _membroSelecionado; }
            set { SetProperty(ref _membroSelecionado, value); }
        }

        private string _search;
        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }

        private string _imagemSelecionada;
        public string ImagemSelecionada
        {
            get { return _imagemSelecionada; }
            set { SetProperty(ref _imagemSelecionada, value); }
        }

        public DelegateCommand ProcuraCommand { get; set; }
        public DelegateCommand SelecionaImagemCommand { get; set; }

        public MembrosViewModel()
        {
            ProcuraCommand = new DelegateCommand(Procura);
            SelecionaImagemCommand = new DelegateCommand(SelecionaImagem);
            MembroSelecionado = new Membro();
            LojasSelecionadas = new ObservableCollection<Loja>();
            MembrosDb = MainWindowViewModel.Instance.db.GetCollection<Membro>("membros");
            LojasDb = MainWindowViewModel.Instance.db.GetCollection<Loja>("lojas");
            StatusDb = MainWindowViewModel.Instance.db.GetCollection<StatusMembro>("statusMembros");

            Lojas = LojasDb.FindAll().ToList();
            Membros = MembrosDb.FindAll().ToList();
        }

        private void SelecionaImagem()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png";
                dialog.ShowDialog();
                if (!string.IsNullOrEmpty(dialog.FileName))
                {
                    //ImagemSelecionada = dialog.FileName;
                    MembroSelecionado.Imagem = Convert.ToBase64String(File.ReadAllBytes(dialog.FileName));
                    //var img = ImagemSelecionada.FromStream(new MemoryStream(Convert.FromBase64String(base64Image)));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void Procura()
        {
            if (!string.IsNullOrEmpty(Search))
                Membros = MembrosDb.Find(x => x.Nome.ToLower().StartsWith(Search.ToLower())).ToList();
            else
                Membros = MembrosDb.FindAll().ToList();
        }
    }

    public class Base64ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = value as string;

            if (string.IsNullOrEmpty(s))
                return null;

            BitmapImage bi = new BitmapImage();

            bi.BeginInit();
            bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(s));
            bi.EndInit();

            return bi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
