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
        private readonly DataGridView _dataGridView = new DataGridView();

        public Caller Caller { get; set; }

        public GeneralInfo GeneralInfo { get; set; }

        public  Membership Membership { get; set; }

        public Subscriber Subscriber { get; set; }

        public CallInfo CallInfo { get; set; }

        public TreatDoctor TreatDoctor { get; set; }

        public MainWindowViewModel()
        {
            Caller = new Caller();
            GeneralInfo = new GeneralInfo();
            Membership = new Membership();
            Subscriber = new Subscriber();
            CallInfo = new CallInfo();
            TreatDoctor = new TreatDoctor();
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
