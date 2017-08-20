using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public class DbHandler : DbContext
    {
        public DbHandler():base("DefaultConnection")
        {
        }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<RelToSub> RelToSubs {get; set; }

        public DbSet<DocView> DocViews { get; set; }
    }
}
