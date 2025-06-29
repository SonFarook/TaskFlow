using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaskFlow.Commands;
using CommunityToolkit.Mvvm.Messaging;
using TaskFlow.Model;
using System.Collections.ObjectModel;

namespace TaskFlow.ViewModel
{
    public class CreateTaskViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _enteredName;
        public string EnteredName 
        {
            get => _enteredName;
            set
            {
                if(_enteredName != value)
                {
                    _enteredName = value;
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
        public ObservableCollection<string> Priorities { get; } = new()
        {
            "Low", "Mid", "High"
        };
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
                if(_selectedDate != value)
                {
                    _selectedDate = value;
                    if (DateTime.TryParse(_selectedDate, out var dt))
                        _selectedDate = dt.ToString("dd.MM.yyyy");
                    else
                        _selectedDate = string.Empty;

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
        private string _validationSuccessMsg;
        public string ValidationSuccessMsg 
        {
            get => _validationSuccessMsg;
            set
            {
                if(_validationSuccessMsg != value)
                {
                    _validationSuccessMsg = value;
                    OnPropertyChanged();
                }

            }
        }

        public ICommand SubmitButtonClicked { get; set; }
        public CreateTaskViewModel()
        {
            SubmitButtonClicked = new RelayCommand(SubmitButtonClicked_Executed);
        }
        private string Validate(string value, string errorMessage) => string.IsNullOrWhiteSpace(value) ? errorMessage : string.Empty;
        private void ValidateDateTime()
        {
            if (!string.IsNullOrEmpty(EnteredTime))
                TimeErrorMsg = TimeSpan.TryParse(EnteredTime, out _) ? string.Empty : "Please enter a valid time.";

            if (!string.IsNullOrEmpty(SelectedDate))
                DateErrorMsg = DateTime.TryParseExact(SelectedDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _) ? string.Empty : "Please enter a valid date.";
 
            if (string.IsNullOrEmpty(TimeErrorMsg) && string.IsNullOrEmpty(DateErrorMsg))
            {
                if(DateTime.TryParseExact($"{SelectedDate} {EnteredTime}", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedDateTime))
                {
                    if (selectedDateTime.Date < DateTime.Now.Date)
                        DateErrorMsg = "Selected date is in the past.";

                    else if (selectedDateTime < DateTime.Now)
                        TimeErrorMsg = "Entered time is in the past.";
                }
            }
        }
        private void ValidateInputs()
        {
            NameErrorMsg = Validate(EnteredName, "Please enter a name.");
            PriorityErrorMsg = Validate(SelectedPriority?.ToString(), "Please select a priority");
            DateErrorMsg = Validate(SelectedDate, "Please enter a date.");
            TimeErrorMsg = Validate(EnteredTime, "Please enter a time.");

            ValidateDateTime();
        }
        private bool IsValid()
        {
            if (new string[] {NameErrorMsg, PriorityErrorMsg, DateErrorMsg, TimeErrorMsg}.All(string.IsNullOrEmpty))
                return true;
            
            else
                return false;
        }
        private void ClearInputs()
        {
            EnteredName = 
            EnteredTime = 
            SelectedDate = string.Empty;
            SelectedPriority = null;
        }
        private void CreateTask()
        {
            var task = new TaskModel(EnteredName, SelectedDate, EnteredTime, SelectedPriority.ToString(), false);
            
            WeakReferenceMessenger.Default.Send(new TaskCreatedMessage(task));
        }
        private void SubmitButtonClicked_Executed()
        {
            ValidateInputs();

            if(IsValid())
            {
                CreateTask();

                ClearInputs();
                ValidationSuccessMsg = "Task created successfully.";
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
