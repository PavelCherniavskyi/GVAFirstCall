using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using FirstEvent.Model;
using FirstEvent.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel.Sections
{
    public class Membership : ViewModelBase
    {
        private InsuranceView _insurance;
        private bool _isInsuranceEnabled = true;
        private string _insuredDays;
        private DateTime _validFrom = DateTime.MinValue;
        private DateTime _validTo = DateTime.MinValue;

        public Membership()
        {
            Messenger.Default.Register(this, "MembershipInsurance", new Action<InsuranceView>(InsuranceCallerMessage));
            PolicyNum = string.Empty;
        }

        public bool FindSubByPolInfoPressed { get; set; }

        public ICommand ButtonFindSubByPolInfoPressed => new RelayCommand(() => FindSubByPolInfoPressed = true, () => true);

        public string PolicyNum { set; get; }

        public string LetterCode { set; get; }

        public string InsuredDays
        {
            get { return _insuredDays; }
            set
            {
                _insuredDays = value;
                RaisePropertyChanged("InsuredDays");
            }
            
        }

        public string InsuredProgramm { set; get; }

        public string Deductable { set; get; }

        public DateTime ValidFrom
        {
            get { return _validFrom; }
            set
            {
                var a = _validTo - value;
                _validFrom = value;
                InsuredDays = a.Days.ToString();
            }
            
        }

        public DateTime ValidTo
        {
            get { return _validTo; }
            set
            {
                var a = value - _validFrom;
                _validTo = value;
                InsuredDays = a.Days.ToString();
            }
        }

        public RelayCommand ShowHotelListWindow { get; } = new RelayCommand(() => new Insurance_Companies().ShowDialog(), () => true);

        public ICommand InsuranceEnterKeyCommand
        {
            get
            {
                return new RelayCommand(InsuranceEnterKeyCommandExecute, () => true);
            }
        }

        public ICommand CancelInsuranceListWindow
        {
            get
            {
                return new RelayCommand(ClearInsuranceFeildExecute, () => true);
            }
        }

        private void InsuranceEnterKeyCommandExecute()
        {
            int tempId;
            if (int.TryParse(Insurance.Name, out tempId))
            {
                foreach (var i in DataBaseManager.InsuranceViews)
                {
                    if (tempId != i.Id)
                        continue;
                    Insurance = i;
                    IsInsuranceEnabled = false;
                    break;
                }
            }
            else
            {
                var strToSrch = Insurance.Name.ToUpper();

                foreach (var i in DataBaseManager.InsuranceViews)
                {
                    if (!i.Name.ToUpper().Contains(strToSrch))
                        continue;
                    Insurance = i;
                    IsInsuranceEnabled = false;
                }
            }
        }

        private void ClearInsuranceFeildExecute()
        {
            Insurance = new InsuranceView();
            IsInsuranceEnabled = true;
        }

        public InsuranceView Insurance
        {
            get { return _insurance ?? (_insurance = new InsuranceView()); }
            set { _insurance = value; RaisePropertyChanged("Insurance"); }
        }

        public bool IsInsuranceEnabled
        {
            get { return _isInsuranceEnabled; }
            set
            {
                _isInsuranceEnabled = value;
                RaisePropertyChanged("IsInsuranceEnabled");
            }
        }

        private void InsuranceCallerMessage(InsuranceView i)
        {
            if (i != null)
            {
                Insurance = i;
                IsInsuranceEnabled = false;
            }
        }
    }
}
