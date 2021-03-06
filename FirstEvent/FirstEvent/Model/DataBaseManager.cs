﻿using System;
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
using System.Windows.Media.Animation;
using FirstEvent.View;
using FirstEvent.ViewModel.Sections;

namespace FirstEvent.Model
{
    public static class DataBaseManager
    {
        private static readonly AppDbContext AppDb;
        private static bool _isLoaded;


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

        public static ObservableCollection<TypeOfContact> TypeOfContacts { get; private set; }

        public static ObservableCollection<Contact> Contacts { get; private set; }

        public static ObservableCollection<FirstCall> FirstCalls { get; private set; }

        public static ObservableCollection<Password> Passwords { get; private set; }

        static DataBaseManager()
        {
            AppDb = new AppDbContext();
        }

        public static void Initialize()
        {
            if (_isLoaded)
                return;
            try
            {
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
                AppDb.TypeOfContacts.Load();
                AppDb.Contacts.Load();
                AppDb.FirstCalls.Load();
                AppDb.Passwords.Load();

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
                TypeOfContacts = AppDb.TypeOfContacts.Local;
                Contacts = AppDb.Contacts.Local;
                FirstCalls = AppDb.FirstCalls.Local;
                Passwords = AppDb.Passwords.Local;

                _isLoaded = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Problems with DB \nMessage: " + e.Message + '\n' + "Inner message: " + e.InnerException);
            }
        }

        public static void AddFirstCall(FirstCall f)
        {
            try
            {
                AppDb.FirstCalls.Add(f);
                AppDb.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + '\n' + e.InnerException);
            }
            
        }

        public static Password GetPassByName(string name)
        {
            var query = from p in Passwords
                        where p.Name == name
                        select p;
            return query.First();
        }

        public static FirstCall SearchfFirstCall(FirstCall firstCall)
        {
            AppDb.FirstCalls.Load();
            FirstCalls = AppDb.FirstCalls.Local;

            var query = from f in FirstCalls
                        where f.DocDateTime == firstCall.DocDateTime
                        select f;
            return query.First();
        }

        public static void DeleteFirstCallByDocTime(string docTime)
        {
            try
            {
                var query = from f in FirstCalls
                            where f.DocDateTime == docTime
                            select f;
                AppDb.FirstCalls.Remove(query.First());
                AppDb.SaveChanges();
                FirstCalls = AppDb.FirstCalls.Local;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + '\n' + e.InnerException);
            }
            
        }

        public static void AddContacts(IEnumerable<Contact> c)
        {
            try
            {
                AppDb.Contacts.AddRange(c);
                AppDb.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + '\n' + e.InnerException);
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

        public static Doctor GetDoctorById(int? id)
        {
            if(id == 0)
                return new Doctor();

            var query = from d in AllDoctors
                        where d.Id == id
                        select d;

            return query.First();
        }

        public static Country GetCountryById(int? id)
        {
            if (id == 0)
                return new Country();

            var query = from c in AllCountries
                        where c.Id == id
                        select c;

            return query.First();
        }

        public static Region GetRegionById(int? id)
        {
            if (id == 0)
                return new Region();

            var query = from r in AllRegions
                        where r.Id == id
                        select r;

            return query.First();
        }

        public static City GetCityById(int? id)
        {
            if (id == 0)
                return new City();

            var query = from c in AllCities
                        where c.Id == id
                        select c;

            return query.First();
        }

        public static HotelView GetHotelViewById(int? id)
        {
            if (id == 0)
                return new HotelView();

            var query = from h in HotelViews
                        where h.Id == id
                        select h;

            return query.First();
        }

        public static TreatingDoctorView GetTreatDocById(int? id)
        {
            if (id == 0)
                return new TreatingDoctorView();

            var query = from t in TreatDocViews
                        where t.Id == id
                        select t;

            return query.First();
        }

        public static RelToSub GetRelToSubById(int? id)
        {
            if (id == 0)
                return new RelToSub();

            var query = from r in AllRelToSubs
                        where r.Id == id
                        select r;

            return query.First();
        }

        public static InsuranceView GetInsuranceViewById(int? id)
        {
            if (id == 0)
                return new InsuranceView();

            var query = from i in InsuranceViews
                        where i.Id == id
                        select i;

            return query.First();
        }

        public static StatusOfCall GetStatusOfCallById(int? id)
        {
            if (id == 0)
                return new StatusOfCall();

            var query = from s in StatusOfCalls
                        where s.Id == id
                        select s;

            return query.First();
        }

        public static TypeOfContact GetTypeOfContactById(int? id)
        {
            if (id == 0)
                return new TypeOfContact();

            var query = from c in TypeOfContacts
                        where c.Id == id
                        select c;

            return query.First();
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

        public static void RemoveFirstCall(FirstCall fc)
        {
            FirstCalls.Remove(fc);
            AppDb.FirstCalls.Remove(fc);
            AppDb.SaveChanges();
        }
    }
}
