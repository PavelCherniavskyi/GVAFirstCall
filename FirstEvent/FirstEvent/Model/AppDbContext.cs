using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext():base("DefaultConnection")
        {
        }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<RelToSub> RelToSubs {get; set; }

        public DbSet<DocView> DocViews { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<HotelView> HotelViews { get; set; }

        public DbSet<Insurance> Insurances { set; get; }

        public DbSet<Office> Offices { set; get; }

        public  DbSet<InsuranceView> InsuranceViews { set; get; }

        public DbSet<StatusOfCall> StatusOfCalls { set; get; }

        public DbSet<TreatingDoctor> TreatingDocs { get; set; }

        public DbSet<TreatingDoctorView> TreatDocViews { get; set; }

        public DbSet<TypeOfContact> TypeOfContacts { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<FirstCall> FirstCalls { get; set; }

        public DbSet<Password> Passwords { get; set; }

    }
}
