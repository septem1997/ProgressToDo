using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProgressBarToDoList.Annotations;

namespace ProgressBarToDoList.Module
{
    class TaskGroup : INotifyPropertyChanged
    {
        private string _name;
        private bool _isExpanded;
        private ObservableCollection<TaskItem> _taskItems;

        public TaskGroup(string name, bool isExpanded, ObservableCollection<TaskItem> taskItems)
        {
            _name = name;
            _isExpanded = isExpanded;
            _taskItems = taskItems;
        }

        public string Name
        {
            set
            {
                _name = value;
                OnPropertyChanged();
            }
            get { return _name; }
        }

        public bool IsExpanded
        {
            set
            {
                _isExpanded = value;
                OnPropertyChanged();
            }
            get { return _isExpanded; }
        }

        public ObservableCollection<TaskItem> TaskItems
        {
            set
            {
                _taskItems = value;
                OnPropertyChanged();
            }
            get { return _taskItems; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
