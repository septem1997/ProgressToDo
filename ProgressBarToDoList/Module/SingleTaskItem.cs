using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using ProgressBarToDoList.Annotations;

namespace ProgressBarToDoList.Module
{
    class SingleTaskItem:TaskItem
    {
        public string UnitName { get; set; }
        

        public SingleTaskItem(double maxValue, double progressValue, string deadLine, double dopamine, string taskName, string note, string group,string unitName) : base(maxValue, progressValue, deadLine, dopamine, taskName, note,group)
        {
            UnitName = unitName;
            var d = (ProgressValue / MaxValue * 100);
            ProgressTips = "当前已完成" + progressValue + "/" + maxValue +unitName+ " (" + d.ToString("##.00") + "%)";
        }

        public SingleTaskItem() : base()
        {
            
        }
    }
}
