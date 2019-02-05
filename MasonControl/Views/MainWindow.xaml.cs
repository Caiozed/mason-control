using MahApps.Metro.Controls;
using MasonControl.ViewModels;
using System;
using System.Windows;

namespace MasonControl.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        MainWindowViewModel viewModel;
        public MainWindow()
        {
            viewModel = new MainWindowViewModel();
            InitializeComponent();
            this.Loaded += OnLoaded;

            DataContext = viewModel;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            viewModel.frame = frame;
            viewModel.menu = Menu;
            frame.Navigate(new LoginView());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu.IsOpen = !Menu.IsOpen;
        }
    }
}
