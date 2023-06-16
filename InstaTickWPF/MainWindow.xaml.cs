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

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";

            if (openFileDialog.ShowDialog() == true)
            {
                var binaryFormatter = new BinaryFormatter();
                using (var fileStream = File.OpenRead(openFileDialog.FileName))
                {
                    var tasks = (List<Task>)binaryFormatter.Deserialize(fileStream);
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
                var binaryFormatter = new BinaryFormatter();
                using (var fileStream = File.OpenWrite(saveFileDialog.FileName))
                {
                    binaryFormatter.Serialize(fileStream, ViewModel.Tasks.ToList());
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
