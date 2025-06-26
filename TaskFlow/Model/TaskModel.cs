using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Model
{
    public class TaskModel
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Priority {  get; set; }
        public bool IsCompleted { get; set; } = false;

        public TaskModel(string name, string date, string time, string priority)
        {
            Name = name;
            Date = date;
            Time = time;
            Priority = priority;
        }
    }
}
