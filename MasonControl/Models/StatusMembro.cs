using LiteDB;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasonControl.Models
{
    public class StatusMembro : BindableBase
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

        [BsonIgnore]
        private string _nomeMembro;
        public string NomeMembro
        {
            get { return _nomeMembro; }
            set { SetProperty(ref _nomeMembro, value); }
        }

        private ObjectId _lojaId;
        public ObjectId LojaId
        {
            get { return _lojaId; }
            set { SetProperty(ref _lojaId, value); }
        }

        private DateTime _dataIniciacao;
        public DateTime DataIniciacao
        {
            get { return _dataIniciacao; }
            set { SetProperty(ref _dataIniciacao, value); }
        }

        private DateTime _dataElevacao;
        public DateTime DataElevacao
        {
            get { return _dataElevacao; }
            set { SetProperty(ref _dataElevacao, value); }
        }

        private DateTime _dataExaltacao;
        public DateTime DataExaltacao
        {
            get { return _dataExaltacao; }
            set { SetProperty(ref _dataExaltacao, value); }
        }

        private string _grauAtual;
        public string GrauAtual
        {
            get { return _grauAtual; }
            set { SetProperty(ref _grauAtual, value); }
        }
    }
}
