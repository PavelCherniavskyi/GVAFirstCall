using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FirstEvent.Model;
using FirstEvent.View;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.ListWindows
{
    public class DoctorsListViewModel : BaseListViewModel<DocView>
    {
        public override ObservableCollection<DocView> Items
        {
            get { return _items ?? (_items = DataBaseManager.DocViews); }
            set { _items = value; RaisePropertyChanged("Items"); }
        }

        protected override void OkWindow(Window window)
        {
            if (SelectedItem != null)
                Messenger.Default.Send<DocView>(SelectedItem, "GeneralInfoDoctor");
            CloseWindow(window);
        }
    }

    public class RelationToSubscrListViewModel : BaseListViewModel<RelToSub>
    {
        public override ObservableCollection<RelToSub> Items
        {
            get { return _items ?? (_items = DataBaseManager.AllRelToSubs); }
            set {_items = value; RaisePropertyChanged("Items"); }
        }

        protected override void OkWindow(Window window)
        {
            if (SelectedItem != null)
                Messenger.Default.Send<RelToSub>(SelectedItem, "CallerRelationToSubscr");
            CloseWindow(window);
        }

    }

    public class CallerCountryListViewModel : BaseListViewModel<Country>
    {

        public override ObservableCollection<Country> Items
        {
            get { return CallerGeographySwitcher.Countries; }
            set { _items = value;}
        }

        protected override void OkWindow(Window window)
        {
            if (SelectedItem == null)
            {
                CallerGeographySwitcher.Country = null;
            }
            else
            {
                CallerGeographySwitcher.Country = SelectedItem;
                switch (CallerGeographySwitcher.WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        Messenger.Default.Send<Country>(SelectedItem, "CallerCountry");
                        break;
                    case GeographyWhoIsRunning.Subcriber:
                        Messenger.Default.Send<Country>(SelectedItem, "SubscriberCountry");
                        break;
                    case GeographyWhoIsRunning.Doctor:
                        Messenger.Default.Send<Country>(SelectedItem, "TreatDoctorCountry");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            CloseWindow(window);
        }
    }

    public class CallerRegionListViewModel : BaseListViewModel<Region>
    {

        public override ObservableCollection<Region> Items
        {
            get { return CallerGeographySwitcher.Regions; }
            set { _items = value; }
        }

        protected override void OkWindow(Window window)
        {
            if (SelectedItem == null)
            {
                CallerGeographySwitcher.Region = null;
            }
            else
            {
                CallerGeographySwitcher.Region = SelectedItem;
                switch (CallerGeographySwitcher.WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        Messenger.Default.Send<Region>(SelectedItem, "CallerRegion");
                        break;
                    case GeographyWhoIsRunning.Subcriber:
                        Messenger.Default.Send<Region>(SelectedItem, "SubscriberRegion");
                        break;
                    case GeographyWhoIsRunning.Doctor:
                        Messenger.Default.Send<Region>(SelectedItem, "TreatDoctorRegion");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            CloseWindow(window);
        }

    }

    public class CallerCityListViewModel : BaseListViewModel<City>
    {
        public override ObservableCollection<City> Items
        {
            get { return CallerGeographySwitcher.Cities; }
            set { _items = value; }
        }

        protected override void OkWindow(Window window)
        {
            if (SelectedItem == null)
            {
                CallerGeographySwitcher.City = null;
            }
            else
            {
                CallerGeographySwitcher.City = SelectedItem;
                switch (CallerGeographySwitcher.WhoIsRunning)
                {
                    case GeographyWhoIsRunning.Caller:
                        Messenger.Default.Send<City>(SelectedItem, "CallerCity");
                        break;
                    case GeographyWhoIsRunning.Subcriber:
                        Messenger.Default.Send<City>(SelectedItem, "SubscriberCity");
                        break;
                    case GeographyWhoIsRunning.Doctor:
                        Messenger.Default.Send<City>(SelectedItem, "TreatDoctorCity");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            CloseWindow(window);
        }

    }

    public class HotelListViewModel : BaseListViewModel<HotelView>
    {
        public override ObservableCollection<HotelView> Items
        {
            get { return _items ?? (_items = DataBaseManager.HotelViews); }
            set { _items = value; RaisePropertyChanged("Items"); }
        }

        protected override void OkWindow(Window window)
        {
            if (SelectedItem != null)
                Messenger.Default.Send<HotelView>(SelectedItem, "SubscriberHotel");
            CloseWindow(window);
        }

    }

    public class FeListViewModel : BaseListViewModel<FirstCall>
    {
        public FeListViewModel()
        {
            BackCommand = new RelayCommand<Window>(BackCommandExecute);
        }
        public override ObservableCollection<FirstCall> Items
        {
            get { return _items ?? (_items = DataBaseManager.FirstCalls); }
            set { _items = value; RaisePropertyChanged("Items"); }
        }

        protected override void OkWindow(Window window)
        {
            if (SelectedItem != null)
                new MainWindow(SelectedItem.BuildViewModel()).Show();
            CloseWindow(window);
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(DeleteCommandExecute, () => true);
            }
        }

        private void DeleteCommandExecute()
        {
            if(SelectedItem == null)
                return;
            DataBaseManager.RemoveFirstCall(SelectedItem);
            Items = DataBaseManager.FirstCalls;
        }

        public RelayCommand<Window> BackCommand { get; set; }

        private void BackCommandExecute(Window w)
        {
            new WelcomeWind().Show();
            w.Close();
        }

    }

    public class InsurancelListViewModel : BaseListViewModel<InsuranceView>
    {
        private ObservableCollection<Office> _offices;
        private Office _office;
        public override ObservableCollection<InsuranceView> Items
        {
            get { return _items ?? (_items = DataBaseManager.InsuranceViews); }
            set { _items = value; RaisePropertyChanged("Items"); }
        }

        public Office OfficeSelected
        {
            get { return _office ?? (_office = new Office()); }
            set
            {
                Items = DataBaseManager.GetInsurancesByOffice(value);
                _office = value;
                RaisePropertyChanged("OfficeSelected");
            }
        }

        public ObservableCollection<Office> Offices
        {
            get { return _offices ?? (_offices = DataBaseManager.AllOffices); }
            set { _offices = value; RaisePropertyChanged("Offices"); }
        }

        protected override void OkWindow(Window window)
        {
            if (SelectedItem != null)
                Messenger.Default.Send<InsuranceView>(SelectedItem, "MembershipInsurance");
            CloseWindow(window);
        }

    }

    public class TreatDocListViewModel : BaseListViewModel<TreatingDoctorView>
    {
        public override ObservableCollection<TreatingDoctorView> Items
        {
            get { return _items ?? (_items = DataBaseManager.TreatDocViews); }
            set { _items = value; RaisePropertyChanged("Items"); }
        }

        protected override void OkWindow(Window window)
        {
            if (SelectedItem != null)
                Messenger.Default.Send<TreatingDoctorView>(SelectedItem, "TreatDoctorTreatingDoctorView");
            CloseWindow(window);
        }
    }
}
