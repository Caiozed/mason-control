using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasonControl.Models
{
    public class Endereco : BindableBase
    {
        private string _bairro;
        public string Bairro
        {
            get { return _bairro; }
            set { SetProperty(ref _bairro, value); }
        }

        private long _numero;
        public long Numero
        {
            get { return _numero; }
            set { SetProperty(ref _numero, value); }
        }

        private string _logradouro;
        public string Logradouro
        {
            get { return _logradouro; }
            set { SetProperty(ref _logradouro, value); }
        }

        private string _complemento;
        public string Complemento
        {
            get { return _complemento; }
            set { SetProperty(ref _complemento, value); }
        }

        private string _uf;
        public string Uf
        {
            get { return _uf; }
            set { SetProperty(ref _uf, value); }
        }

        private string _cidade;
        public string Cidade
        {
            get { return _cidade; }
            set { SetProperty(ref _cidade, value); }
        }

        private long _cep;
        public long Cep
        {
            get { return _cep; }
            set { SetProperty(ref _cep, value); }
        }
    }
}
