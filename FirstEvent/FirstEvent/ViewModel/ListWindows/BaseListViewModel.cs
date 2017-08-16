using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.ListWindows
{
    public abstract class BaseListViewModel<T> where T : class 
    {
        private RelayCommand _okClickCommand;
        private RelayCommand _calcelClickCommand;
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

        public void ExecuteDoneClickCommand()
        {
            if (SelectedItem == null)
            {
                return;
            }
            Messenger.Default.Send<T>(SelectedItem);
        }

        public ICommand CancelClickCommand
        {
            get
            {
                return _calcelClickCommand ?? (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<T>(null), () => true));
            }
        }
    }
}
