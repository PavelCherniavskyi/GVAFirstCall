using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FirstEvent.Model
{
    public static class DataBaseManager
    {
        private static readonly DbHandler Db;

        public static ObservableCollection<Doctor> AllDoctors { get; private set; }

        public static ObservableCollection<Country> AllCountries { get; private set; }

        public static ObservableCollection<Region> AllRegions { get; private set; }

        public static ObservableCollection<City> AllCities { get; private set; }

        public static ObservableCollection<RelToSub> AllRelToSubs { get; private set; }

        public static ObservableCollection<DocView> DocViews { get; private set; }

        public static ObservableCollection<Hotel> AllHotels { get; private set; }

        public static ObservableCollection<HotelView> HotelViews { get; private set; }

        static DataBaseManager()
        {
            try
            {
                Db = new DbHandler();
                Db.Doctors.Load();
                Db.Cities.Load();
                Db.Regions.Load();
                Db.Countries.Load();
                Db.RelToSubs.Load();
                Db.DocViews.Load();
                Db.Hotels.Load();
                Db.HotelViews.Load();

                AllDoctors = Db.Doctors.Local;
                AllCountries = Db.Countries.Local;
                AllRegions = Db.Regions.Local;
                AllCities = Db.Cities.Local;
                AllRelToSubs = Db.RelToSubs.Local;
                DocViews = Db.DocViews.Local;
                AllHotels = Db.Hotels.Local;
                HotelViews = Db.HotelViews.Local;
            }
            catch (Exception e)
            {
                MessageBox.Show("Problems with DB \nMessage: " + e.Message + '\n' + "Inner message: " + e.InnerException);
            }

        }

        public static Country GetCountryByRegion(Region r)
        {
            var query = from c in AllCountries
                where r.CountryId == c.Id
                select c;
            return query.First();
        }

        public static void GetCountryAndRegionByCity(City city, out Region region, out Country country)
        {
            var queryCountry = from c in AllCountries
                where c.Id == city.CountryId
                select c;
            country = queryCountry.First();

            var queryRegion = from r in AllRegions
                               where r.Id == city.RegionId
                               select r;
            region = queryRegion.First();
        }

        public static ObservableCollection<Region> GetRegionsByCountry(Country country)
        {
            var query = from r in AllRegions
                        where r.CountryId == country.Id
                        select r;

            return new ObservableCollection<Region>(query.ToList());
        }

        public static ObservableCollection<City> GetCitiesByCountry(Country country)
        {
            var query = from c in AllCities
                        where c.CountryId == country.Id
                        select c;

            return new ObservableCollection<City>(query.ToList());
        }

        public static ObservableCollection<City> GetCitiesByRegion(Region region)
        {
            var query = from r in AllCities
                        where r.RegionId == region.Id
                        select r;

            return new ObservableCollection<City>(query.ToList());
        }

        public static void AddDoctor(Doctor d)
        {
            AllDoctors.Add(d);
            Db.Doctors.Add(d);
            Db.SaveChanges();
        }

        public static void AddCountry(Country c)
        {
            AllCountries.Add(c);
            Db.Countries.Add(c);
            Db.SaveChanges();
        }

        public static void SetRegions(ObservableCollection<Region> regions)
        {
            AllRegions = regions;
        }

        public static void AddRegion(Region r)
        {
            AllRegions.Add(r);
            Db.Regions.Add(r);
            Db.SaveChanges();
        }

        public static void AddCity(City c)
        {
            AllCities.Add(c);
            Db.Cities.Add(c);
            Db.SaveChanges();
        }
    }
}
