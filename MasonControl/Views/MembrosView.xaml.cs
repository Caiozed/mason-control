using MasonControl.Models;
using MasonControl.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasonControl.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class MembrosView : UserControl
    {
        MembrosViewModel viewModel;
        public MembrosView()
        {
            viewModel = new MembrosViewModel();
            InitializeComponent();

            DataContext = viewModel;
        }

        private void novo_btn_Click(object sender, RoutedEventArgs e)
        {
            membro_flyout.IsOpen = true;
        }

        private async void procura_endrecero_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.MembroSelecionado.Endereco = await MainWindowViewModel.Instance.GetEnderecoAsync(viewModel.MembroSelecionado.Endereco.Cep.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void salva_membro_Click(object sender, RoutedEventArgs e)
        {
            viewModel.MembrosDb.Upsert(viewModel.MembroSelecionado);
            viewModel.Membros = viewModel.MembrosDb.FindAll().ToList();

            foreach (var loja in viewModel.MembroSelecionado.Lojas)
            {
                var status = viewModel.StatusDb.FindAll().ToList();
                if (status.Where(x => x.LojaId == loja && x.MembroId == viewModel.MembroSelecionado.Id).Count() == 0)
                {
                    var newstatus = new StatusMembro() { LojaId = loja, MembroId = viewModel.MembroSelecionado.Id, DataIniciacao = DateTime.Now };
                    viewModel.StatusDb.Upsert(newstatus);
                }
            }
          
            viewModel.MembroSelecionado = new Models.Membro();
            viewModel.LojasSelecionadas = new ObservableCollection<Loja>();
            membro_flyout.IsOpen = false;
        }

        private void cancela_click(object sender, RoutedEventArgs e)
        {
            viewModel.MembroSelecionado = new Models.Membro();
            membro_flyout.IsOpen = false;
        }

        private void select(object sender, MouseButtonEventArgs e)
        {
            membro_flyout.IsOpen = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            viewModel.MembroSelecionado.Lojas = new List<LiteDB.ObjectId>();
            viewModel.LojasSelecionadas = new ObservableCollection<Loja>();
            foreach (var item in list.SelectedItems)
            {
                viewModel.MembroSelecionado.Lojas.Add((item as Loja).Id);
                viewModel.LojasSelecionadas.Add(item as Loja);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel.MembroSelecionado != null)
            {
                viewModel.StatusSelecionados = new ObservableCollection<StatusMembro>(MainWindowViewModel.Instance.db.GetCollection<StatusMembro>("statusMembro").Find(x => x.MembroId == viewModel.MembroSelecionado.Id));
                viewModel.LojasSelecionadas = new ObservableCollection<Loja>();
                foreach (var id in viewModel.MembroSelecionado.Lojas)
                {
                    viewModel.LojasSelecionadas.Add(viewModel.LojasDb.FindById(id));
                }
            }
        }
    }
}
