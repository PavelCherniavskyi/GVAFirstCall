using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FirstEvent.Model
{
    public static class DataBaseManager
    {
        private static readonly DbHandler Db;

        public static ObservableCollection<Doctor> Doctors => Db.Doctors.Local;

        public static ObservableCollection<Country> Countries => Db.Countries.Local;

        public static ObservableCollection<Region> Regions => Db.Regions.Local;

        public static ObservableCollection<City> Cities => Db.Cities.Local;

        public static ObservableCollection<RelationToSubscriber> RelationsToSubscriber => Db.RelationsToSubscriber.Local;

        static DataBaseManager()
        {
            try
            {
                Db = new DbHandler();
                Db.Doctors.Load();
                Db.Cities.Load();
                Db.Regions.Load();
                Db.Countries.Load();
                //Db.RelationsToSubscriber.Load();
            }
            catch (Exception e)
            {
                MessageBox.Show("Problems with DB \n" + e.InnerException);
            }
            

        }

        public static Country GetCountryByRegion(Region r)
        {
            var query = from c in Db.Countries
                where r.CountryId == c.Id
                select c;
            return query.First();
        }

        public static void GetCountryAndRegionByCity(City city, out Region region, out Country country)
        {
            var queryCountry = from c in Db.Countries
                where c.Id == city.CountryId
                select c;
            country = queryCountry.First();

            var queryRegion = from r in Db.Regions
                               where r.Id == city.RegionId
                               select r;
            region = queryRegion.First();
        }

        public static void AddDoctor(Doctor d)
        {
            Doctors.Add(d);
            Db.Doctors.AddRange(Doctors);
            Db.SaveChanges();
        }

        public static void AddCountry(Country c)
        {
            Countries.Add(c);
            Db.Countries.AddRange(Countries);
            Db.SaveChanges();
        }

        public static void AddRegion(Region r)
        {
            Regions.Add(r);
            Db.Regions.AddRange(Regions);
            Db.SaveChanges();
        }

        public static void AddCity(City c)
        {
            Cities.Add(c);
            Db.Cities.AddRange(Cities);
            Db.SaveChanges();
        }

        public static void Dispose()
        {
            Db.Dispose();
        }

    }
}
