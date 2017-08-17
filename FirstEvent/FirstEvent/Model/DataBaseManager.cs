using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public static class DataBaseManager
    {
        public static ObservableCollection<Doctor> Doctors { get; }

        public static ObservableCollection<RelationToSubscr> RelationsToSubscr { get; }

        static DataBaseManager()
        {
            RelationsToSubscr = GetRelationsToSubscriber();
            Doctors = GetDoctorRepository();
        }

        private static ObservableCollection<Doctor> GetDoctorRepository()
        {
            ObservableCollection<Doctor> doctors = new ObservableCollection<Doctor>();
            doctors.Add(new Doctor() {Id = 1, FullName = "Lavrova Kseniya", City = "Odessa", Country = "Ukraine"});
            doctors.Add(new Doctor() { Id = 2, FullName = "Andrievskyi Andrey", City = "Odessa", Country = "Ukraine" });
            doctors.Add(new Doctor() { Id = 3, FullName = "Agalakov Sergey", City = "Odessa", Country = "Ukraine" });
            return doctors;
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
