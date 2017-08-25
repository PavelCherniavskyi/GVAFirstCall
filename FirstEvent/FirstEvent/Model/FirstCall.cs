using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public class FirstCall
    {
        public FirstCall()
        {
            ContactList = new ObservableCollection<Contact>();
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
