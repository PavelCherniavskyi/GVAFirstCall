using System.Collections.ObjectModel;
using System.Windows;

namespace FirstEvent.Model
{
    public class Contact
    {
        public string Id { get; set; }

        public int FirstCallId { get; set; }

        public int RelToSubId { get; set; }

        public int TypeOfContactId { get; set; }

        public string Name { get; set; }

        public string ContactNum { get; set; }

        public string Info { get; set; }

    }

    public class ContactViewInForm
    {
        private static int _idCount;

        public RelToSub SelectedRelToSub { get; set; }

        public TypeOfContact SelectedContact { get; set; }

        public int Id => ++_idCount;

        public string Name { get; set; }

        public string ContactNum { get; set; }

        public string Info { get; set; }

    }
}
