using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaTickWPF
{
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private Category _category;

        public CategoryViewModel(Category category)
        {
            _category = category;
        }

        public string Name
        {
            get { return _category.Name; }
            set
            {
                if (_category.Name != value)
                {
                    _category.Name = value;
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
