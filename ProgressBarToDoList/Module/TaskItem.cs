using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using ProgressBarToDoList.Annotations;

namespace ProgressBarToDoList.Module
{
    public abstract class TaskItem : ITaskItemBase, INotifyPropertyChanged
    {
        private bool? _noteVisibility;
        private double _progressValue;
        private string _progressTips;
        private string _deadLine;
        private double _maxvalue;
        private double _dopamine;
        private string _taskName;
        private string _note;
        private string _group;

        public string Group
        {
            set
            {
                _group = value;
                OnPropertyChanged();
            }
            get { return _group; }
        
        }

        public double MaxValue
        {
            set
            {
                _maxvalue = value;
                OnPropertyChanged();
            }
            get { return _maxvalue; }
        }

        
        public double ProgressValue
        {
            set
            {
                _progressValue = value;
                OnPropertyChanged();
            }
            get { return _progressValue; }
        }

        public string CreatDateTime { set; get; }

        public string DeadLine
        {
            set
            {
                _deadLine = value;
                OnPropertyChanged();
            }
            get { return _deadLine; }
        }

        public double Dopamine
        {
            set
            {
                _dopamine = value;
                OnPropertyChanged();
            }
            get { return _dopamine; }
        }
        public string ProgressTips
        {
            set
            {
                _progressTips = value;
                OnPropertyChanged();
            }
            get { return _progressTips; }
        }

        public bool? NoteVisibility
        {
            set
            {
                _noteVisibility = value;
                OnPropertyChanged();
            }
            get { return _noteVisibility; }
        }

        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged();
            }
        }

        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public bool IsComplete { get; set; }

        protected TaskItem(double maxValue, double progressValue, string deadLine, double dopamine, string taskName,
            string note,string group)
        {
            _noteVisibility = false;
            MaxValue = maxValue;
            ProgressValue = progressValue;
            CreatDateTime = DateTime.Now.ToString("yyyy-MM-dd");
            DeadLine = deadLine;
            Dopamine = dopamine;
            TaskName = taskName;
            Note = note;
            IsComplete = false;
            Group = group;
        }

        protected TaskItem()
        {
            _noteVisibility = false;
            CreatDateTime = DateTime.Now.ToString("yyyy-MM-dd");
            IsComplete = false;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //Debug.WriteLine(propertyName+"属性更改");
        }
    }
}