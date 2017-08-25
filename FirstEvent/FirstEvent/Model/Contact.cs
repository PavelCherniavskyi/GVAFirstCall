using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FirstEvent.Annotations;

namespace FirstEvent.Model
{
    public class Contact
    {
        public int Id { get; set; }

        public int? FirstCallId { get; set; }

        public int? RelToSubId { get; set; }

        public int? TypeOfContactId { get; set; }

        public string Name { get; set; }

        public string ContactNum { get; set; }

        public string Info { get; set; }

    }

    public class ContactViewInForm : ICloneable
    {
        public ContactViewInForm()
        {
            SelectedContact = new TypeOfContact();
            SelectedRelToSub = new RelToSub();
        }

        private static int _idCount;

        public RelToSub SelectedRelToSub { get; set; }

        public TypeOfContact SelectedContact { get; set; }

        public int Id => ++_idCount;

        public string Name { get; set; }

        public string ContactNum { get; set; }

        public string Info { get; set; }
        public object Clone()
        {
            _idCount++;
            var newContact = new ContactViewInForm
            {
                SelectedContact = new TypeOfContact(),
                SelectedRelToSub = new RelToSub(),
                Name = Name,
                Info = Info,
                ContactNum = ContactNum
            };

            return newContact;
        }
    }
}
