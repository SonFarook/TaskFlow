using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaskFlow.Commands;
using TaskFlow.Model;

namespace TaskFlow.ViewModel
{
    public class TasksViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<TaskModel> _tasks;
        public ObservableCollection<TaskModel> Tasks 
        {
            get => _tasks;
            set
            {
                if (_tasks != value)
                {
                    _tasks = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand DeleteTaskButtonClicked {  get; set; }
        public TasksViewModel() 
        {
            _tasks = new ObservableCollection<TaskModel>();

            DeleteTaskButtonClicked = new RelayCommand<TaskModel>(DeleteTaskButtonClicked_Executed);
            
            WeakReferenceMessenger.Default.Register<TaskCreatedMessage>(this,(r,m) =>
            {
                Tasks.Add(m.Value);
            });
        }

        private void DeleteTaskButtonClicked_Executed(TaskModel task)
        {
            if (task != null)
            {
                Tasks.Remove(task);
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
