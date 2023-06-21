using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InstaTickWPF
{
    public class TaskDetailsViewModel : IWindowViewModel, INotifyPropertyChanged
    {
        private string _name;
        private string _description;

        public ObservableCollection<CategoryViewModel> Categories { get; set; }
        public ObservableCollection<PriorityViewModel> Priorities { get; set; }

        public event Action RequestClose = delegate { };
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IWindowService _windowService;

        public ICommand CancelCommand { get; private set; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public TaskDetailsViewModel(Task task, ObservableCollection<CategoryViewModel> categories, ObservableCollection<PriorityViewModel> priorities)
        {
            Categories = categories;
            Priorities = priorities;
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Cancel()
        {
            RequestClose();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
