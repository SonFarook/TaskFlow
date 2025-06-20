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

namespace TaskFlow.ViewModel
{
    public class CreateTaskViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _dateText;
        public string DateText 
        {
            get => _dateText;
            set
            {
                if (_dateText != value) 
                {
                    _dateText = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _selectedDate;
        public string SelectedDate 
        {
            get => _selectedDate;
            set 
            {
                if (_selectedDate != value) 
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand DatePickerSelectionChanged { get; } 
        public CreateTaskViewModel()
        {
            DatePickerSelectionChanged = new RelayCommand(DatePickerSelectionChanged_Executed);
        }

        private void DatePickerSelectionChanged_Executed()
        {
            // convert selectedDate
            // bind to selectionchanged of datepicker
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
