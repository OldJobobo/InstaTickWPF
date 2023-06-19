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
using Newtonsoft.Json;
using System.IO;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;

namespace InstaTickWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
            this.DataContext = ViewModel;
        }

        private void CategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategory = (CategoryViewModel)categoryListView.SelectedItem;

            if (selectedCategory != null)
            {
                var filteredTasks = ViewModel.Tasks.Where(task => task.Category == selectedCategory.Name);
                taskListView.ItemsSource = new ObservableCollection<Task>(filteredTasks);
            }
            else
            {
                taskListView.ItemsSource = null;
            }

        }

        private void Inbox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Clear selection from the categories ListBox.
            categoryListView.SelectedItem = null;

            // Show all tasks.
            taskListView.ItemsSource = new ObservableCollection<Task>(ViewModel.Tasks);
        }


        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                using (var reader = new StreamReader(openFileDialog.FileName))
                {
                    string json = reader.ReadToEnd();
                    var tasks = JsonConvert.DeserializeObject<List<Task>>(json);
                    ViewModel.Tasks = new ObservableCollection<Task>(tasks);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON files (*.json)|*.json";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var writer = new StreamWriter(saveFileDialog.FileName))
                {
                    string json = JsonConvert.SerializeObject(ViewModel.Tasks.ToList(), Formatting.Indented);
                    writer.Write(json);
                }
            }
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



    }
}
