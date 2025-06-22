using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TaskFlow.Commands;
using System.Globalization;

namespace TaskFlow.ViewModel
{
    public class CreateTaskViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //The date from datepicker
        private string _selectedDate;
        public string SelectedDate 
        {
            get => _selectedDate;
            set 
            {
                if (_selectedDate != value) 
                {
                    _selectedDate = value;
                    DateTime dt = DateTime.Parse(_selectedDate);
                    _selectedDate = dt.ToString("dd.MM.yyyy");
                    OnPropertyChanged();
                }
            }
        }

        public CreateTaskViewModel()
        {
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
