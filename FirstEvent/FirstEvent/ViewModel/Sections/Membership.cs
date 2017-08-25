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
        private Brush _insuranceColorTextBox;
        private bool _isInsuranceReadOnly;

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
                    IsInsuranceReadOnly = true;
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
                    IsInsuranceReadOnly = true;
                }
            }
        }

        private void ClearInsuranceFeildExecute()
        {
            Insurance = new InsuranceView();
            IsInsuranceReadOnly = false;
        }

        public InsuranceView Insurance
        {
            get { return _insurance ?? (_insurance = new InsuranceView()); }
            set { _insurance = value; RaisePropertyChanged("Insurance"); }
        }

        public Brush InsuranceColorTextBox
        {
            get { return _insuranceColorTextBox; }
            set { _insuranceColorTextBox = value; RaisePropertyChanged("InsuranceColorTextBox"); }
        }

        public bool IsInsuranceReadOnly
        {
            get { return _isInsuranceReadOnly; }
            set
            {
                _isInsuranceReadOnly = value;
                InsuranceColorTextBox = _isInsuranceReadOnly ?
                    new SolidColorBrush(Color.FromArgb(255, 240, 240, 240)) :
                    new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                RaisePropertyChanged("IsInsuranceReadOnly");
            }
        }

        private void InsuranceCallerMessage(InsuranceView i)
        {
            if (i != null)
            {
                Insurance = i;
                IsInsuranceReadOnly = true;
            }
        }
    }
}
