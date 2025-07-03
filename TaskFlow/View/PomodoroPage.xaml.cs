using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TaskFlow.View
{
    /// <summary>
    /// Interaktionslogik für PomodoroPage.xaml
    /// </summary>
    public partial class PomodoroPage : Page
    {
        private DispatcherTimer _timer;
        private TimeSpan _totalTime;
        private DateTime _startTime;
        public PomodoroPage()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
        }


        private void StartTimer(TimeSpan duration)
        {
            _totalTime = duration;
            _startTime = DateTime.Now;

            UpdateProgress(0);
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var elapsed = DateTime.Now - _startTime;

            if (elapsed >= _totalTime)
            {
                elapsed = _totalTime;
                _timer.Stop();
            }

            double progress = elapsed.TotalSeconds / _totalTime.TotalSeconds;
            UpdateProgress(progress);
        }

        private void UpdateProgress(double progress)
        {
            TimeSpan remainingTime = _totalTime - (_totalTime * progress);
            timerText.Text = remainingTime.ToString(@"mm\:ss");

            double angle = progress * 360;

            if (progress <= 0)
            {
                progressArc.Point = progressFigure.StartPoint;
                progressArc.IsLargeArc = false;
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

            progressArc.Point = new Point(x, y);
            progressArc.IsLargeArc = (angle > 180);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartTimer(TimeSpan.FromMinutes(5));
        }
    }
}
