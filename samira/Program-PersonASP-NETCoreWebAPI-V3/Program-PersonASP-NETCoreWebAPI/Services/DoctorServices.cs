using System;
using System.Collections.Generic;
using System.Linq;

namespace Program_PersonASP_NETCoreWebAPI.Services
{
    public interface IValidator
    {
        bool Validate(string name);
    }
    public class NameValidator : IValidator
    {
        public bool Validate(string name)
        {
            if (name.Length > 10)
                return false;
            return true;
        }
    }
    public class DoctorServices
    {
        List<Doctor> _repo { get; }
        private readonly IValidator _validator;

        //public DoctorServices(IValidator validator) :
        //    this(validator, new List<Doctor> {
        //        new Doctor("ALi", "Rezayi", 36, "Dermatologist"),
        //        new Doctor("Saeid", "Rouzitalab", 38, "Dentist",1),
        //        new Doctor("Samira", "Rouzitalab", 34, Specialities.Dentist)
        //    })
        //{

        //}

        public DoctorServices(IValidator validator) : this(validator, null)
        {
            _repo = new List<Doctor> {
                new Doctor("ALi", "Rezayi", 36, "Dermatologist"),
                new Doctor("Saeid", "Rouzitalab", 38, "Dentist",1),
                new Doctor("Samira", "Rouzitalab", 34, Specialities.Dentist)
            };
        }
        public DoctorServices(IValidator validate, List<Doctor> doctors)
        {
            if (doctors == null)
            {
                _repo = new List<Doctor>();
            }
            _repo = doctors;
            _validator = validate;

        }
        public List<Doctor> GetAll()
        {
            var validList = new List<Doctor>();
             
            foreach (var item in _repo)
            {
                if (_validator.Validate(item.Name))
                    validList.Add(item);
            }

            
            return validList;
        }
        public IEnumerable<Doctor> GetValidDoctors()
        {
            var validList2 =
                 _repo.Select(x => _validator.Validate(x.Name) ? x :
                 new Doctor
                 {
                     Name = x.Name.Substring(0, 10),
                     Age = x.Age,
                     Family = x.Family,
                     Id = x.Id,
                     Speciality = x.Speciality
                 }
             );
            return validList2;
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

        public void ModifyDoctor2(int id, Doctor doctor)
        {
            var doc = new Doctor();
            doc = _repo.FirstOrDefault(x => x.Id == id);
            doc.Id = doctor.Id;
            doc.Name = doctor.Name;
            doc.Name = doctor.Name;
            doc.Family = doctor.Family;
            doc.Age = doctor.Age;
            doc.Speciality = doctor.Speciality;
        }

        //public void DeleteDoctor(int id)
        //{
        //    var doc = new Doctor();
        //    doc = _repo.FirstOrDefault(x => x.Id == id);

        //        _repo.Remove(doc);
        //}

        public long DeleteDoctor(int id)
        {
            var doc = _repo.FirstOrDefault(x => x.Id == id);
            if (doc == null)
                return 0;
            _repo.Remove(doc);
            return 1;
        }

        public void DeleteDoctor_Old(int id)
        {
            var doc = _repo.FirstOrDefault(x => x.Id == id);
            if (doc == null)
                throw new Exception("NotFound");

            _repo.Remove(doc);
        }
        public void DeleteDoctor(Doctor doctor)
        {
            //var doc = new Doctor();
            //doc = _repo.FirstOrDefault(x => x.Id == id);

            _repo.Remove(doctor);
        }

    }
}





