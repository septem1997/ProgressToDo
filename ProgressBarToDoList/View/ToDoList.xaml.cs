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
using ProgressBarToDoList.View;
using ProgressBarToDoList.ViewModule;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProgressBarToDoList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ToDoList : Page
    {
        private ObservableCollection<TaskGroup> _unfinishItems;
        private SingleTaskItem _singleTaskItem;
        private MultiTaskItem _multiTaskItem;
        private ObservableCollection<SimpleTaskItem> _simpleTaskItems;

        public ToDoList()
        {
            this.InitializeComponent();
            _unfinishItems = TaskManager.GetItems();
            NavigationCacheMode = NavigationCacheMode.Required;
        }


        private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var creatTaskContentDialog = new ContentDialog()
            {
                Title = "创建新任务",
                Content = "你想创建哪种类型的任务?",
                PrimaryButtonText = "单一重复型任务",
                SecondaryButtonText = "多步骤型任务"
            };

            var result = await creatTaskContentDialog.ShowAsync();


            this.Frame.Navigate(
                result == ContentDialogResult.Primary
                    ? typeof(View.SingleTaskEditPage)
                    : typeof(View.Multi_TaskEditPage), null);
        }


        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var item = fe.DataContext as TaskItem;
            var sFlyout = fe.FindName("SingleTaskFlyout") as StackPanel;
            var mFlyout = fe.FindName("MultiTaskFlyout") as Grid;
            if (item is SingleTaskItem)
            {
                sFlyout.Visibility = Visibility.Visible;
                mFlyout.Visibility = Visibility.Collapsed;

                FlyoutBase.ShowAttachedFlyout(fe);
                _singleTaskItem = (SingleTaskItem) item;
            }
            else
            {
                sFlyout.Visibility = Visibility.Collapsed;
                mFlyout.Visibility = Visibility.Visible;
                var listview = mFlyout.FindName("SimpleTaskList") as ListView;
                var multiTaskItem = item as MultiTaskItem;
                listview.ItemsSource = multiTaskItem.SimpleTaskItems;
                FlyoutBase.ShowAttachedFlyout(fe);
                _multiTaskItem = multiTaskItem;
            }
        }


        private void ProgressBarInSingleTaskFlyout_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (_singleTaskItem == null)
                return;
            _singleTaskItem.ProgressValue = e.NewValue;
            var d = _singleTaskItem.ProgressValue / _singleTaskItem.MaxValue * 100;
            _singleTaskItem.ProgressTips = "当前已完成" + _singleTaskItem.ProgressValue + "/" + _singleTaskItem.MaxValue +
                                           _singleTaskItem.UnitName + " (" + d.ToString("##.00") + "%)";
        }

        private void InOrDecreaseButton_OnClick(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (b.Name == "IncreaseButton" && _singleTaskItem.ProgressValue < _singleTaskItem.MaxValue)
            {
                _singleTaskItem.ProgressValue++;
            }
            if (b.Name == "DecreaseButton" && _singleTaskItem.ProgressValue > 0)
            {
                _singleTaskItem.ProgressValue--;
            }
        }


        private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "你确定要删除该任务吗？",
                PrimaryButtonText = "确定",
                SecondaryButtonText = "手滑点错了"
            };
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var fe = sender as FrameworkElement;
                var item = fe.DataContext as TaskItem;
                TaskManager.GetTaskGroupByName(item.Group).TaskItems.Remove(item);
            }
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var item = fe.DataContext;
            if (item is SingleTaskItem)
            {
                Frame.Navigate(typeof(SingleTaskEditPage), item);
            }
            else
            {
                Frame.Navigate(typeof(Multi_TaskEditPage), item);
            }
        }

        private void ToDoList_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void FlyoutBase_OnClosed(object sender, object e)
        {
            //更新数据
            Debug.WriteLine("close flyout");
        }

        private void CompleteCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            var item = fe.DataContext as SimpleTaskItem;

            var checkBox = sender as CheckBox;

            var count = _multiTaskItem.SimpleTaskItems.Count(t => t.IsComplete);
            if (checkBox.IsChecked == true)
            {
                Debug.WriteLine("checked");
                _multiTaskItem.ProgressValue = +item.Weight;
                count++;
            }
            else
            {
                Debug.WriteLine("unchecked");
                _multiTaskItem.ProgressValue = -item.Weight;
                count--;
            }
            var d = _multiTaskItem.ProgressValue / _multiTaskItem.MaxValue * 100;
            _multiTaskItem.ProgressTips = "当前已完成" + _multiTaskItem.SimpleTaskItems.Count + "项任务中的" + count + "项 (" +
                                          d.ToString("##.00") + "%)";
        }
    }
}