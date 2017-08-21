using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public class CallerGeographySwitcher
    {
        private static Country _country;
        private static Region _region;
        private static City _city;
        private static bool _isCountrySettled;
        private static bool _isRegionSettled;
        private static bool _isCitySettled;

        public static Country Country
        {
            get { return _country; }
            set
            {
                _isCountrySettled = value != null;
                _country = value;
            }
        }

        public static Region Region
        {
            get { return _region; }
            set
            {
                _isRegionSettled = value != null;
                _region = value;
            }
        }

        public static City City
        {
            get { return _city; }
            set
            {
                _isCitySettled = value != null;
                _city = value;
            }
        }

        public static ObservableCollection<Country> Countries => DataBaseManager.AllCountries;

        public static ObservableCollection<Region> Regions => _isCountrySettled ? DataBaseManager.GetRegionsByCountry(Country) : DataBaseManager.AllRegions;

        public static ObservableCollection<City> Cities
        {
            get
            {
                if (_isRegionSettled)
                {
                    return DataBaseManager.GetCitiesByRegion(Region);
                }
                if (_isCountrySettled)
                {
                    return DataBaseManager.GetCitiesByCountry(Country);
                }

                return DataBaseManager.AllCities;
            }
        }
    }
}
