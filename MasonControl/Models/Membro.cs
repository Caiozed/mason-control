using LiteDB;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasonControl.Models
{
    public class Membro : BindableBase
    {
        private ObjectId _id;
        public ObjectId Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); }
        }

        private long _rg;
        public long Rg
        {
            get { return _rg; }
            set { SetProperty(ref _rg, value); }
        }

        private long _cpf;
        public long Cpf
        {
            get { return _cpf; }
            set { SetProperty(ref _cpf, value); }
        }

        private long _telefone;
        public long Telefone
        {
            get { return _telefone; }
            set { SetProperty(ref _telefone, value); }
        }

        private long _celular;
        public long Celular
        {
            get { return _celular; }
            set { SetProperty(ref _celular, value); }
        }

        private long _telefoneAlt;
        public long TelefoneAlt
        {
            get { return _telefoneAlt; }
            set { SetProperty(ref _telefoneAlt, value); }
        }

        private List<ObjectId> _lojas;
        public List<ObjectId> Lojas
        {
            get { return _lojas; }
            set { SetProperty(ref _lojas, value); }
        }

        private Endereco _endereco;
        public Endereco Endereco
        {
            get { return _endereco; }
            set { SetProperty(ref _endereco, value); }
        }

        private string _imagem;
        public string Imagem
        {
            get { return _imagem; }
            set { SetProperty(ref _imagem, value); }
        }

        public Membro()
        {
            Endereco = new Endereco();
            Lojas = new List<ObjectId>();
        }
    }
}
