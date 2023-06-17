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
        private string _title;
        private string _description;

        public ObservableCollection<CategoryViewModel> Categories { get; set; }


        public event Action<Task> TaskAdded = delegate { };
        //public event Action RequestClose;

        public event Action RequestClose = delegate { };

        private readonly IWindowService _windowService;

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public AddTaskViewModel(ObservableCollection<CategoryViewModel> categories)
        {
            // Set the default DueDate value when the ViewModel is created
            DueDate = DateTime.Today;

            Categories = categories;

            AddCommand = new RelayCommand(AddTask);

            CancelCommand = new RelayCommand(CancelTask);
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
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
       
        private string _priority;
        public string Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged();
            }
        }

        private string _selectedPriority;
        public string SelectedPriority
        {
            get { return _selectedPriority; }
            set
            {
                _selectedPriority = value;
                OnPropertyChanged(nameof(SelectedPriority));
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

            var task = new Task 
            {
                Title = this.Title,
                Description = this.Description,
                DueDate = this.DueDate,
                IsComplete = false,
                Priority = this.SelectedPriority
            };
            TaskAdded(task);

            /*System.Diagnostics.Debug.WriteLine(z"RequestClose event is null: " + (RequestClose == null));

            if (RequestClose != null)
            {
                var invocationList = RequestClose.GetInvocationList();
                System.Diagnostics.Debug.WriteLine("RequestClose invocation list count: " + invocationList.Length);
            }

            System.Diagnostics.Debug.WriteLine("About to invoke RequestClose event");
            */


            RequestClose?.Invoke();

            // Directly invoke the Close method here
           //((AddTaskWindow)Application.Current.Windows.OfType<AddTaskWindow>().FirstOrDefault())?.CloseMethod();
        }

        private void CancelTask()
        {
            System.Diagnostics.Debug.WriteLine("CancelTask method called");
            System.Diagnostics.Debug.WriteLine("About to invoke RequestClose event");
            RequestClose?.Invoke();

            // Directly invoke the Close method here
           // ((AddTaskWindow)Application.Current.Windows.OfType<AddTaskWindow>().FirstOrDefault())?.CloseMethod();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
