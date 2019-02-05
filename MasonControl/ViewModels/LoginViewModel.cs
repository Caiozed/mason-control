using LiteDB;
using MasonControl.Models;
using MasonControl.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MasonControl.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set { SetProperty(ref _usuario, value); }
        }

        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set { SetProperty(ref _senha, value); }
        }

        private LiteCollection<Usuario> _usuarioDb;
        public LiteCollection<Usuario> UsuarioDb
        {
            get { return _usuarioDb; }
            set { SetProperty(ref _usuarioDb, value); }
        }

        public DelegateCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand(Login);
            UsuarioDb = MainWindowViewModel.Instance.db.GetCollection<Usuario>("usuarios");

            if (UsuarioDb.FindAll().Count() == 0)
            {
                UsuarioDb.Insert(new Usuario {Senha = "admin", Login = "admin" });
            }
        }

        private void Login()
        {
            var count = 0;
            if (!string.IsNullOrEmpty(Usuario) && !string.IsNullOrEmpty(Senha))
            {
                count = UsuarioDb.Find(s => s.Senha == Senha && s.Login== Usuario).Count();
            }

            if (count == 0)
            {
                MessageBox.Show("Usuario ou senha incorreto!");
            }
            else
            {
                MainWindowViewModel.Instance.IsloggedIn = true;
                MainWindowViewModel.Instance.frame.Navigate(new MembrosView());
            }
        }
    }
}
