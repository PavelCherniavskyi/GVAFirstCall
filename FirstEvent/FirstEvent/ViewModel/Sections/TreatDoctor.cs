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

        private bool _istreatingDoctorEnabled = true;
        private bool _isCountryEnabled = true;
        private bool _isRegionEnabled = true;
        private bool _isCityEnabled = true;

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

        public bool IstreatingDoctorEnabled
        {
            get { return _istreatingDoctorEnabled; }
            set
            {
                _istreatingDoctorEnabled = value;
                RaisePropertyChanged("IstreatingDoctorEnabled");
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

        public ICommand DocEnterKeyCommand => new RelayCommand(DocEnterKeyCommandExecute, () => true);

        public ICommand CountryEnterKeyCommand => new RelayCommand(CountryEnterKeyCommandExecute, () => true);

        public ICommand RegionEnterKeyCommand => new RelayCommand(RegionEnterKeyCommandExecute, () => true);

        public ICommand CityEnterKeyCommand => new RelayCommand(CityEnterKeyCommandExecute, () => true);

        public ICommand CancelDocTreatListWindow => new RelayCommand(ClearDocTreatFeildExecute, () => true);

        public ICommand CancelCountryListWindow => new RelayCommand(ClearCountryFeildExecute, () => true);

        public ICommand CancelRegionListWindow => new RelayCommand(ClearRegionFeildExecute, () => true);

        public ICommand CancelCityListWindow => new RelayCommand(ClearCityFeildExecute, () => true);

        private void ClearDocTreatFeildExecute()
        {
            TreatingDoctorView = new TreatingDoctorView();
            IstreatingDoctorEnabled = true;
        }

        private void ClearCountryFeildExecute()
        {
            Country = new Country();
            Region = new Region();
            City = new City();
            IsCountryEnabled = true;
            IsRegionEnabled = true;
            IsCityEnabled = true;
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
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
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
            CallerGeographySwitcher.Region = null;
            CallerGeographySwitcher.City = null;
        }

        private void ClearCityFeildExecute()
        {
            City = new City();
            IsCityEnabled = true;
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
                    if (!c.Name.ToUpper().Contains(strToSearch) && strToSearch != c.Code)
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
                IsRegionEnabled = false;
                IsCountryEnabled = false;
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
                IsCityEnabled = false;
                IsCountryEnabled = false;
                IsRegionEnabled = false;
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
                IstreatingDoctorEnabled = false;
                break;
            }
        }

        private void TreatDoctorMessage(TreatingDoctorView doc)
        {
            if (doc == null)
                return;
            TreatingDoctorView = doc;
            IstreatingDoctorEnabled = false;
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
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
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
            CallerGeographySwitcher.WhoIsRunning = GeographyWhoIsRunning.Doctor;
            CallerGeographySwitcher.City = c;
            CallerGeographySwitcher.Country = Country;
            CallerGeographySwitcher.Region = Region;
            IsCityEnabled = false;
            IsCountryEnabled = false;
            IsRegionEnabled = false;
        }
    }
}
