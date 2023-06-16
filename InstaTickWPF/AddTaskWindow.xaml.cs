﻿using System;
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
using System.Windows.Shapes;
using System.ComponentModel;

namespace InstaTickWPF
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    /// 

    public partial class AddTaskWindow : Window
    {
        private AddTaskViewModel _viewModel;
       

        public AddTaskWindow()
        {
            InitializeComponent();

          

            _viewModel = new AddTaskViewModel();
            _viewModel.RequestClose += CloseMethod; // Subscribe to RequestClose event here

            this.DataContext = _viewModel;
        }

        public void CloseMethod() // Renamed method to prevent hiding of base.Close
        {
            System.Diagnostics.Debug.WriteLine("CloseMethod method called"); // Logs when CloseMethod is triggered
            _viewModel.RequestClose -= CloseMethod; // Unsubscribe from RequestClose event here
            this.Close(); // Calls the base class's Close method
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            categoryComboBox.ItemsSource = MainViewModel.Categories; // Replace with your list of categories
            categoryComboBox.DisplayMemberPath = "CategoryName"; // Replace with the property of Category that you want to display
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // _viewModel.RequestClose -= CloseMethod; // No need to unsubscribe here
        }
    }
}