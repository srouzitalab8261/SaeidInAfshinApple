using System.Collections.Generic;
using System.Linq;

namespace Program_PersonASP_NETCoreWebAPI.Services
{
    public class DoctorServices
    {
        public List<Doctor> GetAll()
        {
            var Dr1 = new Doctor("ALi", "Rezayi", 36, "Dermatologist");
            var Dr2 = new Doctor("Saeid", "Rouzitalab", 38, "Dentist");
            var Dr = new List<Doctor>();
            Dr.Add(Dr1);
            Dr.Add(Dr2);
            return Dr;
        }
        public Doctor GetByName(string name) 
        {
            var doctors = GetAll();
            var result = doctors.FirstOrDefault(x => x.Name == name);
            return result;
        }
    }
}
