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
        private static readonly AppDbContext AppDb;

        public static ObservableCollection<Doctor> AllDoctors { get; private set; }

        public static ObservableCollection<Country> AllCountries { get; private set; }

        public static ObservableCollection<Region> AllRegions { get; private set; }

        public static ObservableCollection<City> AllCities { get; private set; }

        public static ObservableCollection<RelToSub> AllRelToSubs { get; private set; }

        public static ObservableCollection<DocView> DocViews { get; private set; }

        public static ObservableCollection<Hotel> AllHotels { get; private set; }

        public static ObservableCollection<HotelView> HotelViews { get; private set; }

        public static ObservableCollection<Insurance> AllInsurances { get; private set; }

        public static ObservableCollection<InsuranceView> InsuranceViews { get; private set; }

        public static ObservableCollection<Office> AllOffices { get; private set; }

        public static ObservableCollection<StatusOfCall> StatusOfCalls { get; private set; }

        public static ObservableCollection<TreatingDoctor> TreatingDocs { get; private set; }

        public static ObservableCollection<TreatingDoctorView> TreatDocViews { get; private set; }

        static DataBaseManager()
        {
            try
            {
                AppDb = new AppDbContext();
                AppDb.Doctors.Load();
                AppDb.Cities.Load();
                AppDb.Regions.Load();
                AppDb.Countries.Load();
                AppDb.RelToSubs.Load();
                AppDb.DocViews.Load();
                AppDb.Hotels.Load();
                AppDb.HotelViews.Load();
                AppDb.Insurances.Load();
                AppDb.InsuranceViews.Load();
                AppDb.Offices.Load();
                AppDb.StatusOfCalls.Load();
                AppDb.TreatingDocs.Load();
                AppDb.TreatDocViews.Load();

                AllDoctors = AppDb.Doctors.Local;
                AllCountries = AppDb.Countries.Local;
                AllRegions = AppDb.Regions.Local;
                AllCities = AppDb.Cities.Local;
                AllRelToSubs = AppDb.RelToSubs.Local;
                DocViews = AppDb.DocViews.Local;
                AllHotels = AppDb.Hotels.Local;
                HotelViews = AppDb.HotelViews.Local;
                AllInsurances = AppDb.Insurances.Local;
                InsuranceViews = AppDb.InsuranceViews.Local;
                AllOffices = AppDb.Offices.Local;
                StatusOfCalls = AppDb.StatusOfCalls.Local;
                TreatingDocs = AppDb.TreatingDocs.Local;
                TreatDocViews = AppDb.TreatDocViews.Local;
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

        public static ObservableCollection<InsuranceView> GetInsurancesByOffice(Office o)
        {
            var query = from i in AllInsurances
                join f in AllOffices on i.OfficeId equals f.Id
                where i.OfficeId == o.Id
                select new InsuranceView() {Id = i.Id, Name = i.Name, Office = f.Name};

            return new ObservableCollection<InsuranceView>(query.ToList());
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
            AppDb.Doctors.Add(d);
            AppDb.SaveChanges();
        }

        public static void AddCountry(Country c)
        {
            AllCountries.Add(c);
            AppDb.Countries.Add(c);
            AppDb.SaveChanges();
        }

        public static void SetRegions(ObservableCollection<Region> regions)
        {
            AllRegions = regions;
        }

        public static void AddRegion(Region r)
        {
            AllRegions.Add(r);
            AppDb.Regions.Add(r);
            AppDb.SaveChanges();
        }

        public static void AddCity(City c)
        {
            AllCities.Add(c);
            AppDb.Cities.Add(c);
            AppDb.SaveChanges();
        }
    }
}
