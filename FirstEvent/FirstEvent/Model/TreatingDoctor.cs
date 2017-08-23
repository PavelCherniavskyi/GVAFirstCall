using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public class TreatingDoctor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }
    }

    public class TreatingDoctorView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
