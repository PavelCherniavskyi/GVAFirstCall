using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FirstEvent.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.ListWindows
{
    public abstract class BaseListViewModel<T> : ViewModelBase
    {
        protected RelayCommand _okClickCommand;
        protected RelayCommand _calcelClickCommand;
        protected ObservableCollection<T> _items;

        public abstract ObservableCollection<T> Items { get; set; }

        public T SelectedItem { set; get; }

        public ICommand DoneClickCommand
        {
            get
            {
                return _okClickCommand ?? (_okClickCommand = new RelayCommand(ExecuteDoneClickCommand, () => true));
            }
        }

        public void OnLoad(byte str)
        {
            RaisePropertyChanged("Items");
        }

        public abstract void ExecuteDoneClickCommand();

        public abstract ICommand CancelClickCommand { get; }
    }
}
