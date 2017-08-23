using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FirstEvent.Model;
using GalaSoft.MvvmLight;

namespace FirstEvent.ViewModel.Sections
{
    public class CallInfo : ViewModelBase
    {
        private ObservableCollection<StatusOfCall> _statusOfCalls;
        private StatusOfCall _statusOfCall;

        public StatusOfCall StatusSelected
        {
            get { return _statusOfCall ?? (_statusOfCall = new StatusOfCall()); }
            set
            {
                _statusOfCall = value;
                RaisePropertyChanged("StatusSelected");
            }
        }

        public ObservableCollection<StatusOfCall> StatusOfCalls
        {
            get { return _statusOfCalls ?? (_statusOfCalls = DataBaseManager.StatusOfCalls); }
            set { _statusOfCalls = value; RaisePropertyChanged("StatusOfCalls"); }
        }
        public string ReasonForCall { get; set; }

        public bool IsChronical { get; set; }

        public bool IsAlcohol { get; set; }
    }
}
