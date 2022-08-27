using System;
using System.Collections.Generic;
using System.Linq;

namespace Program_PersonASP_NETCoreWebAPI.Services
{
    public class DoctorServices
    {
        List<Doctor> _repo { get; }
        public DoctorServices() 
        {
            _repo = new List<Doctor> {
                new Doctor("ALi", "Rezayi", 36, "Dermatologist"),
                new Doctor("Saeid", "Rouzitalab", 38, "Dentist",1),
                new Doctor("Samira", "Rouzitalab", 34, Specialities.Dentist)
            };

        }
        public List<Doctor> GetAll()
        {
            return _repo;
        }
        public Doctor GetByName(string name)
        {
            var doctors = GetAll();
            var result = doctors.FirstOrDefault(x => x.Name == name);
            return result;
        }
        public IEnumerable<Doctor> GetOldest()
        {
            var doctors = GetAll();
            var age = doctors.Max(x => x.Age);
            var result = doctors.Where(x => x.Age == age);
            return result;
        }

        public IEnumerable<Doctor> GetDoctorbySpeciality(string specialist)
        {
            var doctors = GetAll();
            var result = doctors.Where(x => x.Speciality == (Specialities)Enum.Parse(typeof(Specialities), specialist, true));
            //var result = doctors.Where(x => x.Speciality == specialist);
            //var result = doctors.FirstOrDefault(x => x.Name == name);

            return result;
        }

        public void AddNew(Doctor doctor)
        {
            _repo.Add(doctor);
        }

        public void ModifyDoctor(Doctor doctor)
        {
            var doc = new Doctor();
            var id = doctor.Id;
            doc = _repo.FirstOrDefault(x => x.Id == id);
            doc.Name = doctor.Name;
            doc.Family = doctor.Family;
            doc.Age = doctor.Age;
            doc.Speciality = doctor.Speciality;
        }
    }
}




