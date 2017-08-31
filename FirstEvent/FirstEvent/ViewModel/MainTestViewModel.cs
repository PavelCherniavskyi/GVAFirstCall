using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FirstEvent.Model;
using FirstEvent.View;
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
                new WarningWindow("Your results wasn't saved.").ShowDialog();

            Application.Current.Shutdown();
        }

        public ICommand SaveButtonCommand
        {
            get{ return new RelayCommand(SaveButtonCommandExecute, () => true); }
        }

        private void SaveButtonCommandExecute()
        {
            if (CheckMembership() && CheckSubscriber() && CheckResonForCall() && CheckContacts())
            {
                SaveButttonEnabled = false;
            }
            
        }

        private bool CheckMembership()
        {
            if (Membership.Insurance.Name == string.Empty)
            {
                new WarningWindow("How do you think we will validate policy if we don't know name of an insurance company?").ShowDialog();
                return false;
            }
            if (Membership.ValidFrom == DateTime.MinValue || Membership.ValidTo == DateTime.MinValue)
            {
                new WarningWindow("Forgot to put insurance valid terms?").ShowDialog();
                return false;
            }
            if (Membership.FindSubByPolInfoPressed == false)
            {
                new WarningWindow("Forgot to press \"Find Subsriber by Policy info\" button?").ShowDialog();
                return false;
            }
            if (Membership.PolicyNum == string.Empty)
            {
                new WarningWindow("How about policy number? \nIt will be very useful for you if you will not find subsciber in database.").ShowDialog();
                return false;
            }
            if (int.Parse(Membership.InsuredDays) <= 0)
            {
                new WarningWindow("Don't you think count of insured days looks suspicious?").ShowDialog();
                return false;
            }
            return true;
        }

        private bool CheckSubscriber()
        {
            if (Subscriber.City.Name == string.Empty || Subscriber.Region.Name == string.Empty || Subscriber.Country.Name == string.Empty)
            {
                new WarningWindow("It is good idea to indicate where our client is. Don't you think so?").ShowDialog();
                return false;
            }
            if (Subscriber.FirstName == string.Empty || Subscriber.LastName == string.Empty)
            {
                new WarningWindow("When you next time will call to this client what would you say?\n\'Hello Mr./Ms. ... ???\'").ShowDialog();
                return false;
            }
            if (Subscriber.DoB == DateTime.MinValue)
            {
                new WarningWindow("Is our client a child or a grandfather? \nHow do we know his age when organize medical help?").ShowDialog();
                return false;
            }
            return true;
        }

        private bool CheckResonForCall()
        {
            if (CallInfo.ReasonForCall == string.Empty)
            {
                new WarningWindow("Will you keep in secret what client told you about his desease?\nOr you forgot to ask why he called?").ShowDialog();
                return false;
            }
            if (CallInfo.StatusSelected.Name == string.Empty)
            {
                new WarningWindow("It is very important to set up status of call for taking right further actions.").ShowDialog();
                return false;
            }
            return true;
        }

        private bool CheckContacts()
        {
            foreach (var con in ContactSection.ContactViewInForm)
            {
                if (con.SelectedRelToSub.Name == string.Empty)
                {
                    new WarningWindow($"In contact list №{con.Id} don't forget to set up a relation to subscriber.").ShowDialog();
                    return false;
                }
                if (con.Name == string.Empty)
                {
                    new WarningWindow($"In contact list №{con.Id} don't forget to set up a name.").ShowDialog();
                    return false;
                }
                if (con.SelectedContact.Name == string.Empty)
                {
                    new WarningWindow($"In contact list №{con.Id} don't forget to set up a type of contact.").ShowDialog();
                    return false;
                }
                if (con.ContactNum == string.Empty)
                {
                    new WarningWindow($"In contact list №{con.Id} don't forget to set up a contact number.").ShowDialog();
                    return false;
                }
            }
            return true;
        }
    }
}
