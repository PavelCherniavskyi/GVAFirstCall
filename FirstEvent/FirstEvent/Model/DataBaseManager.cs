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

        public static ObservableCollection<Doctor> Doctors { get; private set; }

        public static ObservableCollection<Country> Countries { get; private set; }

        public static ObservableCollection<Region> Regions { get; private set; }

        public static ObservableCollection<City> Cities { get; private set; }

        public static ObservableCollection<RelToSub> RelToSubs { get; private set; }

        public static ObservableCollection<DocView> DocViews { get; private set; }

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

                Doctors = Db.Doctors.Local;
                Countries = Db.Countries.Local;
                Regions = Db.Regions.Local;
                Cities = Db.Cities.Local;
                RelToSubs = Db.RelToSubs.Local;
                DocViews = Db.DocViews.Local;
            }
            catch (Exception e)
            {
                MessageBox.Show("Problems with DB \nDetails: " + e.InnerException);
            }


            //_mDbConnection = new SQLiteConnection("Data Source=.\\Model\\DataBase.sqlite; Version=3;");
            //_mDbConnection.Open();

            //string sql = "SELECT * FROM Doctors";
            //SQLiteCommand command = new SQLiteCommand(sql, _mDbConnection);
            //SQLiteDataReader reader = command.ExecuteReader();
            //var a = reader["Id"];

        }

        public static Country GetCountryByRegion(Region r)
        {
            var query = from c in Countries
                where r.CountryId == c.Id
                select c;
            return query.First();
        }

        public static void GetCountryAndRegionByCity(City city, out Region region, out Country country)
        {
            var queryCountry = from c in Countries
                where c.Id == city.CountryId
                select c;
            country = queryCountry.First();

            var queryRegion = from r in Regions
                               where r.Id == city.RegionId
                               select r;
            region = queryRegion.First();
        }

        public static void AddDoctor(Doctor d)
        {
            Doctors.Add(d);
            Db.Doctors.Add(d);
            Db.SaveChanges();
        }

        public static void AddCountry(Country c)
        {
            Countries.Add(c);
            Db.Countries.Add(c);
            Db.SaveChanges();
        }

        public static void AddRegion(Region r)
        {
            Regions.Add(r);
            Db.Regions.Add(r);
            Db.SaveChanges();
        }

        public static void AddCity(City c)
        {
            Cities.Add(c);
            Db.Cities.Add(c);
            Db.SaveChanges();
        }
    }
}
