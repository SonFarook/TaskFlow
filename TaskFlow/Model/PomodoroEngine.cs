using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace TaskFlow.Model
{
    public class PomodoroEngine
    {
        public TimeSpan CurrentSessionDuration { get; private set; } = TimeSpan.FromMinutes(25);
        public SessionState CurrentState { get; private set; } = SessionState.Working;
        public int PomoCounter { get; set; }
        public void StartNextSession()
        {
            if (CurrentState == SessionState.Working)
            {
                PomoCounter++;
                System.Diagnostics.Debug.WriteLine(PomoCounter);
                CurrentState = PomoCounter % 4 == 0 ? SessionState.LongBreak : SessionState.ShortBreak;
            }
            else
                CurrentState = SessionState.Working;

            switch (CurrentState)
            {
                case SessionState.Working:
                    CurrentSessionDuration = TimeSpan.FromMinutes(25);
                    break;

                case SessionState.ShortBreak:
                    CurrentSessionDuration = TimeSpan.FromMinutes(5);
                    break;

                case SessionState.LongBreak:
                    CurrentSessionDuration = TimeSpan.FromMinutes(15);
                    break;

                default:
                    CurrentSessionDuration = TimeSpan.FromMinutes(25);
                    break;
            }

            System.Diagnostics.Debug.WriteLine(CurrentSessionDuration);

        }
        public enum SessionState { Working, ShortBreak, LongBreak }
    }
}
