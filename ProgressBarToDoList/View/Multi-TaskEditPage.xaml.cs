using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ProgressBarToDoList.Module;
using ProgressBarToDoList.ViewModule;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProgressBarToDoList.View
{
    public sealed partial class Multi_TaskEditPage : Page
    {
        private MultiTaskItem _multiTaskItem;
        private ObservableCollection<SimpleTaskItem> _simpleTaskItems;

        public Multi_TaskEditPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _multiTaskItem = (MultiTaskItem) e.Parameter;
            _simpleTaskItems = _multiTaskItem != null
                ? _multiTaskItem.SimpleTaskItems
                : new ObservableCollection<SimpleTaskItem>();
            if (string.IsNullOrEmpty(GroupBox.PlaceholderText))
            {
                GroupBox.PlaceholderText = "默认分组";
            }
            //从数据库获取分组列表

        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void AcceptButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog()
            {
                Title = "参数填写错误",
                PrimaryButtonText = "朕知道了"
            };

            var name = TaskNameTextBox.Text;
            var count = 0;
            double value = 0, maxValue = 0;
            foreach (var item in _simpleTaskItems)
            {
                if (item.Weight < 0)
                {
                    dialog.Content = "任务的权重不能小于0";
                    await dialog.ShowAsync();
                    return;
                }
                maxValue += item.Weight;
                if (item.IsComplete)
                {
                    count++;
                    value += item.Weight;
                }
            }
            double dopamine;
            try
            {
                dopamine = double.Parse(Dopamine.Text);
            }
            catch (FormatException)
            {
                dialog.Content = "多巴胺文本编辑框必须输入数字";
                await dialog.ShowAsync();
                return;
            }
            if (dopamine<0)
            {
                dialog.Content = "多巴胺不能小于0";
                await dialog.ShowAsync();
                return;
            }
            var note = NoteBox.Text;
            var deadline = CalendarDatePicker.Date?.ToString("yyyy-MM-dd");
            if (deadline==null)
            {
                dialog.Content = "必须选择一个截止日期";
                await dialog.ShowAsync();
                return;
            }
            var group = GroupBox.PlaceholderText;
            TaskManager.GetTaskGroupByName(group).TaskItems.Add(new MultiTaskItem(count, maxValue, value, deadline, dopamine, name, note, group, _simpleTaskItems));

            if (_multiTaskItem != null)
            {
                TaskManager.GetTaskGroupByName(group).TaskItems.Remove(_multiTaskItem);
            }
//            else
//            {
//                _multiTaskItem = new MultiTaskItem(count,maxValue, value, deadline, dopamine, name, note,group, _simpleTaskItems);
//            }
            Frame.GoBack();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _simpleTaskItems.Add(new SimpleTaskItem());
        }


        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var s = fe.DataContext as SimpleTaskItem;
            _simpleTaskItems.Remove(s);
        }
    }
}
