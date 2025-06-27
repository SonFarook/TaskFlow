using CommunityToolkit.Mvvm.Messaging.Messages;
using TaskFlow.Model;

public class TaskCreatedMessage : ValueChangedMessage<TaskModel>
{
    public TaskCreatedMessage(TaskModel task) : base(task) { }
}