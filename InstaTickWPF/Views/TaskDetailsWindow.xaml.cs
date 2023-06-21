using System;
using System.Collections;
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

namespace InstaTickWPF
{
    /// <summary>
    /// Interaction logic for TaskDetailsWindow.xaml
    /// </summary>
    public partial class TaskDetailsWindow : Window
    {
        private Task _task;

        public TaskDetailsWindow(Task task)
        {
            InitializeComponent();

            // Set the data context to the task
            this.DataContext = task;

            _task = task;

            // Assuming you have TextBoxes named taskNameTextBox, taskDescriptionTextBox, etc.
            taskNameTextBox.Text = _task.Name;
            taskDescriptionTextBox.Text = _task.Description;
            dueDateTextBox.Text = _task.DueDate.ToString();
            priorityTextBox.Text = _task.Priority.ToString();
            // If Category is a class, you might need to display a property of it, like _task.Category.Name
            categoryTextBox.Text = _task.Category.ToString();

            // Do something similar for SubTasks
            //subTasksListBox.ItemsSource = _task.SubTasks;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
