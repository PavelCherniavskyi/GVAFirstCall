using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FirstEvent.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.ListWindows
{
    public class DoctorsListViewModel : BaseListViewModel<Doctor>
    {
        public override ObservableCollection<Doctor> Items => _items ?? (_items = DataBaseManager.Doctors);
        public override void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                return;
            }
            Messenger.Default.Send<Doctor>(SelectedItem, "GeneralInfoDoctor");
        }

        public override ICommand CancelClickCommand
        {
            get
            {
                return _calcelClickCommand ??
                    (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<Doctor>(null, "GeneralInfoDoctor"), () => true));
            }
        }
    }

    public class RelationToSubscrListViewModel : BaseListViewModel<RelationToSubscriber>
    {
        public override ObservableCollection<RelationToSubscriber> Items => _items ?? (_items = DataBaseManager.RelationsToSubscriber);
        public override void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                return;
            }
            Messenger.Default.Send<RelationToSubscriber>(SelectedItem, "CallerRelationToSubscr");
        }

        public override ICommand CancelClickCommand
        {
            get
            {
                return _calcelClickCommand ??
                    (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<RelationToSubscriber>(null, "CallerRelationToSubscr"), () => true));
            }
        }
    }
}
