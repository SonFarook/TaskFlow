using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskFlow.View;
using TaskFlow.Commands;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Messaging;
using TaskFlow.Messages;

namespace TaskFlow.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly bool _isInDesignMode = System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());

        private string _currentTime;
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                if (_currentTime != value) 
                {
                    _currentTime = value;
                    OnPropertyChanged();   
                }
            }
        }

        private string _currentDate;
        public string CurrentDate 
        { get => _currentDate;
            set 
            {
                if (_currentDate != value) 
                {
                    _currentDate = value;
                    OnPropertyChanged();
                }   
            }
        }
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
        private bool _isTimerRunning;
        public bool IsTimerRunning
        {
            get => _isTimerRunning;
            set
            {
                if (_isTimerRunning != value) 
                {
                    _isTimerRunning = value;
                    OnPropertyChanged();
                }
            }    
        }

        public MainViewModel()
        {
            if (!_isInDesignMode) 
            {
                InitAndStartTimer();
            }

            WeakReferenceMessenger.Default.Register<TimerTextChangedMessage>(this, (r, m) =>
            {
                TimerText = m.Value;
            });
        }

        private void InitAndStartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            CurrentTime = dateTime.ToString(@"HH\:mm");
            CurrentDate = dateTime.ToString(@"dd\.MM\.yyyy");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
