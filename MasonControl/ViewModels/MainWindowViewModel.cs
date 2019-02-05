using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Controls;
using LiteDB;
using MahApps.Metro.Controls;
using MasonControl.Models;
using MasonControl.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Unity;

namespace MasonControl.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public Frame frame;
        public Flyout menu;
        public LiteDatabase db;
        public static MainWindowViewModel Instance;
        public DelegateCommand<string> NavigateCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }

        public bool _isloggedIn;
        public bool IsloggedIn { get { return _isloggedIn; } set { SetProperty(ref _isloggedIn, value); } }

        public MainWindowViewModel()
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
            LogoutCommand = new DelegateCommand(LogOut);

            Instance = this;

            using (var db = new LiteDatabase(@"data.db"))
            {
                this.db = db;
            }
        }

        private void LogOut()
        {
            IsloggedIn = false;
            menu.IsOpen = false;
            frame.Navigate(new LoginView());
        }

        public void Navigate(string path)
        {
            menu.IsOpen = false;
            switch (path)
            {
                case "MembrosView":
                    frame.Navigate(new MembrosView());
                    break;
                case "PagamentosView":
                    frame.Navigate(new PagamentosView());
                    break;
                case "LojasView":
                    frame.Navigate(new LojasView());
                    break;
                case "UsuariosView":
                    frame.Navigate(new UsuariosView());
                    break;
                case "StatusView":
                    frame.Navigate(new StatusView());
                    break;
            }
        }

        public async Task<Endereco> GetEnderecoAsync(string cep)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.postmon.com.br/v1/cep/" + cep);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentType = "application/json; charset=utf-8";

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<dynamic>(json.ToString());
                return new Endereco() { Bairro = data.bairro, Cep = data.cep, Cidade = data.cidade, Logradouro = data.logradouro, Uf = data.estado };
            }
        }
    }
}
