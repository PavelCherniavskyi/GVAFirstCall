using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public static class DataBaseManager
    {
        private static readonly DbHandler db;

        public static ObservableCollection<Doctor> Doctors { get; }

        public static ObservableCollection<Country> Countries { get; }

        public static ObservableCollection<Region> Regions { get; }

        public static ObservableCollection<City> Cities { get; }

        public static ObservableCollection<RelationToSubscr> RelationsToSubscr { get; }

        static DataBaseManager()
        {
            using (db = new DbHandler())
            {
                db.Doctors.Load();
                db.Cities.Load();
                db.Countries.Load();
                db.Regions.Load();

                Doctors = db.Doctors.Local;
                Doctors = db.Doctors.Local;
                Countries = db.Countries.Local;
                Regions = db.Regions.Local;
                Cities = db.Cities.Local;
            }
            

            

            RelationsToSubscr = GetRelationsToSubscriber();
        }

        private static ObservableCollection<RelationToSubscr> GetRelationsToSubscriber()
        {
            ObservableCollection<RelationToSubscr> relations = new ObservableCollection<RelationToSubscr>();
            relations.Add(new RelationToSubscr() {Id = 1, Relation = "Caller"});
            relations.Add(new RelationToSubscr() { Id = 2, Relation = "Subcriber" });
            relations.Add(new RelationToSubscr() { Id = 3, Relation = "Treating doctor" });
            relations.Add(new RelationToSubscr() { Id = 4, Relation = "Travel Agency" });

            return relations;
        }

        
    }
}
