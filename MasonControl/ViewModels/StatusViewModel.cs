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
    public class StatusViewModel : BindableBase
    {
        private ObservableCollection<Loja> _lojasSelecionadas;
        public ObservableCollection<Loja> LojasSelecionadas
        {
            get { return _lojasSelecionadas; }
            set { SetProperty(ref _lojasSelecionadas, value); }
        }

        private LiteCollection<Loja> _lojasDb;
        public LiteCollection<Loja> LojasDb
        {
            get { return _lojasDb; }
            set { SetProperty(ref _lojasDb, value); }
        }

        private LiteCollection<StatusMembro> _statusDb;
        public LiteCollection<StatusMembro> StatusDb
        {
            get { return _statusDb; }
            set { SetProperty(ref _statusDb, value); }
        }

        private ObservableCollection<StatusMembro> _statusSelecionados;
        public ObservableCollection<StatusMembro> StatusSelecionados
        {
            get { return _statusSelecionados; }
            set { SetProperty(ref _statusSelecionados, value); }
        }

        private StatusMembro _statusSelecionado;
        public StatusMembro StatusSelecionado
        {
            get { return _statusSelecionado; }
            set { SetProperty(ref _statusSelecionado, value); }
        }

        private List<Loja> _lojas;
        public List<Loja> Lojas
        {
            get { return _lojas; }
            set { SetProperty(ref _lojas, value); }
        }

        private LiteCollection<Membro> _membrosDb;
        public LiteCollection<Membro> MembrosDb
        {
            get { return _membrosDb; }
            set { SetProperty(ref _membrosDb, value); }
        }

        private List<Membro> _membros;
        public List<Membro> Membros
        {
            get { return _membros; }
            set { SetProperty(ref _membros, value); }
        }

        private Membro _membroSelecionado;
        public Membro MembroSelecionado
        {
            get { return _membroSelecionado; }
            set { SetProperty(ref _membroSelecionado, value); }
        }

        private Loja _lojaSelecionada;
        public Loja LojaSelecionada
        {
            get { return _lojaSelecionada; }
            set { SetProperty(ref _lojaSelecionada, value); }
        }

        private ObservableCollection<Membro> _membrosSelecionados;
        public ObservableCollection<Membro> MembrosSelecionados
        {
            get { return _membrosSelecionados; }
            set { SetProperty(ref _membrosSelecionados, value); }
        }

        public StatusViewModel()
        {

            MembrosDb = MainWindowViewModel.Instance.db.GetCollection<Membro>("membros");
            LojasDb = MainWindowViewModel.Instance.db.GetCollection<Loja>("lojas");
            StatusDb = MainWindowViewModel.Instance.db.GetCollection<StatusMembro>("statusMembros");

            Lojas = LojasDb.FindAll().ToList();
            Membros = MembrosDb.FindAll().ToList();
            SelecionaStatus();
        }

        public void SelecionaStatus()
        {
            if (MembroSelecionado == null && LojaSelecionada != null)
            {
                StatusSelecionados = new ObservableCollection<Models.StatusMembro>(StatusDb.Find(w => w.LojaId == LojaSelecionada.Id).ToList());
            }
            else if (MembroSelecionado != null && LojaSelecionada == null)
            {
                StatusSelecionados = new ObservableCollection<Models.StatusMembro>(StatusDb.Find(w => w.MembroId == MembroSelecionado.Id).ToList());
            }
            else if (MembroSelecionado != null && LojaSelecionada != null)
            {
                StatusSelecionados = new ObservableCollection<Models.StatusMembro>(StatusDb.Find(w => w.MembroId == MembroSelecionado.Id && w.LojaId == LojaSelecionada.Id).ToList());
            }

            if (StatusSelecionados != null)
            {
                foreach (var status in StatusSelecionados)
                {
                    status.NomeMembro = MembrosDb.FindById(status.MembroId).Nome;
                }
            }
        }
    }
}
