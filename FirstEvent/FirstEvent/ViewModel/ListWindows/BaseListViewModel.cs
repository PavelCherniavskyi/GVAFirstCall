using System.Collections.ObjectModel;
using System.Windows.Input;
using FirstEvent.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.ListWindows
{
    public abstract class BaseListViewModel<T> where T : class 
    {
        protected RelayCommand _okClickCommand;
        protected RelayCommand _calcelClickCommand;
        protected ObservableCollection<T> _items;
        public abstract ObservableCollection<T> Items { get; }

        public T SelectedItem { set; get; }

        public ICommand DoneClickCommand
        {
            get
            {
                return _okClickCommand ?? (_okClickCommand = new RelayCommand(ExecuteDoneClickCommand, () => true));
            }
        }

        public abstract void ExecuteDoneClickCommand();

        public abstract ICommand CancelClickCommand { get; }
    }
}
