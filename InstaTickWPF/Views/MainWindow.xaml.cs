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

        private readonly IWindowService _windowService;


        public MainWindow(IWindowService windowService)
        {
            InitializeComponent();

            _windowService = windowService;

            ViewModel = new MainViewModel();
            this.DataContext = ViewModel;
            // Load tasks from "MyTasks.json" when application starts
            LoadTasks("MyTasks.json");
            _windowService = windowService;
        }

        private void LoadTasks(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    string json = reader.ReadToEnd();
                    var tasks = JsonConvert.DeserializeObject<List<Task>>(json);
                    ViewModel.Tasks = new ObservableCollection<Task>(tasks);
                }
            }
        }

        private void CategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategory = (CategoryViewModel)categoryListView.SelectedItem;

            if (selectedCategory != null)
            {
                var filteredTasks = ViewModel.Tasks.Where(task => task.Category == selectedCategory.Name);
                taskListView.ItemsSource = new ObservableCollection<Task>(filteredTasks);
                // When an item in the categoryListView is selected, clear the selection in the Inbox ListBox.
                inboxListView.SelectedItem = null;
            }
            else
            {
                taskListView.ItemsSource = null;
            }

        }

        private void taskListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (taskListView.SelectedItem is Task selectedTask)
            {
                var taskDetailsViewModel = new TaskDetailsViewModel(selectedTask, ViewModel.Categories, ViewModel.Priorities);
                _windowService.OpenWindow(taskDetailsViewModel);
            }

        }


        private void Inbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // You can check if the selected item is your "Inbox" item. 
            // Here's an example of how you might do that:
            ListBox listBox = sender as ListBox;
            ListBoxItem selectedItem = listBox.SelectedItem as ListBoxItem;
            if (selectedItem != null)
            {
                TextBlock content = selectedItem.Content as TextBlock;

                if (content.Text == "Inbox")
                {
                    // Clear selection from the categories ListBox.
                    categoryListView.SelectedItem = null;

                    // Show all tasks.
                    taskListView.ItemsSource = new ObservableCollection<Task>(ViewModel.Tasks);
                }
            }
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
