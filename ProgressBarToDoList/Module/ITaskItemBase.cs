using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressBarToDoList.Module
{
    interface ITaskItemBase
    {
        string TaskName { set; get; }
        string Note { set; get; }
        bool IsComplete { set; get; }
    }
}
