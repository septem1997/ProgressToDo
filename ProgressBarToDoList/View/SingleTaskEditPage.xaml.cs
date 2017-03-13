using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ProgressBarToDoList.Module;
using ProgressBarToDoList.ViewModule;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProgressBarToDoList.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SingleTaskEditPage : Page
    {
        private SingleTaskItem _singleTaskItem;

        public SingleTaskEditPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _singleTaskItem = (SingleTaskItem) e.Parameter;
            if (string.IsNullOrEmpty(GroupBox.PlaceholderText))
            {
                GroupBox.PlaceholderText = "默认分组";
            }
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
            double maxValue;
            double value;
            double dopamine;
            try
            {
                 maxValue = double.Parse(MaxValueBox.Text);
                 value = double.Parse(CompletedValue.Text);
                 dopamine = double.Parse(Dopamine.Text);
            }
            catch (FormatException)
            {
                dialog.Content = "任务进程数及多巴胺只能输入数字";
                await dialog.ShowAsync();
                return;
            }
            if (dopamine < 0)
            {
                dialog.Content = "多巴胺不能小于0";
                await dialog.ShowAsync();
                return;
            }
            
            if (maxValue < 0 || value < 0)
            {
                dialog.Content = "任务进程数不能小于0";
                await dialog.ShowAsync();
                return;
            }
            if (maxValue < value)
            {
                dialog.Content = "已完成的任务进程数不能大于总的进程数";
                await dialog.ShowAsync();
                return;
            }
            



            var name = TaskNameTextBox.Text;
            var unite = UniteNameBox.Text;
            var note = NoteBox.Text;
            var deadline = CalendarDatePicker.Date?.ToString("yyyy-MM-dd");

            if (deadline==null)
            {
                dialog.Content = "必须选择一个截止日期";
                await dialog.ShowAsync();
                return;
            }
            var group = GroupBox.PlaceholderText;

            TaskManager.GetTaskGroupByName(group).TaskItems.Add(new SingleTaskItem(maxValue, value, deadline, dopamine, name, note, group, unite));


            if (_singleTaskItem != null)
            {
                TaskManager.GetTaskGroupByName(group).TaskItems.Remove(_singleTaskItem);
                //TaskManager.GetTaskGroupByName(group).TaskItems.Add(new SingleTaskItem(maxValue, value, deadline, dopamine, name, note,group, unite));
            }
//            else
//            {
//                _singleTaskItem.MaxValue = maxValue;
//                _singleTaskItem.ProgressValue = value;
//                _singleTaskItem.DeadLine = deadline;
//                _singleTaskItem.Dopamine = dopamine;
//                _singleTaskItem.TaskName = name;
//                _singleTaskItem.UnitName = unite;
//                _singleTaskItem.Note = note;
//                var d = (value / maxValue * 100);
//                _singleTaskItem.ProgressTips = "当前已完成" + value + "/" + maxValue + unite + " (" + d.ToString("##.00") + "%)";
//            }


            Frame.GoBack();
        }
    }
}