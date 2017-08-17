using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public struct City
    {
        public string Name;

        public City(string name)
        {
            Name = name;
        }
    }

    public struct Region
    {
        public string Name;

        public ObservableCollection<City> Cities;

        public Region(string name, ObservableCollection<City> cities )
        {
            Name = name;
            Cities = cities;
        }
    }

    public struct Country
    {
        public string Name;

        public ObservableCollection<Region> Regions;

        public ObservableCollection<City> Cities;

        public Country(string name, ObservableCollection<Region> regions, ObservableCollection<City> cities)
        {
            Name = name;
            Cities = cities;
            Regions = regions;
        }
    }
}
