using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProgressBarToDoList.Annotations;

namespace ProgressBarToDoList.Module
{
    class SimpleTaskItem:ITaskItemBase
    {
        public string TaskName { get; set; }
        public string Note { get; set; }
        public bool IsComplete { get; set; }
        public double Weight { get; set; }

        public SimpleTaskItem()
        {
            Weight = 1;
        }

        public SimpleTaskItem(string taskName, string note, bool isComplete, double weight)
        {
            TaskName = taskName;
            Note = note;
            IsComplete = isComplete;
            Weight = weight;
        }

    }
    
}
