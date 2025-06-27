using System.Windows.Controls;
using TaskFlow.ViewModel;

namespace TaskFlow.View
{
    /// <summary>
    /// Interaktionslogik für TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        public TasksPage(TasksViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
