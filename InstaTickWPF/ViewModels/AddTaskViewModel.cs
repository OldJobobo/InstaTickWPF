using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;

namespace InstaTickWPF
{
    public class AddTaskViewModel : IWindowViewModel
    {
        private string _name;
        private string _description;

        public ObservableCollection<CategoryViewModel> Categories { get; set; }
        public ObservableCollection<PriorityViewModel> Priorities { get; set; }

        // public List<string> Priorities { get; } = new List<string> { Priority.Low, Priority.Medium, Priority.High, Priority.Urgent };


        public event Action<Task> TaskAdded = delegate { };
        //public event Action RequestClose;

        public event Action RequestClose = delegate { };

        private readonly IWindowService _windowService;

        public ICommand AddCommand { get; }

        public ICommand CancelCommand { get; }

        public AddTaskViewModel(ObservableCollection<CategoryViewModel> categories, ObservableCollection<PriorityViewModel> priorities)
        {
            // Set the default DueDate value when the ViewModel is created
            DueDate = DateTime.Today;

            Categories = categories;

            Priorities = priorities;

            AddCommand = new RelayCommand(AddTask);

            CancelCommand = new RelayCommand(CancelTask);
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        private DateTime _dueDate;
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged();
            }
        }

        private CategoryViewModel _selectedCategory;
        public CategoryViewModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }


        private PriorityViewModel _selectedPriority;
        public PriorityViewModel SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                _selectedPriority = value;
                OnPropertyChanged();
            }
        }


        private bool _isComplete;
        public bool IsComplete
        {
            get => _isComplete;
            set
            {
                _isComplete = value;
                OnPropertyChanged();
            }
        }

        private void AddTask()
        {
            System.Diagnostics.Debug.WriteLine("AddTask method called");

            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Task name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var task = new Task 
            {
                Name = this.Name,
                Description = this.Description,
                DueDate = this.DueDate,
                IsComplete = false,
                Priority = SelectedPriority != null ? SelectedPriority.Name : null,
                Category = SelectedCategory != null ? SelectedCategory.Name : null
            };
            TaskAdded(task);

            RequestClose?.Invoke();

        }

        private void CancelTask()
        {
            System.Diagnostics.Debug.WriteLine("CancelTask method called");

            RequestClose?.Invoke();

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
