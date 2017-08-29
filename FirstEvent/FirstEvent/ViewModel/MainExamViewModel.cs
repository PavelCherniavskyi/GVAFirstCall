using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using FirstEvent.Model;
using FirstEvent.ViewModel.Sections;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel
{
    public class MainExamViewModel : ViewModelBase
    {
        private bool _saveButttonEnabled = true;

        public Caller Caller { get; set; }

        public GeneralInfo GeneralInfo { get; set; }

        public Membership Membership { get; set; }

        public Subscriber Subscriber { get; set; }

        public CallInfo CallInfo { get; set; }

        public TreatDoctor TreatDoctor { get; set; }

        public ContactSection ContactSection { get; set; }

        public bool SaveButttonEnabled
        {
            get { return _saveButttonEnabled; }
            set
            {
                _saveButttonEnabled = value;
                RaisePropertyChanged("SaveButttonEnabled");
            }
        }

        public MainExamViewModel(string dutyOps)
        {
            Caller = new Caller();
            GeneralInfo = new GeneralInfo {DutyOps = dutyOps};
            Membership = new Membership();
            Subscriber = new Subscriber();
            CallInfo = new CallInfo();
            TreatDoctor = new TreatDoctor();
            ContactSection = new ContactSection();
        }


        public ICommand OkButtonCommand
        {
            get
            {
                return new RelayCommand(OkButtonCommandExecute, () => true);
            }
        }

        private void OkButtonCommandExecute()
        {
            if (SaveButttonEnabled)
            {
                SaveButtonCommandExecute();
            }

            MessageBox.Show("Your results has been saved successfully.\nPlease ask Senior operator to check it.");
            Application.Current.Shutdown();
        }

        public ICommand CancelButtonCommand
        {
            get
            {
                return new RelayCommand(CancelButtonCommandExecute, () => true);
            }
        }

        private void CancelButtonCommandExecute()
        {
            if (SaveButttonEnabled)
            {
                MessageBox.Show("Your results wasn't saved.");
            }
            
            Application.Current.Shutdown();
        }

        public ICommand SaveButtonCommand
        {
            get
            {
                return new RelayCommand(SaveButtonCommandExecute, () => true);
            }
        }

        private void SaveButtonCommandExecute()
        {
            var firstCall = new FirstCall();

            firstCall.DutyOperator = GeneralInfo.DutyOps;
            firstCall.DutyDoctorId = GeneralInfo.DutyDoctor.Id;
            firstCall.DocDateTime = GeneralInfo.DocDateTime.ToString(CultureInfo.CurrentCulture);
            firstCall.EventDateTime = GeneralInfo.EventDateTime.ToString(CultureInfo.CurrentCulture);

            firstCall.CalFirstName = Caller.FirstName;
            firstCall.CalLastName = Caller.LastName;
            firstCall.CalMiddleName = Caller.MiddleName;
            firstCall.CalLocationInfo = Caller.LocationInfo;
            firstCall.PlaceOfStay = Caller.PlaceOfStay;
            firstCall.CalRoom = Caller.Room;
            firstCall.CallerId = Caller.CallerId;
            firstCall.CalRelToSubId = Caller.RelToSub.Id;
            firstCall.CalCountryId = Caller.Country.Id;
            firstCall.CalRegionId = Caller.Region.Id;
            firstCall.CalCityId = Caller.City.Id;

            firstCall.InsuranceId = Membership.Insurance.Id;
            firstCall.PolicyNum = Membership.PolicyNum;
            firstCall.LetterCode = Membership.LetterCode;
            firstCall.InsuredDays = Membership.InsuredDays;
            firstCall.InsuredProgramm = Membership.InsuredProgramm;
            firstCall.Deductable = Membership.Deductable;
            firstCall.ValidFrom = Membership.ValidFrom.ToString(CultureInfo.CurrentCulture);
            firstCall.ValidTo = Membership.ValidTo.ToString(CultureInfo.CurrentCulture);

            firstCall.HomeAdress = Subscriber.HomeAdress;
            firstCall.SubFirstName = Subscriber.FirstName;
            firstCall.SubLastName = Subscriber.LastName;
            firstCall.SubMiddleName = Subscriber.MiddleName;
            firstCall.SubLocationInfo = Subscriber.LocationInfo;
            firstCall.SubRoom = Subscriber.Room;
            firstCall.Age = Subscriber.Age;
            firstCall.DoB = Subscriber.DoB.ToString(CultureInfo.CurrentCulture);
            firstCall.Arrival = Subscriber.Arrival.ToString(CultureInfo.CurrentCulture);
            firstCall.Departure = Subscriber.Departure.ToString(CultureInfo.CurrentCulture);
            firstCall.HotelId = Subscriber.Hotel.Id;
            firstCall.SubCountryId = Subscriber.Country.Id;
            firstCall.SubRegionId = Subscriber.Region.Id;
            firstCall.SubCityId = Subscriber.City.Id;

            firstCall.StatusOfCallId = CallInfo.StatusSelected.Id;
            firstCall.ReasonForCall = CallInfo.ReasonForCall;
            firstCall.IsChronical = CallInfo.IsChronical ? 1 : 0;
            firstCall.IsAlcohol = CallInfo.IsAlcohol ? 1 : 0;

            firstCall.DocLocationInfo = TreatDoctor.LocationInfo;
            firstCall.IsDoctor = TreatDoctor.IsDoctor ? 1 : 0;
            firstCall.IsFacility = TreatDoctor.IsFacility ? 1 : 0;
            firstCall.DocCountryId = TreatDoctor.Country.Id;
            firstCall.DocRegionId = TreatDoctor.Region.Id;
            firstCall.DocCityId = TreatDoctor.City.Id;
            firstCall.TreatingDoctorId = TreatDoctor.TreatingDoctorView.Id;

            DataBaseManager.AddFirstCall(firstCall);

            var lastFirstCall = DataBaseManager.SearchfFirstCall(firstCall);
            var listOfConacts = new List<Contact> ();
            foreach (var c in ContactSection.ContactViewInForm)
            {
                var contact = new Contact()
                {
                    FirstCallId = lastFirstCall.Id,
                    RelToSubId = c.SelectedRelToSub.Id,
                    TypeOfContactId = c.SelectedContact.Id,
                    Name = c.Name,
                    ContactNum = c.ContactNum,
                    Info = c.Info
                };

                listOfConacts.Add(contact);
            }

            DataBaseManager.AddContacts(listOfConacts);
            SaveButttonEnabled = false;

        }


    }
}
