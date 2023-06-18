using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaTickWPF
{
    public class PriorityViewModel : INotifyPropertyChanged
    {
        private Priority _priority;

        public PriorityViewModel(Priority priority)
        {
            _priority = priority;
        }

        public string Name
        {
            get { return _priority.Name; }
            set
            {
                if (_priority.Name != value)
                {
                    _priority.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        // This is the PropertyChanged event declaration
        public event PropertyChangedEventHandler PropertyChanged;

        // Implement INotifyPropertyChanged
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
