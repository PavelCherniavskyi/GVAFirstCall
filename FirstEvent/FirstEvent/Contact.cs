using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent
{
    public enum TypeOfContact
    {
        TelephoneB,
        TelephoneH,
        Mobile,
        FaxB,
        FaxH,
        Email
    }
    public class Contact
    {
        public string Id { get; set; }

        public string RelationToSubscr { get; set; }

        public string Name { get; set; }

        public ObservableCollection<TypeOfContact> TypeOfContact { get; set; }

        public string ContactNumber { get; set; }

        public string Info { get; set; }

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
            { ContactNumber = contactNumber, Id = _idCount.ToString(), Info = info, Name = name, RelationToSubscr = relationToSubscr,
                TypeOfContact = new ObservableCollection<TypeOfContact>() {TypeOfContact.TelephoneB, TypeOfContact.TelephoneH, TypeOfContact.Email, TypeOfContact.FaxB, TypeOfContact.FaxH, TypeOfContact.Mobile} });
            return _contacts;
        }
    }
}
