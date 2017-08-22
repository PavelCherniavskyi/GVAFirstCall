using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public enum GeographyWhoIsRunning
    {
        Caller,
        Subcriber,
        Doctor
    }
    public class CallerGeographySwitcher
    {
        
        private static Country _callerCountry;
        private static Region _callerRegion;
        private static City _callerCity;

        private static Country _subscriberCountry;
        private static Region _subscriberRegion;
        private static City _subscriberCity;

        private static Country _doctorCountry;
        private static Region _doctorRegion;
        private static City _doctorCity;

        private static bool _isCallerCountrySettled;
        private static bool _isCallerRegionSettled;
        private static bool _isCallerCitySettled;

        private static bool _isSubscriberCountrySettled;
        private static bool _isSubscriberRegionSettled;
        private static bool _isSubscriberCitySettled;

        private static bool _isDoctorCountrySettled;
        private static bool _isDoctorRegionSettled;
        private static bool _isDoctorCitySettled;

        public static GeographyWhoIsRunning WhoIsRunning { set; get; }

        public static Country Country
        {
            get
            {
                switch (WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        return _callerCountry;
                    case GeographyWhoIsRunning.Subcriber:
                        return _subscriberCountry;
                    case GeographyWhoIsRunning.Doctor:
                        return _doctorCountry;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
            set
            {
                switch (WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        _isCallerCountrySettled = value != null;
                        _callerCountry = value;
                        break;
                    case GeographyWhoIsRunning.Subcriber:
                        _isSubscriberCountrySettled = value != null;
                        _subscriberCountry = value;
                        break;
                    case GeographyWhoIsRunning.Doctor:
                        _isDoctorCountrySettled = value != null;
                        _doctorCountry = value;
                        break;
                }
                
            }
        }

        public static Region Region
        {
            get
            {
                switch (WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        return _callerRegion;
                    case GeographyWhoIsRunning.Subcriber:
                        return _subscriberRegion;
                    case GeographyWhoIsRunning.Doctor:
                        return _doctorRegion;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }
            set
            {
                switch (WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        _isCallerRegionSettled = value != null;
                        _callerRegion = value;
                        break;
                    case GeographyWhoIsRunning.Subcriber:
                        _isSubscriberRegionSettled = value != null;
                        _subscriberRegion = value;
                        break;
                    case GeographyWhoIsRunning.Doctor:
                        _isDoctorRegionSettled = value != null;
                        _doctorRegion = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }
        }

        public static City City
        {
            get
            {
                switch (WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        return _callerCity;
                    case GeographyWhoIsRunning.Subcriber:
                        return _subscriberCity;
                    case GeographyWhoIsRunning.Doctor:
                        return _doctorCity;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }
            set
            {
                switch (WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        _isCallerCitySettled = value != null;
                        _callerCity = value;
                        break;
                    case GeographyWhoIsRunning.Subcriber:
                        _isSubscriberCitySettled = value != null;
                        _subscriberCity = value;
                        break;
                    case GeographyWhoIsRunning.Doctor:
                        _isDoctorCitySettled = value != null;
                        _doctorCity = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public static ObservableCollection<Country> Countries => DataBaseManager.AllCountries;

        public static ObservableCollection<Region> Regions
        {
            get
            {
                switch (WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        return _isCallerCountrySettled ? DataBaseManager.GetRegionsByCountry(Country) : DataBaseManager.AllRegions;
                    case GeographyWhoIsRunning.Subcriber:
                        return _isSubscriberCountrySettled ? DataBaseManager.GetRegionsByCountry(Country) : DataBaseManager.AllRegions;
                    case GeographyWhoIsRunning.Doctor:
                        return _isDoctorCountrySettled ? DataBaseManager.GetRegionsByCountry(Country) : DataBaseManager.AllRegions;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
        }

        public static ObservableCollection<City> Cities
        {
            get
            {
                switch (WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        if (_isCallerRegionSettled)
                        {
                            return DataBaseManager.GetCitiesByRegion(Region);
                        }
                        return _isCallerCountrySettled ? DataBaseManager.GetCitiesByCountry(Country) : DataBaseManager.AllCities;

                    case GeographyWhoIsRunning.Subcriber:
                        if (_isSubscriberRegionSettled)
                        {
                            return DataBaseManager.GetCitiesByRegion(Region);
                        }
                        return _isSubscriberCountrySettled ? DataBaseManager.GetCitiesByCountry(Country) : DataBaseManager.AllCities;

                    case GeographyWhoIsRunning.Doctor:
                        if (_isDoctorRegionSettled)
                        {
                            return DataBaseManager.GetCitiesByRegion(Region);
                        }
                        return _isDoctorCountrySettled ? DataBaseManager.GetCitiesByCountry(Country) : DataBaseManager.AllCities;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
        }
    }
}
