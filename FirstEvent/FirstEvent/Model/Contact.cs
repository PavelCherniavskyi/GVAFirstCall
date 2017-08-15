using System.Collections.ObjectModel;

namespace FirstEvent.Model
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
}
