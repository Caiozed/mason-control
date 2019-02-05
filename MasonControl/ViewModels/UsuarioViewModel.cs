using LiteDB;
using MasonControl.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasonControl.ViewModels
{
    public class UsuarioViewModel : BindableBase
    {
        private LiteCollection<Usuario> _usuarioDb;
        public LiteCollection<Usuario> UsuarioDb
        {
            get { return _usuarioDb; }
            set { SetProperty(ref _usuarioDb, value); }
        }

        private ObservableCollection<Usuario> _usuarios;
        public ObservableCollection<Usuario> Usuarios
        {
            get { return _usuarios; }
            set { SetProperty(ref _usuarios, value); }
        }

        private Usuario _usuarioSelecionado;
        public Usuario UsuarioSelecionado
        {
            get { return _usuarioSelecionado; }
            set { SetProperty(ref _usuarioSelecionado, value); }
        }

        private string _search;
        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }

        public DelegateCommand ProcuraCommand { get; set; }


        public UsuarioViewModel()
        {
            ProcuraCommand = new DelegateCommand(Procura);
            UsuarioDb = MainWindowViewModel.Instance.db.GetCollection<Usuario>("usuarios");

            Usuarios = new ObservableCollection<Usuario>(UsuarioDb.FindAll().ToList());
            UsuarioSelecionado = new Models.Usuario();
        }

        public void Procura()
        {
            if (!string.IsNullOrEmpty(Search))
                Usuarios = new ObservableCollection<Usuario>(UsuarioDb.Find(x => x.Login.ToLower().StartsWith(Search.ToLower())).ToList());
            else
                Usuarios = new ObservableCollection<Usuario>(UsuarioDb.FindAll().ToList());
        }
    }
}
