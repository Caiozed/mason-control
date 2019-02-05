using LiteDB;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasonControl.Models
{
    public class Pagamento : BindableBase
    {
        private ObjectId _id;
        public ObjectId Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private ObjectId _membroId;
        public ObjectId MembroId
        {
            get { return _membroId; }
            set { SetProperty(ref _membroId, value); }
        }

        private string _membroNome;
        public string MembroNome
        {
            get { return _membroNome; }
            set { SetProperty(ref _membroNome, value); }
        }

        private ObjectId _lojaId;
        public ObjectId LojaId
        {
            get { return _lojaId; }
            set { SetProperty(ref _lojaId, value); }
        }

        private string _lojaNome;
        public string LojaNome
        {
            get { return _lojaNome; }
            set { SetProperty(ref _lojaNome, value); }
        }

        private DateTime _dataVencimento;
        public DateTime DataVencimento
        {
            get { return _dataVencimento; }
            set { SetProperty(ref _dataVencimento, value); }
        }

        private DateTime _dataPagamento;
        public DateTime DataPagamento
        {
            get { return _dataPagamento; }
            set { SetProperty(ref _dataPagamento, value); }
        }

        private StatusPagamento _statusPagamento;
        public StatusPagamento StatusPagamento
        {
            get { return _statusPagamento; }
            set { SetProperty(ref _statusPagamento, value); }
        }
    }

    public enum StatusPagamento {
        Irregular,
        Regular,
        Isento
    }
}
