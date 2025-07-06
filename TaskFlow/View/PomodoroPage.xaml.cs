using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using TaskFlow.ViewModel;

namespace TaskFlow.View
{
    /// <summary>
    /// Interaktionslogik für PomodoroPage.xaml
    /// </summary>
    public partial class PomodoroPage : Page
    {
        public PomodoroPage(PomodoroViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;

        }
    }
}
