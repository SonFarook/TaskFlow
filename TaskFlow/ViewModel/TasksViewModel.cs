using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public TasksViewModel() 
        {
            _tasks = new ObservableCollection<TaskModel>();
            Tasks.Add(new TaskModel("Delete Button stylen","25.06.2025","15:00","Low"));
            Tasks.Add(new TaskModel("SQLite Tutorial gucken","25.06.2025","15:00","Mid"));
            Tasks.Add(new TaskModel("GitHub pushen","25.06.2025","15:00","Low"));
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
