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
    public class Subscriber : ViewModelBase
    {
        private HotelView _hotel;
        private Country _country;
        private Region _region;
        private City _city;

        private bool _isHotelEnabled = true;
        private bool _isCountryEnabled = true;
        private bool _isRegionEnabled = true;
        private bool _isCityEnabled = true;
        private DateTime _departure = DateTime.Now;
        private DateTime _arrival = DateTime.Now;
        private string _duration;
        private Brush _departureColor;

        public Subscriber()
        {
            Messenger.Default.Register(this, "SubscriberHotel", new Action<HotelView>(HotelCallerMessage));
            Messenger.Default.Register(this, "SubscriberCountry", new Action<Country>(CountryCallerMessage));
            Messenger.Default.Register(this, "SubscriberRegion", new Action<Region>(RegionCallerMessage));
            Messenger.Default.Register(this, "SubscriberCity", new Action<City>(CityCallerMessage));
            _country = new Country();
            _region = new Region();
            _city = new City();
            _hotel = new HotelView();
            _departureColor = new SolidColorBrush(Color.FromArgb(255, 22, 32, 133));
        }

        public string HomeAdress { set; get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string LocationInfo { get; set; }

        public string Room { get; set; }

        public string Age { get; set; }

        public DateTime DoB { get; set; } = DateTime.Now;

        public DateTime Arrival
        {
            get { return _arrival; }
            set
            {
                var a = _departure - value;
                DepartureColor = a.Days > 0 ? new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)) : new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                Duration = a.Days + " Days.";
                _arrival = value;

            }
        }

        public DateTime Departure
        {
            get { return _departure; }
            set
            {
                var a = value - _arrival;
                DepartureColor = a.Days > 0 ? new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)) : new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                Duration = a.Days + " Days.";
                _departure = value;
            }
        }

        public Brush DepartureColor
        {
            get { return _departureColor; }
            set
            {
                _departureColor = value;
                RaisePropertyChanged("DepartureColor");
            }

        }

        public string Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                RaisePropertyChanged("Duration");
            }
        }

        public bool IsHotelEnabled
        {
            get { return _isHotelEnabled; }
            set
            {
                _isHotelEnabled = value;
                RaisePropertyChanged("IsHotelEnabled");
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

        public HotelView Hotel
        {
            get { return _hotel; }
            set { _hotel = value; RaisePropertyChanged("Hotel"); }
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

        public RelayCommand ShowHotelListWindow { get; } = new RelayCommand(() => new HotelList().ShowDialog(), () => true);

        public RelayCommand ShowCountryListWindow { get; } = new RelayCommand(() => {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;
            new CountryList().ShowDialog();
        }, () => true);

        public RelayCommand ShowRegionListWindow { get; } = new RelayCommand(() => {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;
            new RegionList().ShowDialog();
        }, () => true);

        public RelayCommand ShowCityListWindow { get; } = new RelayCommand(() =>
        {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;
            new CityList().ShowDialog();
        }, () => true );

        public ICommand HotelEnterKeyCommand {
            get { return new RelayCommand(HotelEnterKeyCommandExecute, () => true); }
        }

        public ICommand CountryEnterKeyCommand {
            get { return new RelayCommand(CountryEnterKeyCommandExecute, () => true); }
        }

        public ICommand RegionEnterKeyCommand {
            get { return new RelayCommand(RegionEnterKeyCommandExecute, () => true); }
        }

        public ICommand CityEnterKeyCommand {
            get { return new RelayCommand(CityEnterKeyCommandExecute, () => true); }
        }

        public ICommand CancelHotelListWindow {
            get { return new RelayCommand(ClearHotelFeildExecute, () => true); }
        }

        public ICommand CancelCountryListWindow {
            get { return new RelayCommand(ClearCountryFeildExecute, () => true); }
        }

        public ICommand CancelRegionListWindow {
            get { return new RelayCommand(ClearRegionFeildExecute, () => true); }
        }

        public ICommand CancelCityListWindow {
            get { return new RelayCommand(ClearCityFeildExecute, () => true); }
        }

        private void ClearHotelFeildExecute()
        {
            Hotel = new HotelView();
            IsHotelEnabled = true;
        }

        private void ClearCountryFeildExecute()
        {
            Country = new Country();
            Region = new Region();
            City = new City();
            IsCountryEnabled = true;
            IsRegionEnabled = true;
            IsCityEnabled = true;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;
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
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;
            CallerGeographySwitcher.Region = null;
            CallerGeographySwitcher.City = null;
        }

        private void ClearCityFeildExecute()
        {
            City = new City();
            IsCityEnabled = true;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;
            CallerGeographySwitcher.City = null;
        }

        private void CountryEnterKeyCommandExecute()
        {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;
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
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;
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
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;

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

        private void HotelEnterKeyCommandExecute()
        {
            int tempId;
            var isNumber = int.TryParse(Hotel.Name, out tempId);
            var strToSearch = Hotel.Name.ToUpper();

            foreach (var d in DataBaseManager.HotelViews)
            {
                if (isNumber)
                {
                    if (tempId != d.Id)
                        continue;
                }
                else
                {
                    if (!d.Name.ToUpper().Contains(strToSearch))
                        continue;
                }
                Hotel = d;
                IsHotelEnabled = false;
                break;
            }
        }

        private void HotelCallerMessage(HotelView h)
        {
            if (h != null)
            {
                Hotel = h;
                IsHotelEnabled = false;
            }
        }

        private void CountryCallerMessage(Country c)
        {
            if (c != null)
            {
                Country = c;
                IsCountryEnabled = false;
            }
        }

        private void RegionCallerMessage(Region r)
        {
            if (r != null)
            {
                Region = r;
                CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;
                CallerGeographySwitcher.Region = r;
                Country = DataBaseManager.GetCountryByRegion(r);
                CallerGeographySwitcher.Country = Country;
                IsRegionEnabled = false;
                IsCountryEnabled = false;
            }
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
                CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Subcriber;
                CallerGeographySwitcher.City = c;
                CallerGeographySwitcher.Country = Country;
                CallerGeographySwitcher.Region = Region;
                IsCityEnabled = false;
                IsCountryEnabled = false;
                IsRegionEnabled = false;
            }
        }
    }
}
