using MasonControl.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interação lógica para UsuariosViewxaml.xam
    /// </summary>
    public partial class UsuariosView : Page
    {
        UsuarioViewModel viewModel;
        public UsuariosView()
        {
            viewModel = new UsuarioViewModel();
            InitializeComponent();

            DataContext = viewModel;
        }

        private void salva_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(viewModel.UsuarioSelecionado.Login) && !string.IsNullOrEmpty(viewModel.UsuarioSelecionado.Senha))
            {
                viewModel.UsuarioDb.Upsert(viewModel.UsuarioSelecionado);
                viewModel.Usuarios.Add(viewModel.UsuarioSelecionado);
                flyout.IsOpen = false;
            }
        }

        private void cancela_click(object sender, RoutedEventArgs e)
        {
            flyout.IsOpen = false;
            viewModel.UsuarioSelecionado = new Models.Usuario();
        }

        private void select(object sender, MouseButtonEventArgs e)
        {
            flyout.IsOpen = true;
        }

        private void novo_btn_Click(object sender, RoutedEventArgs e)
        {
            flyout.IsOpen = true;
        }
    }
}
