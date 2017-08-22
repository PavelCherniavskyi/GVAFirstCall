using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public class Hotel
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public int CountryId { get; set; }

        public int CityId { get; set; }
    }

    public class HotelView
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string City { get; set; }

        public string Country { get; set; }
        
    }
}
