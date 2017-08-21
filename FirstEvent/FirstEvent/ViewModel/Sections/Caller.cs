using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using FirstEvent.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.Sections
{
    public class Caller : ViewModelBase
    {
        private RelayCommand _showRelationToSubscrListCommand;
        private RelayCommand _cancelRelationToSubscrListCommand;
        private RelayCommand _showCountryListCommand;
        private RelayCommand _cancelCountryListCommand;
        private RelayCommand _showCityListCommand;
        private RelayCommand _cancelCityListCommand;
        private RelayCommand _showRegionListCommand;
        private RelayCommand _cancelRegionListCommand;

        private RelToSub _relToSub;
        private Country _country;
        private Region _region;
        private City _city;

        private RelayCommand _relEnterKeyCommand;
        private RelayCommand _countryEnterKeyCommand;
        private RelayCommand _regionEnterKeyCommand;
        private RelayCommand _cityEnterKeyCommand;

        private Brush _relationToSubscrColorTextBox;
        private Brush _countryColorTextBox;
        private Brush _regionColorTextBox;
        private Brush _cityColorTextBox;

        private bool _isRelToSubscrReadOnly;
        private bool _isCountryReadOnly;
        private bool _isRegionReadOnly;
        private bool _isCityReadOnly;

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

        public bool IsRelToSubscrReadOnly
        {
            get { return _isRelToSubscrReadOnly; }
            set
            {
                _isRelToSubscrReadOnly = value;
                RelationToSubscrColorTextBox = _isRelToSubscrReadOnly ?
                    new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)) :
                    new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                RaisePropertyChanged("IsRelToSubscrReadOnly");
            }
        }

        public bool IsCountryReadOnly
        {
            get { return _isCountryReadOnly; }
            set
            {
                _isCountryReadOnly = value;
                CountryColorTextBox = _isCountryReadOnly ?
                    new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)) :
                    new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                RaisePropertyChanged("IsCountryReadOnly");
            }
        }

        public bool IsRegionReadOnly
        {
            get { return _isRegionReadOnly; }
            set
            {
                _isRegionReadOnly = value;
                RegionColorTextBox = _isRegionReadOnly ?
                    new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)) :
                    new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                RaisePropertyChanged("IsRegionReadOnly");
            }
        }

        public bool IsCityReadOnly
        {
            get { return _isCityReadOnly; }
            set
            {
                _isCityReadOnly = value;
                CityColorTextBox = _isCityReadOnly ?
                    new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)) :
                    new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                RaisePropertyChanged("IsCityReadOnly");
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

        public Brush RelationToSubscrColorTextBox
        {
            get { return _relationToSubscrColorTextBox; }
            set { _relationToSubscrColorTextBox = value; RaisePropertyChanged("RelationToSubscrColorTextBox"); }
        }

        public Brush CountryColorTextBox
        {
            get { return _countryColorTextBox; }
            set { _countryColorTextBox = value; RaisePropertyChanged("CountryColorTextBox"); }
        }

        public Brush RegionColorTextBox
        {
            get { return _regionColorTextBox; }
            set { _regionColorTextBox = value; RaisePropertyChanged("RegionColorTextBox"); }
        }

        public Brush CityColorTextBox
        {
            get { return _cityColorTextBox; }
            set { _cityColorTextBox = value; RaisePropertyChanged("CityColorTextBox"); }
        }

        public ICommand ShowRelationToSubscrListWindow
        {
            get
            {
                return _showRelationToSubscrListCommand ??
                    (_showRelationToSubscrListCommand = new RelayCommand(() => Messenger.Default.Send<string>("SubscrListShow"), () => true));
            }
        }

        public ICommand ShowCountryListWindow
        {
            get
            {
                return _showCountryListCommand ??
                    (_showCountryListCommand = new RelayCommand(() => Messenger.Default.Send<string>("CountryListShow"), () => true));
            }
        }

        public ICommand ShowRegionListWindow
        {
            get
            {
                return _showRegionListCommand ??
                    (_showRegionListCommand = new RelayCommand(() => Messenger.Default.Send<string>("RegionListShow"), () => true));
            }
        }

        public ICommand ShowCityListWindow
        {
            get
            {
                return _showCityListCommand ??
                    (_showCityListCommand = new RelayCommand(() => Messenger.Default.Send<string>("CityListShow"), () => true));
            }
        }

        public ICommand RelEnterKeyCommand
        {
            get
            {
                return _relEnterKeyCommand ??
                    (_relEnterKeyCommand = new RelayCommand(RelEnterKeyCommandExecute, () => true));
            }
        }

        public ICommand CountryEnterKeyCommand
        {
            get
            {
                return _countryEnterKeyCommand ??
                    (_countryEnterKeyCommand = new RelayCommand(CountryEnterKeyCommandExecute, () => true));
            }
        }

        public ICommand RegionEnterKeyCommand
        {
            get
            {
                return _regionEnterKeyCommand ??
                    (_regionEnterKeyCommand = new RelayCommand(RegionEnterKeyCommandExecute, () => true));
            }
        }

        public ICommand CityEnterKeyCommand
        {
            get
            {
                return _cityEnterKeyCommand ??
                    (_cityEnterKeyCommand = new RelayCommand(CityEnterKeyCommandExecute, () => true));
            }
        }

        public ICommand CancelRelationToSubscrListWindow
        {
            get
            {
                return _cancelRelationToSubscrListCommand ??
                    (_cancelRelationToSubscrListCommand = new RelayCommand(ClearRelToSubFeildExecute, () => true));
            }
        }

        public ICommand CancelCountryListWindow
        {
            get
            {
                return _cancelCountryListCommand ??
                    (_cancelCountryListCommand = new RelayCommand(ClearCountryFeildExecute, () => true));
            }
        }

        public ICommand CancelRegionListWindow
        {
            get
            {
                return _cancelRegionListCommand ??
                    (_cancelRegionListCommand = new RelayCommand(ClearRegionFeildExecute, () => true));
            }
        }

        public ICommand CancelCityListWindow
        {
            get
            {
                return _cancelCityListCommand ??
                    (_cancelCityListCommand = new RelayCommand(ClearCityFeildExecute, () => true));
            }
        }

        private void ClearRelToSubFeildExecute()
        {
            RelToSub = new RelToSub();
            IsRelToSubscrReadOnly = false;
        }

        private void ClearCountryFeildExecute()
        {
            Country = new Country();
            Region = new Region();
            City = new City();
            IsCountryReadOnly = false;
            IsRegionReadOnly = false;
            IsCityReadOnly = false;
            CallerGeographySwitcher.Country = null;
            CallerGeographySwitcher.Region = null;
            CallerGeographySwitcher.City = null;
        }

        private void ClearRegionFeildExecute()
        {
            Region = new Region();
            City = new City();
            IsRegionReadOnly = false;
            IsCityReadOnly = false;
            CallerGeographySwitcher.Region = null;
            CallerGeographySwitcher.City = null;
        }

        private void ClearCityFeildExecute()
        {
            City = new City();
            IsCityReadOnly = false;
            CallerGeographySwitcher.City = null;
        }

        private void CountryEnterKeyCommandExecute()
        {
            int tempId;
            if (int.TryParse(Country.Name, out tempId))
            {
                foreach (var c in DataBaseManager.AllCountries)
                {
                    if (tempId != c.Id)
                        continue;
                    Country = c;
                    CallerGeographySwitcher.Country = c;
                    IsCountryReadOnly = true;
                    break;
                }
            }
            else
            {
                var strToSrch = Country.Name.ToUpper();

                foreach (var d in DataBaseManager.AllCountries)
                {
                    if (!d.Name.ToUpper().Contains(strToSrch))
                        continue;
                    Country = d;
                    CallerGeographySwitcher.Country = d;
                    IsCountryReadOnly = true;
                }
            }

        }

        private void RegionEnterKeyCommandExecute()
        {
            int tempId;
            if (int.TryParse(Region.Name, out tempId))
            {
                foreach (var r in DataBaseManager.AllRegions)
                {
                    if (tempId != r.Id)
                        continue;
                    Region = r;
                    CallerGeographySwitcher.Region = r;
                    Country = DataBaseManager.GetCountryByRegion(r);
                    CallerGeographySwitcher.Country = Country;
                    IsRegionReadOnly = true;
                    IsCountryReadOnly = true;
                    break;
                }
            }
            else
            {
                var strToSrch = Region.Name.ToUpper();

                foreach (var r in DataBaseManager.AllRegions)
                {
                    if (!r.Name.ToUpper().Contains(strToSrch))
                        continue;
                    Region = r;
                    CallerGeographySwitcher.Region = r;
                    Country = DataBaseManager.GetCountryByRegion(r);
                    CallerGeographySwitcher.Country = Country;
                    IsRegionReadOnly = true;
                    IsCountryReadOnly = true;
                }
            }

        }

        private void CityEnterKeyCommandExecute()
        {
            int tempId;
            if (int.TryParse(City.Name, out tempId))
            {
                foreach (var c in DataBaseManager.AllCities)
                {
                    if (tempId != c.Id)
                        continue;
                    Country country;
                    Region region;
                    DataBaseManager.GetCountryAndRegionByCity(c, out region, out country);
                    City = c;
                    Region = region;
                    Country = country;
                    CallerGeographySwitcher.City = c;
                    CallerGeographySwitcher.Country = Country;
                    CallerGeographySwitcher.Region = Region;
                    IsCityReadOnly = true;
                    IsCountryReadOnly = true;
                    IsRegionReadOnly = true;
                    break;
                }
            }
            else
            {
                var strToSrch = City.Name.ToUpper();

                foreach (var c in DataBaseManager.AllCities)
                {
                    if (!c.Name.ToUpper().Contains(strToSrch))
                        continue;
                    Country country;
                    Region region;
                    DataBaseManager.GetCountryAndRegionByCity(c, out region, out country);
                    City = c;
                    Region = region;
                    Country = country;
                    CallerGeographySwitcher.City = c;
                    CallerGeographySwitcher.Country = Country;
                    CallerGeographySwitcher.Region = Region;
                    IsCityReadOnly = true;
                    IsCountryReadOnly = true;
                    IsRegionReadOnly = true;
                }
            }

        }

        private void RelEnterKeyCommandExecute()
        {
            int tempId;
            if (int.TryParse(RelToSub.Name, out tempId))
            {
                foreach (var d in DataBaseManager.AllRelToSubs)
                {
                    if (tempId != d.Id)
                        continue;
                    RelToSub = d;
                    IsRelToSubscrReadOnly = true;
                    break;
                }
            }
            else
            {
                var strToSrch = RelToSub.Name.ToUpper();

                foreach (var d in DataBaseManager.AllRelToSubs)
                {
                    if (!d.Name.ToUpper().Contains(strToSrch))
                        continue;
                    RelToSub = d;
                    IsRelToSubscrReadOnly = true;
                }
            }
        }

        private void RelCallerMessage(RelToSub rel)
        {
            if (rel != null)
            {
                RelToSub = rel;
                IsRelToSubscrReadOnly = true;
            }
            Messenger.Default.Send<string>("SubscrListHide");

        }

        private void CountryCallerMessage(Country c)
        {
            if (c != null)
            {
                Country = c;
                IsCountryReadOnly = true;
            }
            Messenger.Default.Send<string>("CountryListHide");

        }

        private void RegionCallerMessage(Region r)
        {
            if (r != null)
            {
                Region = r;
                CallerGeographySwitcher.Region = r;
                Country = DataBaseManager.GetCountryByRegion(r);
                CallerGeographySwitcher.Country = Country;
                IsRegionReadOnly = true;
                IsCountryReadOnly = true;
            }
            Messenger.Default.Send<string>("RegionListHide");

        }

        private void CityCallerMessage(City c)
        {
            if (c != null)
            {
                City = c;
                Country country;
                Region region;
                DataBaseManager.GetCountryAndRegionByCity(c, out region, out country);
                City = c;
                Region = region;
                Country = country;
                CallerGeographySwitcher.City = c;
                CallerGeographySwitcher.Country = Country;
                CallerGeographySwitcher.Region = Region;
                IsCityReadOnly = true;
                IsCountryReadOnly = true;
                IsRegionReadOnly = true;
            }
            Messenger.Default.Send<string>("CityListHide");

        }
    }
}
