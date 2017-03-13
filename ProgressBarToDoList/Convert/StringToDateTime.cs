using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ProgressBarToDoList.Convert
{
    class StringToDateTime:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var s = (string) value;
            var date = new DateTimeOffset(DateTime.Parse(s));
            return date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
