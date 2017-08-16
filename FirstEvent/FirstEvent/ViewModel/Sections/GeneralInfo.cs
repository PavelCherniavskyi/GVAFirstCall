﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FirstEvent.Model;
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
        private Visibility _docTextBoxVisibility;


        public GeneralInfo()
        {
            Messenger.Default.Register(this, new Action<Doctor>(DoctorFieldMessage));
            EventDateTime = DocDateTime = DateTime.Now;
            _dutyDoctor = new Doctor();
            DutyOPS = "Pavel Cherniavskyi";
            DocTextBoxVisibility = Visibility.Hidden;
            DocInSearchList = new ObservableCollection<Doctor>();
        }

        public ObservableCollection<Doctor> DocInSearchList { get; set; }


        public Visibility DocTextBoxVisibility
        {
            get { return _docTextBoxVisibility; }
            set { _docTextBoxVisibility = value; RaisePropertyChanged("DocTextBoxVisibility"); }
        }

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
                return _showDocListWindowCommand ?? 
                    (_showDocListWindowCommand = new RelayCommand(() => Messenger.Default.Send<string>("doctorsListShow"), () => true));
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

        private void DoctorFieldMessage(Doctor doc)
        {
            if (doc != null)
            {
                DutyDoctor = doc;
                IsDutyDocReadOnly = true;
            }
            Messenger.Default.Send<string>("doctorsListHide");

        }

        private void EnterKeyCommandExecute()
        {
            int tempId;
            if (int.TryParse(DutyDoctor.FullName, out tempId))
            {
                foreach (var d in DataBaseManager.Doctors)
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

                foreach (var d in DataBaseManager.Doctors)
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
            DocTextBoxVisibility = Visibility.Visible;
            DocInSearchList.Add(new Doctor() {FullName = "asdfa"});
            DocInSearchList.Add(new Doctor() { FullName = "fghdfg" });
            DocInSearchList.Add(new Doctor() { FullName = "werwe" });
            DutyDoctor = new Doctor();
            IsDutyDocReadOnly = false;
        }
    }
}
