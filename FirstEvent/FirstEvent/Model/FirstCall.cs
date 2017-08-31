using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstEvent.ViewModel;
using FirstEvent.ViewModel.Sections;

namespace FirstEvent.Model
{
    public class FirstCall
    {
        public FirstCall()
        {
            ContactList = new ObservableCollection<Contact>();
        }

        public MainAdminViewModel BuildAdminViewModel()
        {
            GeneralInfo generalInfo = new GeneralInfo();
            generalInfo.DutyOps = DutyOperator;
            generalInfo.DutyDoctor = DataBaseManager.GetDoctorById(DutyDoctorId);
            generalInfo.DocDateTime = DateTime.Parse(DocDateTime);
            generalInfo.EventDateTime = DateTime.Parse(EventDateTime);

            Caller caller = new Caller();
            caller.FirstName = CalFirstName;
            caller.LastName = CalLastName;
            caller.MiddleName = CalMiddleName;
            caller.LocationInfo = CalLocationInfo;
            caller.PlaceOfStay = PlaceOfStay;
            caller.Room = CalRoom;
            caller.CallerId = CallerId;
            caller.RelToSub = DataBaseManager.GetRelToSubById(CalRelToSubId);
            caller.Country = DataBaseManager.GetCountryById(CalCountryId);
            caller.Region = DataBaseManager.GetRegionById(CalRegionId);
            caller.City = DataBaseManager.GetCityById(CalCityId);

            Membership membership = new Membership();
            membership.Insurance = DataBaseManager.GetInsuranceViewById(InsuranceId);
            membership.PolicyNum = PolicyNum;
            membership.LetterCode = LetterCode;
            membership.InsuredDays = InsuredDays;
            membership.InsuredProgramm = InsuredProgramm;
            membership.Deductable = Deductable;
            membership.ValidFrom = DateTime.Parse(ValidFrom);
            membership.ValidTo = DateTime.Parse(ValidTo);

            Subscriber subscriber = new Subscriber();
            subscriber.HomeAdress = HomeAdress;
            subscriber.FirstName = SubFirstName;
            subscriber.LastName = SubLastName;
            subscriber.MiddleName = SubMiddleName;
            subscriber.LocationInfo = SubLocationInfo;
            subscriber.Room = SubRoom;
            subscriber.Age = Age;
            subscriber.DoB = DateTime.Parse(DoB);
            subscriber.Arrival = DateTime.Parse(Arrival);
            subscriber.Departure = DateTime.Parse(Departure);
            subscriber.Hotel = DataBaseManager.GetHotelViewById(HotelId);
            subscriber.Country = DataBaseManager.GetCountryById(SubCountryId);
            subscriber.Region = DataBaseManager.GetRegionById(SubRegionId);
            subscriber.City = DataBaseManager.GetCityById(SubCityId);

            CallInfo callInfo = new CallInfo();
            callInfo.StatusSelected = DataBaseManager.GetStatusOfCallById(StatusOfCallId);
            callInfo.ReasonForCall = ReasonForCall;
            callInfo.IsChronical = IsChronical == 1;
            callInfo.IsAlcohol = IsAlcohol == 1;

            TreatDoctor treatDoctor = new TreatDoctor();
            treatDoctor.LocationInfo = DocLocationInfo;
            treatDoctor.IsDoctor = IsDoctor == 1;
            treatDoctor.IsFacility = IsFacility == 1;
            treatDoctor.TreatingDoctorView = DataBaseManager.GetTreatDocById(TreatingDoctorId);
            treatDoctor.Country = DataBaseManager.GetCountryById(DocCountryId);
            treatDoctor.Region = DataBaseManager.GetRegionById(DocRegionId);
            treatDoctor.City = DataBaseManager.GetCityById(DocCityId);

            ContactSection contactSection = new ContactSection();
            ObservableCollection<ContactViewInForm> contactView = new ObservableCollection<ContactViewInForm>();
            foreach (var c in ContactList)
            {
                contactView.Add(new ContactViewInForm()
                {
                    ContactNum = c.ContactNum,
                    Info = c.Info,
                    Name = c.Name,
                    SelectedContact = DataBaseManager.GetTypeOfContactById(c.TypeOfContactId),
                    SelectedRelToSub = DataBaseManager.GetRelToSubById(c.RelToSubId)
                });
            }
            contactSection.ContactViewInForm = contactView;

            return new MainAdminViewModel(caller, generalInfo, membership, subscriber, callInfo, treatDoctor, contactSection);

        }

        public ObservableCollection<Contact> ContactList { get; set; }

        public int Id { get; set; }

        #region General Info

        public string DutyOperator { get; set; }

        public int? DutyDoctorId { get; set; }

        public string DocDateTime { get; set; }

        public string EventDateTime { get; set; }

        #endregion

        #region Caller

        public string CalFirstName { get; set; }

        public string CalLastName { get; set; }

        public string CalMiddleName { get; set; }

        public string CalLocationInfo { get; set; }

        public string PlaceOfStay { get; set; }

        public string CalRoom { get; set; }

        public string CallerId { get; set; }

        public int? CalRelToSubId { get; set; }

        public int? CalCountryId { get; set; }

        public int? CalRegionId { get; set; }

        public int? CalCityId { get; set; }

        #endregion

        #region Membership

        public int? InsuranceId { get; set; }

        public string PolicyNum { set; get; }

        public string LetterCode { set; get; }

        public string InsuredDays { set; get; }

        public string InsuredProgramm { set; get; }

        public string Deductable { set; get; }

        public string ValidFrom { set; get; }

        public string ValidTo { set; get; }

        #endregion

        #region Subscriber

        public string HomeAdress { set; get; }

        public string SubFirstName { get; set; }

        public string SubLastName { get; set; }

        public string SubMiddleName { get; set; }

        public string SubLocationInfo { get; set; }

        public string SubRoom { get; set; }

        public string Age { get; set; }

        public string DoB { get; set; }

        public string Arrival { get; set; }

        public string Departure { get; set; }

        public int? HotelId { get; set; }

        public int? SubCountryId { get; set; }

        public int? SubRegionId { get; set; }

        public int? SubCityId { get; set; }

        #endregion

        #region Call Info

        public int? StatusOfCallId { get; set; }

        public string ReasonForCall { get; set; }

        public int? IsChronical { get; set; }

        public int? IsAlcohol { get; set; }

        #endregion

        #region Treating Doctor

        public string DocLocationInfo { get; set; }

        public int? IsDoctor { get; set; }

        public int? IsFacility { get; set; }

        public int? DocCountryId { get; set; }

        public int? DocRegionId { get; set; }

        public int? DocCityId { get; set; }

        public int? TreatingDoctorId { get; set; }

        #endregion

    }
}
