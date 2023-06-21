using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaTickWPF
{
    


    [Serializable]
    public class Task : INotifyPropertyChanged
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

     

      //  public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }

        public ObservableCollection<SubTask> SubTasks { get; set; }

        public Task()
        {
            SubTasks = new ObservableCollection<SubTask>();
        }

        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(Name); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }




}
