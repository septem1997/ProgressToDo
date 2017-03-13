using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using ProgressBarToDoList.ViewModule;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProgressBarToDoList.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            TaskManager.InitApp();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ToDoList.IsSelected)
                MainFrame.Navigate(typeof(ToDoList));
            else if (Store.IsSelected)
                MainFrame.Navigate(typeof(Store));
            else if (Setting.IsSelected)
                MainFrame.Navigate(typeof(Setting));
        }

        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            MainFrame.ContentTransitions.Clear();
            MainFrame.ContentTransitions.Add(new EntranceThemeTransition());
        }
    }
}
