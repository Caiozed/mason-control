﻿using MasonControl.ViewModels;
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
    /// Interação lógica para LoginView.xam
    /// </summary>
    public partial class LoginView : Page
    {
        LoginViewModel viewModel;
        public LoginView()
        {
            viewModel = new LoginViewModel();
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}