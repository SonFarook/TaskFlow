namespace TaskFlow.Model
{
    public class TaskModel
    {
        public int TaskID { get; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Priority {  get; set; }
        public bool IsCompleted { get; set; } = false;
        private int _taskCount = 0;
        public TaskModel(string name, string date, string time, string priority)
        {
            Name = name;
            Date = date;
            Time = time;
            Priority = priority;

            //Auto Increment Logic
            TaskID = _taskCount;

            _taskCount++;
        }
    }
}
