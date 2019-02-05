using LiteDB;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasonControl.Models
{
    public class Loja : BindableBase
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); }
        }

        private ObjectId _id;
        public ObjectId Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private Endereco _endereco;
        public Endereco Endereco
        {
            get { return _endereco; }
            set { SetProperty(ref _endereco, value); }
        }

        public Loja()
        {
            Endereco = new Endereco();
        }
    }
}
