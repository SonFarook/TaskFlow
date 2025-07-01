using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
        public ICommand CheckBoxChecked { get; set; }
        public ICommand CheckBoxUnChecked { get; set; }
        long newID;
        public TasksViewModel() 
        {
            _tasks = new ObservableCollection<TaskModel>();
            GetTasksFromDB();

            DeleteTaskButtonClicked = new RelayCommand<TaskModel>(DeleteTaskButtonClicked_Executed);
            CheckBoxChecked = new RelayCommand<TaskModel>(CheckBoxChecked_Executed);
            CheckBoxUnChecked = new RelayCommand<TaskModel>(CheckBoxUnChecked_Executed);
            
            WeakReferenceMessenger.Default.Register<TaskCreatedMessage>(this,(r,m) =>
            {
                //Set the TaskID of the TaskModel to the Id Value in the Db
                m.Value.TaskID = InsertTaskToDBAndGetID(m.Value.Name, m.Value.Date, m.Value.Time, m.Value.Priority, 0);

                Tasks.Add(m.Value);
                Tasks = new ObservableCollection<TaskModel>(Tasks.OrderBy(task => DateTime.ParseExact(task.Date,"dd.MM.yyyy",new CultureInfo("de-DE"))));
            });
        }

        private void DeleteTaskButtonClicked_Executed(TaskModel task)
        {
            if (task != null)
            {
                Tasks.Remove(task);
                DeleteTaskFromDB(task.TaskID);
            }
        }
        private void CheckBoxChecked_Executed(TaskModel task)
        {
            if (task != null) 
            {
                UpdateIsCompletedInDB(true, task.TaskID);
            }
        }
        private void CheckBoxUnChecked_Executed(TaskModel task)
        {
            if (task != null)
            {
                UpdateIsCompletedInDB(false, task.TaskID);
            }
        }
        private int InsertTaskToDBAndGetID(string taskName, string taskDate, string taskTime, string taskPriority, int isCompleted)
        {
            using (var connection = new SqliteConnection("Data Source=app.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO tasks(Name, Date, Time, Priority, IsCompleted)
                                        VALUES (@name, @date, @time, @priority, @isCompleted);
                                        SELECT last_insert_rowid();
                                        ";
                command.Parameters.AddWithValue("@name", taskName);
                command.Parameters.AddWithValue("@date", taskDate);
                command.Parameters.AddWithValue("@time", taskTime);
                command.Parameters.AddWithValue("@priority", taskPriority);
                command.Parameters.AddWithValue("@isCompleted", isCompleted);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        private void DeleteTaskFromDB(int taskID)
        {
            using (var connection = new SqliteConnection("Data Source=app.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"DELETE FROM tasks 
                                        WHERE Id = @id;
                                        ";
                command.Parameters.AddWithValue("@id", taskID);
                command.ExecuteNonQuery();
            }
        }
        private void GetTasksFromDB()
        {
            using (var connection = new SqliteConnection("Data Source=app.db")) 
            {
                connection.Open();

                //Order the tasks by date
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM tasks
                                        ORDER BY 
                                        substr(Date, 7, 4) || '-' || substr(Date, 4, 2) || '-' || substr(Date, 1, 2) ASC,
                                        Time ASC;
                                        ";

                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read()) 
                {
                    bool convertedIsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                    var task = new TaskModel(reader["Name"].ToString(), reader["Date"].ToString(), reader["Time"].ToString(), reader["Priority"].ToString(), convertedIsCompleted);
                    task.TaskID = Convert.ToInt32(reader["Id"]);
                    Tasks.Add(task);
                }
            }
        }
        private void UpdateIsCompletedInDB(bool isCompleted, int taskID)
        {
            using (var connection = new SqliteConnection("Data Source=app.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"UPDATE tasks SET IsCompleted = @isCompleted WHERE Id = @taskID";
                command.Parameters.AddWithValue("@isCompleted", isCompleted);
                command.Parameters.AddWithValue("@taskID", taskID);
                command.ExecuteNonQuery();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
