using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public class City
    {
        public int Id { get; set; }

        public int RegionId { get; set; }

        public int CountryId { get; set; }

        public string Name { get; set; }

    }

    public class Region
    {
        public int Id { get; set; }

        public int CountryId { get; set; }

        public string Name { get; set; }

    }

    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
