using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FirstEvent.Model;
using FirstEvent.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.Sections
{
    public class GeneralInfo : ViewModelBase
    {
        private RelayCommand _showDocListWindowCommand;
        private RelayCommand _cancelDocTextFieldCommand;
        private RelayCommand _enterKeyCommand;
        private DateTime _docDateTime;
        private DateTime _eventDateTime;
        private bool _isDutyDocReadOnly;
        private Doctor _dutyDoctor;
        private Brush _dutyDocColorTextBox;
        private string _dutyOPS;


        public GeneralInfo()
        {
            Messenger.Default.Register(this, "GeneralInfoDoctor", new Action<DocView>(DoctorFieldMessage));
            EventDateTime = DocDateTime = DateTime.Now;
            _dutyDoctor = new Doctor();
            DutyOPS = "Pavel Cherniavskyi";
            DocInSearchList = new ObservableCollection<Doctor>();
        }

        public ObservableCollection<Doctor> DocInSearchList { get; set; }


        public string DutyOPS
        {
            set { _dutyOPS = value; RaisePropertyChanged("DutyOPS"); }
            get { return _dutyOPS; }
        }

        public bool IsDutyDocReadOnly
        {
            get { return _isDutyDocReadOnly; }
            set
            {
                _isDutyDocReadOnly = value;
                DutyDocColorTextBox = _isDutyDocReadOnly ? 
                    new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)) : 
                    new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                RaisePropertyChanged("IsDutyDocReadOnly");
            }
        }

        public Doctor DutyDoctor
        {
            get { return _dutyDoctor; }
            set { _dutyDoctor = value; RaisePropertyChanged("DutyDoctor"); }
        }

        public Brush DutyDocColorTextBox
        {
            get { return _dutyDocColorTextBox;}
            set { _dutyDocColorTextBox = value; RaisePropertyChanged("DutyDocColorTextBox"); }
        }

        public DateTime DocDateTime
        {
            get { return _docDateTime; }
            set
            {
                _docDateTime = value;
                RaisePropertyChanged("DocDateTime");
            }
        }

        public DateTime EventDateTime
        {
            get { return _eventDateTime; }
            set
            {
                _eventDateTime = value;
                RaisePropertyChanged("EventDateTime");
            }
        }

        public ICommand ShowDocListWindow
        {
            get
            {
                return new RelayCommand(() => new DoctorsList().ShowDialog(), () => true);
            }
        }

        public ICommand EnterKeyCommand
        {
            get
            {
                return _enterKeyCommand ?? 
                    (_enterKeyCommand = new RelayCommand(EnterKeyCommandExecute, () => true));
            }
        }

        public ICommand CancelDocTextField
        {
            get
            {
                return _cancelDocTextFieldCommand ?? 
                    (_cancelDocTextFieldCommand = new RelayCommand(ClearDocFeildExecute, () => true));
            }
        }

        private void DoctorFieldMessage(DocView doc)
        {
            if (doc == null)
                return;
            var query = from d in DataBaseManager.AllDoctors
                where doc.Id == d.Id
                select d;
            DutyDoctor = query.First();
            IsDutyDocReadOnly = true;
        }

        private void EnterKeyCommandExecute()
        {
            int tempId;
            if (int.TryParse(DutyDoctor.FullName, out tempId))
            {
                foreach (var d in DataBaseManager.AllDoctors)
                {
                    if (tempId != d.Id)
                        continue;
                    DutyDoctor = d;
                    IsDutyDocReadOnly = true;
                    break;
                }
            }
            else
            {
                var strToSrch = DutyDoctor.FullName.ToUpper();

                foreach (var d in DataBaseManager.AllDoctors)
                {
                    if (!d.FullName.ToUpper().Contains(strToSrch))
                        continue;
                    DutyDoctor = d;
                    IsDutyDocReadOnly = true;
                }
            }
            
        }

        private void ClearDocFeildExecute()
        {
            DutyDoctor = new Doctor();
            IsDutyDocReadOnly = false;
        }
    }
}
