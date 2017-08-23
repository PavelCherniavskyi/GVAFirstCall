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
    public class TreatDoctor : ViewModelBase
    {
        private TreatingDoctorView _treatingDoctorView;
        private Country _country;
        private Region _region;
        private City _city;

        private Brush _treatingDoctorColorTextBox;
        private Brush _countryColorTextBox;
        private Brush _regionColorTextBox;
        private Brush _cityColorTextBox;

        private bool _istreatingDoctorReadOnly;
        private bool _isCountryReadOnly;
        private bool _isRegionReadOnly;
        private bool _isCityReadOnly;

        public TreatDoctor()
        {
            Messenger.Default.Register(this, "TreatDoctorTreatingDoctorView", new Action<TreatingDoctorView>(TreatDoctorMessage));
            Messenger.Default.Register(this, "TreatDoctorCountry", new Action<Country>(CountryCallerMessage));
            Messenger.Default.Register(this, "TreatDoctorRegion", new Action<Region>(RegionCallerMessage));
            Messenger.Default.Register(this, "TreatDoctorCity", new Action<City>(CityCallerMessage));

            _country = new Country();
            _region = new Region();
            _city = new City();
            _treatingDoctorView = new TreatingDoctorView();
        }

        public string LocationInfo { get; set; }

        public bool IsDoctor { get; set; }

        public bool IsFacility { get; set; }

        public bool IstreatingDoctorReadOnly
        {
            get { return _istreatingDoctorReadOnly; }
            set
            {
                _istreatingDoctorReadOnly = value;
                TreatingDoctorColorTextBox = _istreatingDoctorReadOnly ?
                    new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)) :
                    new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                RaisePropertyChanged("IstreatingDoctorReadOnly");
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

        public TreatingDoctorView TreatingDoctorView
        {
            get { return _treatingDoctorView; }
            set { _treatingDoctorView = value; RaisePropertyChanged("TreatingDoctorView"); }
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

        public Brush TreatingDoctorColorTextBox
        {
            get { return _treatingDoctorColorTextBox; }
            set { _treatingDoctorColorTextBox = value; RaisePropertyChanged("TreatingDoctorColorTextBox"); }
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

        public RelayCommand ShowTreatDocListWindow { get; } = new RelayCommand(() => new TreatDocListWindow().ShowDialog(), () => true);

        public RelayCommand ShowCountryListWindow { get; } = new RelayCommand(() => {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
            new CountryList().ShowDialog();
        }, () => true);

        public RelayCommand ShowRegionListWindow { get; } = new RelayCommand(() => {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
            new RegionList().ShowDialog();
        }, () => true);

        public RelayCommand ShowCityListWindow { get; } = new RelayCommand(() =>
        {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
            new CityList().ShowDialog();
        }, () => true);

        public ICommand DocEnterKeyCommand
        {
            get
            {
                return new RelayCommand(DocEnterKeyCommandExecute, () => true);
            }
        }

        public ICommand CountryEnterKeyCommand
        {
            get
            {
                return new RelayCommand(CountryEnterKeyCommandExecute, () => true);
            }
        }

        public ICommand RegionEnterKeyCommand
        {
            get
            {
                return new RelayCommand(RegionEnterKeyCommandExecute, () => true);
            }
        }

        public ICommand CityEnterKeyCommand
        {
            get
            {
                return new RelayCommand(CityEnterKeyCommandExecute, () => true);
            }
        }

        public ICommand CancelDocTreatListWindow
        {
            get
            {
                return new RelayCommand(ClearDocTreatFeildExecute, () => true);
            }
        }

        public ICommand CancelCountryListWindow
        {
            get
            {
                return new RelayCommand(ClearCountryFeildExecute, () => true);
            }
        }

        public ICommand CancelRegionListWindow
        {
            get
            {
                return new RelayCommand(ClearRegionFeildExecute, () => true);
            }
        }

        public ICommand CancelCityListWindow
        {
            get
            {
                return new RelayCommand(ClearCityFeildExecute, () => true);
            }
        }

        private void ClearDocTreatFeildExecute()
        {
            TreatingDoctorView = new TreatingDoctorView();
            IstreatingDoctorReadOnly = false;
        }

        private void ClearCountryFeildExecute()
        {
            Country = new Country();
            Region = new Region();
            City = new City();
            IsCountryReadOnly = false;
            IsRegionReadOnly = false;
            IsCityReadOnly = false;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
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
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
            CallerGeographySwitcher.Region = null;
            CallerGeographySwitcher.City = null;
        }

        private void ClearCityFeildExecute()
        {
            City = new City();
            IsCityReadOnly = false;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
            CallerGeographySwitcher.City = null;
        }

        private void CountryEnterKeyCommandExecute()
        {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
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
                IsCountryReadOnly = true;
                break;
            }

        }

        private void RegionEnterKeyCommandExecute()
        {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
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
                IsRegionReadOnly = true;
                IsCountryReadOnly = true;
                break;
            }
        }

        private void CityEnterKeyCommandExecute()
        {
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;

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
                IsCityReadOnly = true;
                IsCountryReadOnly = true;
                IsRegionReadOnly = true;
                break;
            }
        }

        private void DocEnterKeyCommandExecute()
        {
            int tempId;
            var isNumber = int.TryParse(TreatingDoctorView.Name, out tempId);
            var strToSrch = TreatingDoctorView.Name.ToUpper();

            foreach (var d in DataBaseManager.TreatDocViews)
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
                TreatingDoctorView = d;
                IstreatingDoctorReadOnly = true;
                break;
            }
        }

        private void TreatDoctorMessage(TreatingDoctorView doc)
        {
            if (doc == null)
                return;
            TreatingDoctorView = doc;
            IstreatingDoctorReadOnly = true;
        }

        private void CountryCallerMessage(Country c)
        {
            if (c == null)
                return;
            Country = c;
            IsCountryReadOnly = true;
        }

        private void RegionCallerMessage(Region r)
        {
            if (r == null)
                return;
            Region = r;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
            CallerGeographySwitcher.Region = r;
            Country = DataBaseManager.GetCountryByRegion(r);
            CallerGeographySwitcher.Country = Country;
            IsRegionReadOnly = true;
            IsCountryReadOnly = true;
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
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
            CallerGeographySwitcher.City = c;
            CallerGeographySwitcher.Country = Country;
            CallerGeographySwitcher.Region = Region;
            IsCityReadOnly = true;
            IsCountryReadOnly = true;
            IsRegionReadOnly = true;
        }
    }
}
