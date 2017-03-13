using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace ProgressBarToDoList.Module
{
    class MultiTaskItem:TaskItem
    {
        
        public ObservableCollection<SimpleTaskItem> SimpleTaskItems { set; get; }
        
        public MultiTaskItem(int count,double maxValue, double progressValue, string deadLine, double dopamine, string taskName, string note,string group,ObservableCollection<SimpleTaskItem> simpleTaskItems ) : base(maxValue, progressValue, deadLine, dopamine, taskName, note,group)
        {
            var d = progressValue/maxValue*100;
            SimpleTaskItems = simpleTaskItems;
            ProgressTips = "当前已完成" + simpleTaskItems.Count + "项任务中的" + count + "项 (" + d.ToString("##.00") + "%)";
        }
        
    }
}
