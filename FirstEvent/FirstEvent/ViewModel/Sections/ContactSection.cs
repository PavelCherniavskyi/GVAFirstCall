using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FirstEvent.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FirstEvent.ViewModel.Sections
{
    public class ContactSection : ViewModelBase
    {
        public int SelectedIndex { get; set; }

        public ContactSection()
        {
            ContactViewInForm = new ObservableCollection<ContactViewInForm>();
        }
        public ObservableCollection<ContactViewInForm> ContactViewInForm { get; set; }

        public ObservableCollection<TypeOfContact> TypeOfContacts => DataBaseManager.TypeOfContacts;

        public static ObservableCollection<RelToSub> RelToSubs => DataBaseManager.AllRelToSubs;

        public ICommand AddButtonCommand
        {
            get
            {
                return new RelayCommand(AddButtonCommandExecute, () => true);
            }
        }

        private void AddButtonCommandExecute()
        {
            ContactViewInForm.Add(new ContactViewInForm());
        }

        public ICommand DeleteButtonCommand
        {
            get
            {
                return new RelayCommand(DeleteButtonCommandExecute, () => true);
            }
        }

        private void DeleteButtonCommandExecute()
        {
            if (!ContactViewInForm.Any())
                return;
            ContactViewInForm.RemoveAt(SelectedIndex);
        }

        public ICommand CopyButtonCommand
        {
            get
            {
                return new RelayCommand(CopyButtonCommandExecute, () => true);
            }
        }

        private void CopyButtonCommandExecute()
        {
            if (!ContactViewInForm.Any())
                return;
            if (SelectedIndex == ContactViewInForm.Count - 1)
            {
                ContactViewInForm.Add((ContactViewInForm)ContactViewInForm[SelectedIndex].Clone());
            }
            else
            {
                ContactViewInForm.Insert(SelectedIndex + 1, (ContactViewInForm)ContactViewInForm[SelectedIndex].Clone());
            }
            
        }
    }
}
