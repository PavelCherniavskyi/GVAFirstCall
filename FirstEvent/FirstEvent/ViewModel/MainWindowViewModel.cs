using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using FirstEvent.Model;
using FirstEvent.ViewModel.Sections;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public Caller Caller { get; set; }

        public GeneralInfo GeneralInfo { get; set; }

        public Membership Membership { get; set; }

        public Subscriber Subscriber { get; set; }

        public CallInfo CallInfo { get; set; }

        public TreatDoctor TreatDoctor { get; set; }

        public ContactSection ContactSection { get; set; }

        public MainWindowViewModel()
        {
            Caller = new Caller();
            GeneralInfo = new GeneralInfo();
            Membership = new Membership();
            Subscriber = new Subscriber();
            CallInfo = new CallInfo();
            TreatDoctor = new TreatDoctor();
            ContactSection = new ContactSection();
        }


        public ICommand OkButtonCommand
        {
            get
            {
                return new RelayCommand(OkButtonCommandExecute, () => true);
            }
        }

        private void OkButtonCommandExecute()
        {
            
        }

        public ICommand CancelButtonCommand
        {
            get
            {
                return new RelayCommand(CancelButtonCommandExecute, () => true);
            }
        }

        private void CancelButtonCommandExecute()
        {
            
        }

        public ICommand SaveButtonCommand
        {
            get
            {
                return new RelayCommand(SaveButtonCommandExecute, () => true);
            }
        }

        private void SaveButtonCommandExecute()
        {


        }


    }
}
