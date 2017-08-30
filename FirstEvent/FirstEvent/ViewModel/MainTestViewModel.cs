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

            if (!SaveButttonEnabled)
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
            if (CheckMembership() && CheckSubscriber() && CheckResonForCall() && CheckCheckContacts())
            {
                MessageBox.Show("Everything looks right.\nWell done!!!");
                SaveButttonEnabled = false;
            }
            
        }

        private bool CheckMembership()
        {
            if (Membership.Insurance.Name == string.Empty)
            {
                MessageBox.Show("How do you think we will validate policy \nif we don't know name of an insurance company?");
                return false;
            }
            if (Membership.ValidFrom == DateTime.MinValue || Membership.ValidTo == DateTime.MinValue)
            {
                MessageBox.Show("Forgot to put insurance valid terms?");
                return false;
            }
            if (Membership.FindSubByPolInfoPressed == false)
            {
                MessageBox.Show("Forgot to press \"Find Subsriber by Policy info\" button?");
                return false;
            }
            if (Membership.PolicyNum == string.Empty)
            {
                MessageBox.Show("How about policy number? \nIt will be very useful for you if you will not find subsciber in database.");
                return false;
            }
            if (int.Parse(Membership.InsuredDays) <= 0)
            {
                MessageBox.Show("Don't you think count of insured days looks suspicious?");
                return false;
            }
            return true;
        }

        private bool CheckSubscriber()
        {
            return true;
        }

        private bool CheckResonForCall()
        {
            return true;
        }

        private bool CheckCheckContacts()
        {
            return true;
        }
    }
}
