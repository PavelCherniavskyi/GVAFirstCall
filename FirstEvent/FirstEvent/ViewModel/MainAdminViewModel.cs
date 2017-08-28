using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FirstEvent.Model;
using FirstEvent.View;
using FirstEvent.ViewModel.Sections;
using GalaSoft.MvvmLight.Command;

namespace FirstEvent.ViewModel
{
    public class MainAdminViewModel
    {
        public Caller Caller { get; set; }

        public GeneralInfo GeneralInfo { get; set; }

        public Membership Membership { get; set; }

        public Subscriber Subscriber { get; set; }

        public CallInfo CallInfo { get; set; }

        public TreatDoctor TreatDoctor { get; set; }

        public ContactSection ContactSection { get; set; }

        public MainAdminViewModel(Caller c, GeneralInfo g, Membership m, Subscriber s, CallInfo cl, TreatDoctor td, ContactSection cs)
        {
            Caller = c;
            GeneralInfo = g;
            Membership = m;
            Subscriber = s;
            CallInfo = cl;
            TreatDoctor = td;
            ContactSection = cs;

            DeleteFeCommand = new RelayCommand<Window>(DeleteFeExecute);
        }

        public ICommand CancelButtonCommand {
            get { return new RelayCommand(Application.Current.Shutdown, () => true); }
        }

        public RelayCommand<Window> DeleteFeCommand { get; set; }

        private void DeleteFeExecute(Window win)
        {
            DataBaseManager.DeleteFirstCallByDocTime(GeneralInfo.DocDateTime.ToString(CultureInfo.CurrentCulture));
            new FEList().Show();
            win.Close();
        }

    }
}
