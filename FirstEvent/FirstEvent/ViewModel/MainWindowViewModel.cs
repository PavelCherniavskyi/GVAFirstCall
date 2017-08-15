using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using FirstEvent.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FirstEvent.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly DataGridView _dataGridView = new DataGridView();
        private RelayCommand _showDocListWindowCommand;
        private RelayCommand _cancelDocTextFieldCommand;

        public MainWindowViewModel()
        {
            Messenger.Default.Register(this, new Action<Doctor>(DoctorField));
        }

        private void DoctorField(Doctor doc)
        {
            if (doc != null)
            {
                DutyDoctor = doc.FullName;
                IsDutyDocReadOnly = true;
                RaisePropertyChanged("IsDutyDocReadOnly");
                RaisePropertyChanged("DutyDoctor");
            }
            Messenger.Default.Send<string>("doctorsListHide");

        }

        private void ContactsAddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //DataContext = _dataGridView.AddContact("Mother", "Petya", "456", "he is good");
        }

        private void ContactsCopyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //var context = DataContext as ObservableCollection<Contact>;
            //if (context == null)
            //    return;
            //if (DataGridContacts.SelectedIndex == context.Count - 1)
            //{
            //    context.Add(context[DataGridContacts.SelectedIndex]);
            //}
            //else
            //{
            //    context.Insert(DataGridContacts.SelectedIndex + 1, context[DataGridContacts.SelectedIndex]);
            //}

            //DataContext = context;
        }

        private void ContactsDeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //var context = DataContext as ObservableCollection<Contact>;
            //if (context == null)
            //    return;
            //context.RemoveAt(DataGridContacts.SelectedIndex);
            //DataContext = context;
        }

        public ICommand ShowDocListWindow
        {
            get {
                return _showDocListWindowCommand ?? (_showDocListWindowCommand = new RelayCommand(() => Messenger.Default.Send<string>("doctorsListShow"), () => true));
            }
        }

        public ICommand CancelDocTextField
        {
            get
            {
                return _cancelDocTextFieldCommand ?? (_cancelDocTextFieldCommand = new RelayCommand(ClearDocFeild, () => true));
            }
        }

        private void ClearDocFeild()
        {
            DutyDoctor = string.Empty;
            IsDutyDocReadOnly = false;
            RaisePropertyChanged("IsDutyDocReadOnly");
            RaisePropertyChanged("DutyDoctor");
        }

        public bool IsDutyDocReadOnly { get; set; }

        public string DutyDoctor { get; set; }
    }

    public class DataGridView
    {
        private readonly ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();
        private static int _idCount;

        public ObservableCollection<Contact> AddContact(string relationToSubscr, string name,
            string contactNumber, string info)
        {
            _idCount++;
            _contacts.Add(new Contact()
            {
                ContactNumber = contactNumber,
                Id = _idCount.ToString(),
                Info = info,
                Name = name,
                RelationToSubscr = relationToSubscr,
                TypeOfContact = new ObservableCollection<TypeOfContact>() { TypeOfContact.TelephoneB, TypeOfContact.TelephoneH, TypeOfContact.Email, TypeOfContact.FaxB, TypeOfContact.FaxH, TypeOfContact.Mobile }
            });
            return _contacts;
        }
    }
}
