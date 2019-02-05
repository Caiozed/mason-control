using LiteDB;
using MasonControl.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasonControl.ViewModels
{
    public class LojaViewModel : BindableBase
    {
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

        private ObservableCollection<Membro> _membrosSelecionados;
        public ObservableCollection<Membro> MembrosSelecionados
        {
            get { return _membrosSelecionados; }
            set { SetProperty(ref _membrosSelecionados, value); }
        }

        private string _search;
        public string Search
        {
            get { return _search; }
            set { SetProperty(ref _search, value); }
        }

        public DelegateCommand ProcuraCommand { get; set; }

        public LojaViewModel()
        {
            ProcuraCommand = new DelegateCommand(Procura);
            LojaSelecionada = new Loja();
            MembrosSelecionados = new ObservableCollection<Membro>();
            LojasDb = MainWindowViewModel.Instance.db.GetCollection<Loja>("lojas");
            MembrosDb = MainWindowViewModel.Instance.db.GetCollection<Membro>("membros");

            Lojas = LojasDb.FindAll().ToList();
        }

        public void Procura()
        {
            if (!string.IsNullOrEmpty(Search))
                Lojas = LojasDb.Find(x => x.Nome.ToLower().StartsWith(Search.ToLower())).ToList();
            else
                Lojas = LojasDb.FindAll().ToList();
        }
    }
}
