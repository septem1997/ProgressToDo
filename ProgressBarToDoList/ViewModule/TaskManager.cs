using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProgressBarToDoList.Annotations;
using ProgressBarToDoList.Module;

namespace ProgressBarToDoList.ViewModule
{
    class TaskManager:INotifyPropertyChanged
    {
        private static TaskManager _taskManager=null;
        private static ObservableCollection<TaskGroup> _unFinishItemsGroups;

        private TaskManager()
        {
            
        }

        public static void InitApp()
        {
            _unFinishItemsGroups = new ObservableCollection<TaskGroup> { new TaskGroup("默认分组",false, new ObservableCollection<TaskItem>()) };
            var defaultGroup = _unFinishItemsGroups[0].TaskItems;
            defaultGroup.Add(new SingleTaskItem(600,233,"2017-05-25",10,"看完xxx","nothing", "默认分组", "页"));
            defaultGroup.Add(new SingleTaskItem(100,1 , "2017-06-25", 10, "戒撸100天", "nothing", "默认分组", "天"));
            var simpleList = new ObservableCollection<SimpleTaskItem>
            {
                new SimpleTaskItem("aaaa", "aaa", false, 1),
                new SimpleTaskItem("bbb", "aaa", false, 1)
            };
            defaultGroup.Add(new MultiTaskItem(0,2,0,"2017-10-18",10,"name","note","默认分组",simpleList));
        }

        public static TaskGroup GetTaskGroupByName(string name)
        {
            return _unFinishItemsGroups.FirstOrDefault(g => g.Name == name);
        }

        public static TaskManager GetTaskManager()
        {
            return _taskManager ?? new TaskManager();
        }

        public static ObservableCollection<TaskGroup> GetItems()
        {
            return _unFinishItemsGroups;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
