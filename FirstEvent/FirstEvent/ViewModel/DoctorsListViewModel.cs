using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using FirstEvent.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel
{
    public class DoctorsListViewModel
    {
        ObservableCollection<Doctor> _doctors;
        private RelayCommand _okClickCommand;
        private RelayCommand _calcelClickCommand;
        public ObservableCollection<Doctor> Doctors
        {
            get { return _doctors ?? (_doctors = DataBaseManager.Doctors); }
        }

        public Doctor SelectedDoctor { set; get; }

        public ICommand DoneClickCommand
        {
            get
            {
                return _okClickCommand ?? (_okClickCommand = new RelayCommand(ExecuteDoneClickCommand, () => true));
            }
        }

        public void ExecuteDoneClickCommand()
        {
            if (SelectedDoctor == null)
            {
                return;
            }
            Messenger.Default.Send<Doctor>(SelectedDoctor);
        }

        public ICommand CancelClickCommand
        {
            get
            {
                return _okClickCommand ?? (_calcelClickCommand = new RelayCommand(() => Messenger.Default.Send<Doctor>(null), () => true));
            }
        }
    }
}
