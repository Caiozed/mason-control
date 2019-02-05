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
    /// Interação lógica para StatusView.xam
    /// </summary>
    public partial class StatusView : Page
    {
        StatusViewModel viewModel;
        public StatusView()
        {
            viewModel = new StatusViewModel();
            InitializeComponent();

            DataContext = viewModel;
        }


        private void salva_Click(object sender, RoutedEventArgs e)
        {
            flyout.IsOpen = false;
            viewModel.StatusDb.Upsert(viewModel.StatusSelecionado);
            viewModel.SelecionaStatus();
        }

        private void cancela_click(object sender, RoutedEventArgs e)
        {
            flyout.IsOpen = false;
        }

        private void select(object sender, MouseButtonEventArgs e)
        {
            flyout.IsOpen = true;
        }

        private void novo_btn_Click(object sender, RoutedEventArgs e)
        {
            flyout.IsOpen = true;
        }

        private void Filtro_SelecionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SelecionaStatus();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel.StatusSelecionado != null)
            {
                viewModel.LojaSelecionada = viewModel.LojasDb.FindById(viewModel.StatusSelecionado.LojaId);
                viewModel.MembroSelecionado = viewModel.MembrosDb.FindById(viewModel.StatusSelecionado.MembroId);
            }
        }
    }
}
