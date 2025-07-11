using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using TaskFlow.ViewModel;

namespace TaskFlow.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void Minimize_Clicked(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {

            var createTaskPage = App.ServiceProvider.GetService<CreateTaskPage>();
            MainFrame.Navigate(createTaskPage);
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            var tasksPage = App.ServiceProvider.GetService<TasksPage>();
            MainFrame.Navigate(tasksPage);
        }

        private void RadioButton_Click_2(object sender, RoutedEventArgs e)
        {
            var pomodoroPage = App.ServiceProvider.GetService<PomodoroPage>();
            MainFrame.Navigate(pomodoroPage);
        }
    }
}