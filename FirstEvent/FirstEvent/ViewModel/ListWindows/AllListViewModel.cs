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
        public override ObservableCollection<DocView> Items => _items ?? (_items = DataBaseManager.DocViews);
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
        public override ObservableCollection<RelToSub> Items => _items ?? (_items = DataBaseManager.RelToSubs);
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

    public class CountryListViewModel : BaseListViewModel<Country>
    {
        public override ObservableCollection<Country> Items => _items ?? (_items = DataBaseManager.Countries);
        public override void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                return;
            }
            Messenger.Default.Send<Country>(SelectedItem, "CallerCountry");
        }

        public override ICommand CancelClickCommand
        {
            get
            {
                return _calcelClickCommand ??
                    (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<Country>(null, "CallerCountry"), () => true));
            }
        }
    }

    public class RegionListViewModel : BaseListViewModel<Region>
    {
        public override ObservableCollection<Region> Items => _items ?? (_items = DataBaseManager.Regions);
        public override void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                return;
            }
            Messenger.Default.Send<Region>(SelectedItem, "CallerRegion");
        }

        public override ICommand CancelClickCommand
        {
            get
            {
                return _calcelClickCommand ??
                    (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<Region>(null, "CallerRegion"), () => true));
            }
        }
    }

    public class CityListViewModel : BaseListViewModel<City>
    {
        public override ObservableCollection<City> Items => _items ?? (_items = DataBaseManager.Cities);
        public override void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                return;
            }
            Messenger.Default.Send<City>(SelectedItem, "CallerCity");
        }

        public override ICommand CancelClickCommand
        {
            get
            {
                return _calcelClickCommand ??
                    (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<City>(null, "CallerCity"), () => true));
            }
        }
    }
}
