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
    /// Interação lógica para LojaView.xam
    /// </summary>
    public partial class LojasView : Page
    {
        LojaViewModel viewModel;
        public LojasView()
        {
            viewModel = new LojaViewModel();
            InitializeComponent();

            DataContext = viewModel;
        }

        private void salva_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LojasDb.Upsert(viewModel.LojaSelecionada);
            viewModel.Lojas = viewModel.LojasDb.FindAll().ToList();
            flyout.IsOpen = false;
        }

        private void cancela_click(object sender, RoutedEventArgs e)
        {
            viewModel.LojaSelecionada = new Models.Loja();
            flyout.IsOpen = false;
        }

        private async void procura_endrecero_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.LojaSelecionada.Endereco = await MainWindowViewModel.Instance.GetEnderecoAsync(viewModel.LojaSelecionada.Endereco.Cep.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void select(object sender, MouseButtonEventArgs e)
        {
            flyout.IsOpen = true;
        }

        private void novo_btn_Click(object sender, RoutedEventArgs e)
        {
            flyout.IsOpen = true;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.MembrosSelecionados = new ObservableCollection<Models.Membro>(viewModel.MembrosDb.Find(x=>x.Lojas.Contains(viewModel.LojaSelecionada.Id)).ToList());
        }
    }
}
