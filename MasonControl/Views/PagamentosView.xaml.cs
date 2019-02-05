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
    /// Interação lógica para Pagamentos.xam
    /// </summary>
    public partial class PagamentosView : Page
    {
        PagamentoViewModel viewModel;
        public PagamentosView()
        {
            viewModel = new PagamentoViewModel();
            InitializeComponent();

            DataContext = viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
                viewModel.SelecionaPagamentos();
        }

        private void Filtro_SelecionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                viewModel.Pagamentos = new ObservableCollection<Pagamento>(viewModel.PagamentosDb.FindAll().ToList());
                viewModel.Pagamentos = new ObservableCollection<Pagamento>(viewModel.Pagamentos.Where(x => x.StatusPagamento == viewModel.StatusSelecionado && x.DataVencimento.Month == viewModel.DataSelecionada.Month && x.DataVencimento.Year == viewModel.DataSelecionada.Year).ToList());
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
                viewModel.SelecionaPagamentos();
        }

        private void status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel.PagamentoSelecionado != null)
            {
                if (viewModel.PagamentoSelecionado.StatusPagamento == StatusPagamento.Irregular)
                    viewModel.PagamentoSelecionado.DataPagamento = new DateTime();
                else
                    viewModel.PagamentoSelecionado.DataPagamento = viewModel.PagamentoSelecionado.DataVencimento;

                viewModel.PagamentosDb.Upsert(viewModel.PagamentoSelecionado);
            }
        }
    }
}
