using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TaskFlow.Model;
using System.Windows;
using System.Diagnostics;

namespace TaskFlow.ViewModel
{
    public class PomodoroViewModel : INotifyPropertyChanged
    {
        // Constants defining the geometry of the circular progress bar.
        private const double ArcCenterX = 110;
        private const double ArcCenterY = 110;
        private const double ArcRadius = 100;
        // The starting point of the arc (top-middle of the circle).
        private static readonly Point ArcStartPosition = new(ArcCenterX, ArcCenterY - ArcRadius);

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ToggleTimerCommand { get; set; }
        public ICommand SkipTimerCommand { get; set; }

        // Stores the total time passed in the session, accumulated across pauses.
        private TimeSpan _elapsedTime;
        // Stores the exact timestamp when the timer was last started or resumed.
        private DateTime _lastStartTime;
        private readonly PomodoroEngine _engine;
        private readonly DispatcherTimer _timer;

        private string _timerText;
        public string TimerText
        {
            get => _timerText;
            set
            {
                if (_timerText != value)
                {
                    _timerText = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _toggleTimerBtnText;
        public string ToggleTimerBtnText
        {
            get => _toggleTimerBtnText;
            set
            {
                if (_toggleTimerBtnText != value)
                {
                    _toggleTimerBtnText = value;
                    OnPropertyChanged();
                }
            }
        }
        private Point _arcPoint;
        public Point ArcPoint
        {
            get => _arcPoint;
            set
            {
                if (_arcPoint != value)
                {
                    _arcPoint = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isLargeArc;
        public bool IsLargeArc
        {
            get => _isLargeArc;
            set
            {
                if (_isLargeArc != value)
                {
                    _isLargeArc = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _showSkipTimerBtn;
        public bool ShowSkipTimerBtn
        {
            get => _showSkipTimerBtn;
            set
            {
                if (_showSkipTimerBtn != value)
                {
                    _showSkipTimerBtn = value;
                    OnPropertyChanged();
                }
            }
        }
        public PomodoroViewModel()
        {
            ToggleTimerCommand = new RelayCommand(ToggleTimerCommand_Executed);
            SkipTimerCommand = new RelayCommand(SkipTimerCommand_Executed);

            _engine = new PomodoroEngine();
            _timer = new DispatcherTimer
            {
                // Set the timer to tick frequently for a smooth UI update.
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _timer.Tick += Timer_Tick;

            // Set the initial state of the UI.
            SetDefaultValues();
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            // Calculate total elapsed time by adding previously accumulated time to the current running interval.
            TimeSpan totalElapsed = _elapsedTime + (DateTime.Now - _lastStartTime);
            TimeSpan remainingTime = _engine.CurrentSessionDuration - totalElapsed;

            // Update the circular progress bar based on the elapsed time.
            UpdateProgress(totalElapsed, _engine.CurrentSessionDuration);

            // Check if the current session has finished.
            if (remainingTime.TotalSeconds <= 0)
            {
                StopTimer();
                PrepareNewSession();
                return; // Exit the tick to prevent further updates in this cycle.
            }

            // Update the displayed time text.
            TimerText = remainingTime.ToString(@"mm\:ss");
        }

        private void ToggleTimerCommand_Executed()
        {
            if (!_timer.IsEnabled)
            {
                // Record the start time for the current interval.
                _lastStartTime = DateTime.Now;
                _timer.Start();
                ToggleTimerBtnText = "Stop";
                ShowSkipTimerBtn = true;
            }
            else
            {
                // Add the time of the interval that just ended to our total elapsed time.
                _elapsedTime += DateTime.Now - _lastStartTime;

                // Stop the timer and update button states.
                StopTimer();
            }
        }

        private void SkipTimerCommand_Executed()
        {
            StopTimer();
            PrepareNewSession();
        }

        private void PrepareNewSession()
        {
            // Advance the engine to the next state (e.g., from work to break).
            _engine.StartNextSession();

            // CRITICAL: Reset the elapsed time for the new session.
            _elapsedTime = TimeSpan.Zero;

            // Update the UI text with the duration of the new session.
            TimerText = _engine.CurrentSessionDuration.ToString(@"mm\:ss");

            // Reset the progress arc to its starting position.
            ArcPoint = ArcStartPosition;
            IsLargeArc = false;
        }

        private void StopTimer()
        {
            _timer.Stop();
            ShowSkipTimerBtn = false;
            ToggleTimerBtnText = "Start";
        }

        private void SetDefaultValues()
        {
            // Get the default session duration from the engine (25:00).
            TimerText = _engine.CurrentSessionDuration.ToString(@"mm\:ss");
            ToggleTimerBtnText = "Start";

            // Reset the progress arc and hide the skip button.
            ArcPoint = ArcStartPosition;
            IsLargeArc = false;
            ShowSkipTimerBtn = false;
        }

        private void UpdateProgress(TimeSpan elapsedTime, TimeSpan currentSession)
        {
            // Calculate progress as a value between 0.0 and 1.0.
            double progress = elapsedTime.TotalSeconds / currentSession.TotalSeconds;
            if (progress < 0) progress = 0;
            // Cap the progress just below 1.0 to prevent a full 360-degree arc, which might not render correctly.
            if (progress >= 1.0) progress = 0.9999;

            // Convert progress percentage to an angle in degrees.
            double angle = progress * 360;

            // Convert angle to radians and calculate the X, Y coordinates on the circle.
            Point center = new Point(ArcCenterX, ArcCenterY);
            double angleRad = (Math.PI / 180.0) * (angle - 90); // -90 degrees to start from the top.

            double x = center.X + ArcRadius * Math.Cos(angleRad);
            double y = center.Y + ArcRadius * Math.Sin(angleRad);

            // Update the UI properties.
            ArcPoint = new Point(x, y);
            IsLargeArc = angle > 180;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
