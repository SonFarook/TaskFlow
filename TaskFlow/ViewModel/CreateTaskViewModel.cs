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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskFlow.ViewModel
{
    public class CreateTaskViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _enteredText;
        public string EnteredText 
        {
            get => _enteredText;
            set
            {
                if(_enteredText != value)
                {
                    _enteredText = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _nameErrorMsg;
        public string NameErrorMsg 
        {
            get => _nameErrorMsg;
            set
            {
                if (_nameErrorMsg != value)
                {
                    _nameErrorMsg = value;
                    OnPropertyChanged();
                }
            }
        }

        private object _selectedPriority;
        public object SelectedPriority 
        { 
            get => _selectedPriority;
            set
            {
                if (value != _selectedPriority) 
                {
                    _selectedPriority = value;
                    OnPropertyChanged();
                }    
            }
        }

        private string _priorityErrorMsg;
        public string PriorityErrorMsg
        {
            get => _priorityErrorMsg;
            set
            {
                if (_priorityErrorMsg != value)
                {
                    _priorityErrorMsg = value;
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
                    DateTime dt = DateTime.Parse(_selectedDate);
                    _selectedDate = dt.ToString("dd.MM.yyyy");
                    OnPropertyChanged();
                }
            }
        }

        private string _dateErrorMsg;
        public string DateErrorMsg
        {
            get => _dateErrorMsg;
            set
            {
                if (_dateErrorMsg != value)
                {
                    _dateErrorMsg = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _enteredTime;
        public string EnteredTime 
        {
            get => _enteredTime;
            set
            {
                if (_enteredTime != value)
                {
                    _enteredTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _timeErrorMsg;
        public string TimeErrorMsg
        {
            get => _timeErrorMsg;
            set
            {
                if (_timeErrorMsg != value)
                {
                    _timeErrorMsg = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SubmitButtonClicked { get; set; }
        public CreateTaskViewModel()
        {
            SubmitButtonClicked = new RelayCommand(SubmitButtonClicked_Executed);
        }
        private void ValidateInputs()
        {
            NameErrorMsg = Validate(EnteredText, "Please enter a name.");
            PriorityErrorMsg = Validate(SelectedPriority?.ToString(), "Please select a priority");
            DateErrorMsg = Validate(SelectedDate, "Please enter a date.");
            TimeErrorMsg = Validate(EnteredTime, "Please enter a time.");
        }
        private string Validate(string value, string errorMessage) => string.IsNullOrWhiteSpace(value) ? errorMessage : string.Empty;
        private bool IsValid()
        {
            string temp = string.Empty;
            if (new string[] {NameErrorMsg, PriorityErrorMsg, DateErrorMsg, TimeErrorMsg}.All(string.IsNullOrEmpty))
                return true;
            
            else
                return false;
        }
        private void SubmitButtonClicked_Executed()
        {
            ValidateInputs();

            if (IsValid()) 
            {
                // Create the Task
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
