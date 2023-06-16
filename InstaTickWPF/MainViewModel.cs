using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InstaTickWPF
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // public ObservableCollection<Task> Tasks { get; set; } = new ObservableCollection<Task>();

        private readonly IWindowService _windowService = new WindowService();

        private ObservableCollection<Task> _tasks = new ObservableCollection<Task>();

        public ObservableCollection<CategoryViewModel> Categories { get; set; }


        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; set; }

        private Task _selectedTask;

        
        public MainViewModel()
        {
            AddTaskCommand = new RelayCommand(OpenAddTaskWindow);

            Tasks = new ObservableCollection<Task>();

            Categories = new ObservableCollection<CategoryViewModel>();
            // populate the Categories collection
            Categories.Add(new CategoryViewModel(new Category { Name = "Work" }));
            Categories.Add(new CategoryViewModel(new Category { Name = "Personal" }));
          

            RemoveTaskCommand = new RelayCommand(
                execute: () => {
                    if (SelectedTask != null)
                    {
                        Tasks.Remove(SelectedTask);
                        SelectedTask = null;
                    }
                },
                canExecute: () => SelectedTask != null
            );
        }

        private void OpenAddTaskWindow()
        {
            var addTaskViewModel = new AddTaskViewModel();

            addTaskViewModel.TaskAdded += task => Tasks.Add(task);

            _windowService.OpenWindow(addTaskViewModel);
        }


        public void AddTask()
        {
            var newTask = new Task
            {
                Title = "New Task",
                Description = "This is a new task",
                DueDate = DateTime.Now.AddDays(1),
                IsComplete = false
            };

            Tasks.Add(newTask);
        }

        private bool CanAddTask(object obj)
        {
            // Add logic here to decide if a new task can be added.
            // For now, let's just return true.
            return true;
        }

        public void RemoveTask(Task task)
        {
            Tasks.Remove(task);
            OnPropertyChanged("Tasks");
        }

        public Task SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
                ((RelayCommand)RemoveTaskCommand).RaiseCanExecuteChanged(); // Notify the RemoveTaskCommand that its executability might have changed
            }
        }



        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
