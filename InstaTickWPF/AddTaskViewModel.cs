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

namespace InstaTickWPF
{
    public class AddTaskViewModel : IWindowViewModel
    {
        private string _title;
        private string _description;

        public event Action<Task> TaskAdded = delegate { };
        //public event Action RequestClose;

        public event Action RequestClose = delegate { };

        private readonly IWindowService _windowService;

        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public AddTaskViewModel()
        {
            // Set the default DueDate value when the ViewModel is created
            DueDate = DateTime.Today;

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
       
        private Priority _priority;
        public Priority Priority
        {
            get => _priority;
            set
            {
                _priority = value;
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

            var task = new Task 
            { 
                Title = Title, 
                Description = Description, 
                DueDate = DueDate, 
                Priority = Priority 
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
