using LiteDB;
using MasonControl.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasonControl.ViewModels
{
    public class PagamentoViewModel : BindableBase
    {
        private DateTime _dataSelecionada;
        public DateTime DataSelecionada
        {
            get { return _dataSelecionada; }
            set { SetProperty(ref _dataSelecionada, value); }
        }

        private LiteCollection<Loja> _lojasDb;
        public LiteCollection<Loja> LojasDb
        {
            get { return _lojasDb; }
            set { SetProperty(ref _lojasDb, value); }
        }

        private LiteCollection<Membro> _membrosDb;
        public LiteCollection<Membro> MembrosDb
        {
            get { return _membrosDb; }
            set { SetProperty(ref _membrosDb, value); }
        }

        private LiteCollection<Pagamento> _pagamentosDb;
        public LiteCollection<Pagamento> PagamentosDb
        {
            get { return _pagamentosDb; }
            set { SetProperty(ref _pagamentosDb, value); }
        }

        private List<Loja> _lojas;
        public List<Loja> Lojas
        {
            get { return _lojas; }
            set { SetProperty(ref _lojas, value); }
        }

        private Loja _lojaSelecionada;
        public Loja LojaSelecionada
        {
            get { return _lojaSelecionada; }
            set { SetProperty(ref _lojaSelecionada, value); }
        }

        private ObservableCollection<Pagamento> _pagamentos;
        public ObservableCollection<Pagamento> Pagamentos
        {
            get { return _pagamentos; }
            set { SetProperty(ref _pagamentos, value); }
        }

        private Pagamento _pagamentoSelecionado;
        public Pagamento PagamentoSelecionado
        {
            get { return _pagamentoSelecionado; }
            set { SetProperty(ref _pagamentoSelecionado, value); }
        }

        private List<Membro> _membrosSelecionados;
        public List<Membro> MembrosSelecionados
        {
            get { return _membrosSelecionados; }
            set { SetProperty(ref _membrosSelecionados, value); }
        }

        private List<StatusPagamento> _status;
        public List<StatusPagamento> Status
        {
            get { return Enum.GetValues(typeof(StatusPagamento)).Cast<StatusPagamento>().ToList(); }
            set { SetProperty(ref _status, value); }
        }

        private StatusPagamento _statusSelecionado;
        public StatusPagamento StatusSelecionado
        {
            get { return _statusSelecionado; }
            set { SetProperty(ref _statusSelecionado, value); }
        }

        public PagamentoViewModel()
        {
            DataSelecionada = DateTime.Now;
            Pagamentos = new ObservableCollection<Pagamento>();
            Lojas = new List<Loja>();
            MembrosSelecionados = new List<Membro>();

            PagamentosDb = MainWindowViewModel.Instance.db.GetCollection<Pagamento>("pagamentos");
            LojasDb = MainWindowViewModel.Instance.db.GetCollection<Loja>("lojas");
            MembrosDb = MainWindowViewModel.Instance.db.GetCollection<Membro>("membros");
            //Pagamentos = new ObservableCollection<Pagamento>(PagamentosDb.FindAll().ToList());
            Lojas = LojasDb.FindAll().ToList();
            LojaSelecionada = Lojas.FirstOrDefault();
        }

        public void SelecionaPagamentos()
        {
            if (LojaSelecionada != null)
            {
                Pagamentos = new ObservableCollection<Pagamento>(PagamentosDb.FindAll().ToList());
                Pagamentos = new ObservableCollection<Pagamento>(Pagamentos.Where(x => x.LojaId == LojaSelecionada.Id && x.DataVencimento.Month == DataSelecionada.Month && x.DataVencimento.Year == DataSelecionada.Year).ToList());
                foreach (var item in Pagamentos)
                {
                    item.LojaNome = LojaSelecionada.Nome;
                    item.MembroNome = MembrosDb.FindById(item.MembroId).Nome;
                }
            }
            else
                return;

            foreach (var membro in MembrosDb.Find(x=>x.Lojas.Exists(v=>v == LojaSelecionada.Id)).ToList())
            {
                if (Pagamentos.Where(x => x.MembroId == membro.Id && x.LojaId == LojaSelecionada.Id && x.DataVencimento.Month == DataSelecionada.Month && x.DataVencimento.Year == DataSelecionada.Year).Count() == 0)
                {
                    var pagamento = new Pagamento()
                    {
                        MembroId = membro.Id,
                        DataVencimento = new DateTime(DataSelecionada.Year, DataSelecionada.Month, 10),
                        LojaId = LojaSelecionada.Id,
                        StatusPagamento = StatusPagamento.Irregular,
                        LojaNome = LojaSelecionada.Nome,
                        MembroNome = membro.Nome
                    };

                    PagamentosDb.Insert(pagamento);
                    Pagamentos.Add(pagamento);
                }
            }
        }
    }
}
