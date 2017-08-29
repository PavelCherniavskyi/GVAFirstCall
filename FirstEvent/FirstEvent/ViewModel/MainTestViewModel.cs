using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FirstEvent.Model;
using FirstEvent.ViewModel.Sections;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FirstEvent.ViewModel
{
    public class MainTestViewModel : ViewModelBase
    {
        private bool _saveButttonEnabled = true;

        public GeneralInfo GeneralInfo { get; set; }

        public Caller Caller { get; set; }

        public Membership Membership { get; set; }

        public Subscriber Subscriber { get; set; }

        public CallInfo CallInfo { get; set; }

        public TreatDoctor TreatDoctor { get; set; }

        public ContactSection ContactSection { get; set; }

        public bool SaveButttonEnabled
        {
            get { return _saveButttonEnabled; }
            set {
                _saveButttonEnabled = value;
                RaisePropertyChanged("SaveButttonEnabled");
            }
        }

        public MainTestViewModel(string dutyOps)
        {
            Caller = new Caller();
            GeneralInfo = new GeneralInfo { DutyOps = dutyOps };
            Membership = new Membership();
            Subscriber = new Subscriber();
            CallInfo = new CallInfo();
            TreatDoctor = new TreatDoctor();
            ContactSection = new ContactSection();
        }


        public ICommand OkButtonCommand
        {
            get { return new RelayCommand(OkButtonCommandExecute, () => true); }
        }

        private void OkButtonCommandExecute()
        {
            if (SaveButttonEnabled)
                SaveButtonCommandExecute();
            Application.Current.Shutdown();
        }

        public ICommand CancelButtonCommand
        {
            get { return new RelayCommand(CancelButtonCommandExecute, () => true); }
        }

        private void CancelButtonCommandExecute()
        {
            if (SaveButttonEnabled)
                MessageBox.Show("Your results wasn't saved.");

            Application.Current.Shutdown();
        }

        public ICommand SaveButtonCommand
        {
            get{ return new RelayCommand(SaveButtonCommandExecute, () => true); }
        }

        private void SaveButtonCommandExecute()
        {

            MessageBox.Show("Everything looks right.\nWell done!!!");
            SaveButttonEnabled = false;
        }
    }
}
