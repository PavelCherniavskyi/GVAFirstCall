using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstEvent.Model;

namespace FirstEvent.ViewModel.ListWindows
{
    public class DoctorsListViewModel : BaseListViewModel<Doctor>
    {
        public override ObservableCollection<Doctor> Items => _items ?? (_items = DataBaseManager.Doctors);

    }

    public class RelationToSubscrListViewModel : BaseListViewModel<RelationToSubscr>
    {
        public override ObservableCollection<RelationToSubscr> Items => _items ?? (_items = DataBaseManager.RelationsToSubscr);

    }
}
