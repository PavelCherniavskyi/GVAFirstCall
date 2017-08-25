

namespace FirstEvent.Model
{
    public class Doctor
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int? CountryId { get; set; }

        public int? CityId { get; set; }
    }

    public class DocView
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
