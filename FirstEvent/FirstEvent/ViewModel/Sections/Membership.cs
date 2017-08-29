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

        public Membership()
        {
            Messenger.Default.Register(this, "MembershipInsurance", new Action<InsuranceView>(InsuranceCallerMessage));
        }

        public string PolicyNum { set; get; }

        public string LetterCode { set; get; }

        public string InsuredDays { set; get; }

        public string InsuredProgramm { set; get; }

        public string Deductable { set; get; }

        public DateTime ValidFrom { set; get; } = DateTime.Now;

        public DateTime ValidTo { set; get; } = DateTime.Now;

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
