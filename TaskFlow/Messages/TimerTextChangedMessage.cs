using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TaskFlow.Messages
{
    public class TimerTextChangedMessage : ValueChangedMessage<string>
    {
        public TimerTextChangedMessage(string value) : base(value) { }
    }
}
