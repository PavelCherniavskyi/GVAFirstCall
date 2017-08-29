using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using FirstEvent.Model;
using FirstEvent.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.Sections
{
    public class Caller : ViewModelBase
    {
        private RelToSub _relToSub;
        private Country _country;
        private Region _region;
        private City _city;

        private bool _isRelToSubscrEnabled = true;
        private bool _isCountryEnabled = true;
        private bool _isRegionEnabled = true;
        private bool _isCityEnabled = true;

        public Caller()
        {
            Messenger.Default.Register(this, "CallerRelationToSubscr", new Action<RelToSub>(RelCallerMessage));
            Messenger.Default.Register(this, "CallerCountry", new Action<Country>(CountryCallerMessage));
            Messenger.Default.Register(this, "CallerRegion", new Action<Region>(RegionCallerMessage));
            Messenger.Default.Register(this, "CallerCity", new Action<City>(CityCallerMessage));

            _country = new Country();
            _region = new Region();
            _city = new City();
            _relToSub = new RelToSub();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string LocationInfo { get; set; }

        public string PlaceOfStay { get; set; }

        public string Room { get; set; }

        public string CallerId { get; set; }

        public bool IsRelToSubscrEnabled
        {
            get { return _isRelToSubscrEnabled; }
            set
            {
                _isRelToSubscrEnabled = value;
                RaisePropertyChanged("IsRelToSubscrEnabled");
            }
        }

        public bool IsCountryEnabled
        {
            get { return _isCountryEnabled; }
            set
            {
                _isCountryEnabled = value;
                RaisePropertyChanged("IsCountryEnabled");
            }
        }

        public bool IsRegionEnabled
        {
            get { return _isRegionEnabled; }
            set
            {
                _isRegionEnabled = value;
                RaisePropertyChanged("IsRegionEnabled");
            }
        }

        public bool IsCityEnabled
        {
            get { return _isCityEnabled; }
            set
            {
                _isCityEnabled = value;
                RaisePropertyChanged("IsCityEnabled");
            }
        }

        public RelToSub RelToSub
        {
            get { return _relToSub; }
            set { _relToSub = value; RaisePropertyChanged("RelToSub"); }
        }

        public Country Country
        {
            get { return _country; }
            set { _country = value; RaisePropertyChanged("Country"); }
        }

        public Region Region
        {
            get { return _region; }
            set { _region = value; RaisePropertyChanged("Region"); }
        }

        public City City
        {
            get { return _city; }
            set { _city = value; RaisePropertyChanged("City"); }
        }

        public RelayCommand ShowRelationToSubscrListWindow { get; } = new RelayCommand(() => new RelationToSubscrList().ShowDialog(), () => true);

        public RelayCommand ShowCountryListWindow { get; } = new RelayCommand(() => {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;
            new CountryList().ShowDialog();
        }, () => true);

        public RelayCommand ShowRegionListWindow { get; } = new RelayCommand(() => {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;
            new RegionList().ShowDialog();
        }, () => true);

        public RelayCommand ShowCityListWindow { get; } = new RelayCommand(() =>
        {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;
            new CityList().ShowDialog();
        }, () => true);

        public ICommand RelEnterKeyCommand {
            get { return new RelayCommand(RelEnterKeyCommandExecute, () => true); }
        }

        public ICommand CountryEnterKeyCommand{
            get { return new RelayCommand(CountryEnterKeyCommandExecute, () => true); }
        }

        public ICommand RegionEnterKeyCommand {
            get { return new RelayCommand(RegionEnterKeyCommandExecute, () => true); }
        }

        public ICommand CityEnterKeyCommand {
            get { return new RelayCommand(CityEnterKeyCommandExecute, () => true); }
        }

        public ICommand CancelRelationToSubscrListWindow {
            get { return new RelayCommand(ClearRelToSubFeildExecute, () => true); }
        }

        public ICommand CancelCountryListWindow{
            get { return new RelayCommand(ClearCountryFeildExecute, () => true); }
        }

        public ICommand CancelRegionListWindow {
            get { return new RelayCommand(ClearRegionFeildExecute, () => true); }
        }

        public ICommand CancelCityListWindow {
            get { return new RelayCommand(ClearCityFeildExecute, () => true); }
        }

        private void ClearRelToSubFeildExecute()
        {
            RelToSub = new RelToSub();
            IsRelToSubscrEnabled = true;
        }

        private void ClearCountryFeildExecute()
        {
            Country = new Country();
            Region = new Region();
            City = new City();
            IsCountryEnabled = true;
            IsRegionEnabled = true;
            IsCityEnabled = true;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;
            CallerGeographySwitcher.Country = null;
            CallerGeographySwitcher.Region = null;
            CallerGeographySwitcher.City = null;
        }

        private void ClearRegionFeildExecute()
        {
            Region = new Region();
            City = new City();
            IsRegionEnabled = true;
            IsCityEnabled = true;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;
            CallerGeographySwitcher.Region = null;
            CallerGeographySwitcher.City = null;
        }

        private void ClearCityFeildExecute()
        {
            City = new City();
            IsCityEnabled = true;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;
            CallerGeographySwitcher.City = null;
        }

        private void CountryEnterKeyCommandExecute()
        {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;
            int tempId;
            var isNumber = int.TryParse(Country.Name, out tempId);
            var strToSearch = Country.Name.ToUpper();

            foreach (var c in DataBaseManager.AllCountries)
            {
                if (isNumber)
                {
                    if (tempId != c.Id)
                        continue;
                }
                else
                {
                    if (!c.Name.ToUpper().Contains(strToSearch))
                        continue;
                }
                
                Country = c;
                CallerGeographySwitcher.Country = c;
                IsCountryEnabled = false;
                break;
            }

        }

        private void RegionEnterKeyCommandExecute()
        {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;
            int tempId;
            var isNumber = int.TryParse(Region.Name, out tempId);
            var strToSearch = Region.Name.ToUpper();

            foreach (var r in CallerGeographySwitcher.Regions)
            {
                if (isNumber)
                {
                    if (tempId != r.Id)
                        continue;
                }
                else
                {
                    if (!r.Name.ToUpper().Contains(strToSearch))
                        continue;
                }

                Region = r;
                CallerGeographySwitcher.Region = r;
                Country = DataBaseManager.GetCountryByRegion(r);
                CallerGeographySwitcher.Country = Country;
                IsRegionEnabled = false;
                IsCountryEnabled = false;
                break;
            }
        }

        private void CityEnterKeyCommandExecute()
        {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;

            int tempId;
            var isNumber = int.TryParse(City.Name, out tempId);
            var strToSearch = City.Name.ToUpper();

            foreach (var c in CallerGeographySwitcher.Cities)
            {
                if (isNumber)
                {
                    if (tempId != c.Id)
                        continue;
                }
                else
                {
                    if (!c.Name.ToUpper().Contains(strToSearch))
                        continue;
                }

                Country country;
                Region region;
                DataBaseManager.GetCountryAndRegionByCity(c, out region, out country);
                City = c;
                Region = region;
                Country = country;
                CallerGeographySwitcher.City = c;
                CallerGeographySwitcher.Country = Country;
                CallerGeographySwitcher.Region = Region;
                IsCityEnabled = false;
                IsCountryEnabled = false;
                IsRegionEnabled = false;
                break;
            }

        }

        private void RelEnterKeyCommandExecute()
        {
            int tempId;
            var isNumber = int.TryParse(RelToSub.Name, out tempId);
            var strToSrch = RelToSub.Name.ToUpper();

            foreach (var d in DataBaseManager.AllRelToSubs)
            {
                if (isNumber)
                {
                    if (tempId != d.Id)
                        continue;
                }
                else
                {
                    if (!d.Name.ToUpper().Contains(strToSrch))
                        continue;
                }
                RelToSub = d;
                IsRelToSubscrEnabled = false;
                break;
            }
        }

        private void RelCallerMessage(RelToSub rel)
        {
            if (rel == null)
                return;
            RelToSub = rel;
            IsRelToSubscrEnabled = false;
        }

        private void CountryCallerMessage(Country c)
        {
            if (c == null)
                return;
            Country = c;
            IsCountryEnabled = false;
        }

        private void RegionCallerMessage(Region r)
        {
            if (r == null)
                return;
            Region = r;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;
            CallerGeographySwitcher.Region = r;
            Country = DataBaseManager.GetCountryByRegion(r);
            CallerGeographySwitcher.Country = Country;
            IsRegionEnabled = false;
            IsCountryEnabled = false;
        }

        private void CityCallerMessage(City c)
        {
            if (c == null)
                return;
            City = c;
            Country country;
            Region region;
            DataBaseManager.GetCountryAndRegionByCity(c, out region, out country);
            City = c;
            Region = region;
            Country = country;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Caller;
            CallerGeographySwitcher.City = c;
            CallerGeographySwitcher.Country = Country;
            CallerGeographySwitcher.Region = Region;
            IsCityEnabled = false;
            IsCountryEnabled = false;
            IsRegionEnabled = false;
        }
    }
}
