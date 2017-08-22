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
                        Messenger.Default.Send<Country>(SelectedItem, "DoctorCountry");
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
                        Messenger.Default.Send<Region>(SelectedItem, "DoctorRegion");
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
                        Messenger.Default.Send<City>(SelectedItem, "DoctorCity");
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
}
