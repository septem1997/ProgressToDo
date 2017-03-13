using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProgressBarToDoList.Annotations;

namespace ProgressBarToDoList.ViewModule
{
    class AppSetting:INotifyPropertyChanged
    {

        private bool _dataChanged;

        public bool DataChanged
        {
            get { return _dataChanged; }
            set
            {
                _dataChanged = value;
                //储存到本地
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
