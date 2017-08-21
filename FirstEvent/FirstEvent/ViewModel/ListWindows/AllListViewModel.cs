using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                return;
            }
            Messenger.Default.Send<DocView>(SelectedItem, "GeneralInfoDoctor");
        }

        public override ICommand CancelClickCommand
        {
            get
            {
                return _calcelClickCommand ??
                    (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<DocView>(null, "GeneralInfoDoctor"), () => true));
            }
        }
    }

    public class RelationToSubscrListViewModel : BaseListViewModel<RelToSub>
    {
        public override ObservableCollection<RelToSub> Items
        {
            get { return _items ?? (_items = DataBaseManager.AllRelToSubs); }
            set {_items = value; RaisePropertyChanged("Items"); }
        }

        public override void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                return;
            }
            Messenger.Default.Send<RelToSub>(SelectedItem, "CallerRelationToSubscr");
        }

        public override ICommand CancelClickCommand
        {
            get
            {
                return _calcelClickCommand ??
                    (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<RelToSub>(null, "CallerRelationToSubscr"), () => true));
            }
        }
    }

    public class CallerCountryListViewModel : BaseListViewModel<Country>
    {
        public CallerCountryListViewModel()
        {
            Messenger.Default.Register(this, "CallerCountryOnLoad", new Action<byte>(OnLoad));
        }

        public override ObservableCollection<Country> Items
        {
            get { return CallerGeographySwitcher.Countries; }
            set { _items = value;}
        }

        public override void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                CallerGeographySwitcher.Country = null;
                return;
            }
            CallerGeographySwitcher.Country = SelectedItem;
            Messenger.Default.Send<Country>(SelectedItem, "CallerCountry");
        }

        public override ICommand CancelClickCommand
        {
            get
            {
                CallerGeographySwitcher.Country = null;
                return _calcelClickCommand ??
                    (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<Country>(null, "CallerCountry"), () => true));
            }
        }
    }

    public class CallerRegionListViewModel : BaseListViewModel<Region>
    {

        public CallerRegionListViewModel()
        {
            Messenger.Default.Register(this, "CallerRegionOnLoad", new Action<byte>(OnLoad));
        }
        public override ObservableCollection<Region> Items
        {
            get { return CallerGeographySwitcher.Regions; }
            set { _items = value; }
        }

        public override void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                CallerGeographySwitcher.Region = null;
                return;
            }
            CallerGeographySwitcher.Region = SelectedItem;
            Messenger.Default.Send<Region>(SelectedItem, "CallerRegion");
        }

        public override ICommand CancelClickCommand
        {
            get
            {
                CallerGeographySwitcher.Region = null;
                return _calcelClickCommand ??
                    (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<Region>(null, "CallerRegion"), () => true));
            }
        }
    }

    public class CallerCityListViewModel : BaseListViewModel<City>
    {
        public CallerCityListViewModel()
        {
            Messenger.Default.Register(this, "CallerCityOnLoad", new Action<byte>(OnLoad));
        }

        public override ObservableCollection<City> Items
        {
            get { return CallerGeographySwitcher.Cities; }
            set { _items = value; }
        }

        public override void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                CallerGeographySwitcher.City = null;
                return;
            }
            CallerGeographySwitcher.City = SelectedItem;
            Messenger.Default.Send<City>(SelectedItem, "CallerCity");
        }

        public override ICommand CancelClickCommand
        {
            get
            {
                CallerGeographySwitcher.City = null;
                return _calcelClickCommand ??
                    (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<City>(null, "CallerCity"), () => true));
            }
        }
    }
}
