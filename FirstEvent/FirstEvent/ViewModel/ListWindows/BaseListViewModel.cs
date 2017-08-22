using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FirstEvent.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.ListWindows
{
    public abstract class BaseListViewModel<T> : ViewModelBase
    {
        protected RelayCommand OkClickCommand;
        protected RelayCommand CalcelClickCommand;
        protected ObservableCollection<T> _items;

        protected BaseListViewModel()
        {
            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            OkWindowCommand = new RelayCommand<Window>(OkWindow);
        }

        public abstract ObservableCollection<T> Items { get; set; }

        public T SelectedItem { set; get; }

        public RelayCommand<Window> CloseWindowCommand { get; protected set; }

        public RelayCommand<Window> OkWindowCommand { get; protected set; }

        protected void CloseWindow(Window window)
        {
            window?.Close();
        }

        protected abstract void OkWindow(Window window);

    }
}
