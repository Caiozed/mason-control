using LiteDB;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasonControl.Models
{
    public class Usuario : BindableBase
    {
        private ObjectId _id;
        public ObjectId Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set { SetProperty(ref _senha, value); }
        }       
    }
}
