using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public class DataBaseManager
    {
        private static ObservableCollection<Doctor> _doctors;

        public static ObservableCollection<Doctor> Doctors
        {
            get { return _doctors ?? (_doctors = GetDoctorRepository()); }
        }

        private static ObservableCollection<Doctor> GetDoctorRepository()
        {
            ObservableCollection<Doctor> doctors = new ObservableCollection<Doctor>();
            doctors.Add(new Doctor() {Id = 1, FullName = "Lavrova Kseniya", City = "Odessa", Country = "Ukraine"});
            doctors.Add(new Doctor() { Id = 2, FullName = "Andrievskyi Andrey", City = "Odessa", Country = "Ukraine" });
            doctors.Add(new Doctor() { Id = 3, FullName = "Agalakov Sergey", City = "Odessa", Country = "Ukraine" });
            return doctors;
        }
    }
}
