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

namespace TaskFlow.ViewModel
{
    public class PomodoroViewModel : INotifyPropertyChanged
    {
        TimeSpan CurrentSessionDuration;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ToggleTimerCommand { get; set; }
        public ICommand SkipTimerCommand { get; set; }
        private readonly PomodoroTimer _timer;
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
            _timer = new PomodoroTimer();
            SetDefaultValues();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TimeSpan remainingTime = _timer.GetRemainingTime();

            TimerText = remainingTime.ToString(@"mm\:ss");

            UpdateProgress(CurrentSessionDuration);

            if (remainingTime.TotalSeconds <= 0)
            {
                _timer.IsBreakTime = !_timer.IsBreakTime;

                if (!_timer.IsBreakTime)
                {
                    _timer.PomoCounter++; //only raise the counter, if a pomodoro (25min) timer ends
                    TimerText = "25:00";
                    CurrentSessionDuration = _timer.PomoTime;
                }

                else
                    SetBreakValues();
                
                ResetTimer();
            }
        }

        private void ToggleTimerCommand_Executed() 
        {
            if (_timer.IsRunning) 
            {
                _timer.Stop();
                ToggleTimerBtnText = "Start";
            }

            else
            {
                ShowSkipTimerBtn = true;
                if (!_timer.GotClickedBefore)
                    // if the button got clicked the first time, add one second (because the timer is dropping 1 second instantly)
                    _timer.TimerStartTime = DateTime.Now + TimeSpan.FromSeconds(1) - _timer.ElapsedTime;
                
                else
                    _timer.TimerStartTime = DateTime.Now - _timer.ElapsedTime;
                
                _timer.Start();
                ToggleTimerBtnText = "Stop";
            }

            _timer.IsRunning = !_timer.IsRunning;
            _timer.GotClickedBefore = true;
        }
        private void SkipTimerCommand_Executed()
        {
            ResetTimer();
            ArcPoint = new Point(110, 10);
            if (_timer.IsBreakTime)
            {
                TimerText = "25:00";
                CurrentSessionDuration = _timer.PomoTime;
            }

            else
            {
                _timer.PomoCounter++;
                SetBreakValues();
            }

            _timer.IsBreakTime = !_timer.IsBreakTime;
        }

        private void ResetTimer()
        {
            ToggleTimerBtnText = "Start";
            _timer.Stop();
            _timer.GotClickedBefore = false;
            _timer.IsRunning = false;
            _timer.ElapsedTime = TimeSpan.FromSeconds(0);
            ShowSkipTimerBtn = false;
        }

        private void SetDefaultValues()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += Timer_Tick;
            ToggleTimerBtnText = "Start";
            TimerText = "25:00";
            ArcPoint = new Point(110, 10);
            CurrentSessionDuration = _timer.PomoTime;
        }
        private void SetBreakValues()
        {
            if (_timer.PomoCounter % 4 == 0)
            {
                TimerText = "15:00";
                CurrentSessionDuration = _timer.LongBreakTime;
            }

            else
            {
                TimerText = "05:00";
                CurrentSessionDuration = _timer.ShortBreakTime;
            }
        }

        private void UpdateProgress(TimeSpan currentTimer)
        {
            double progress = _timer.ElapsedTime.TotalSeconds / currentTimer.TotalSeconds;

            double angle = progress * 360;

            if (progress <= 0)
            {
                ArcPoint = new Point(110,10);
                IsLargeArc = false;
                return;
            }

            if (progress >= 1.0)
            {
                angle = 359.99;
            }

            Point center = new Point(110, 110);
            double radius = 100;

            double angleRad = (Math.PI / 180.0) * (angle - 90);

            double x = center.X + radius * Math.Cos(angleRad);
            double y = center.Y + radius * Math.Sin(angleRad);

            ArcPoint = new Point(x, y);
            IsLargeArc = angle > 180;
        } 

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
