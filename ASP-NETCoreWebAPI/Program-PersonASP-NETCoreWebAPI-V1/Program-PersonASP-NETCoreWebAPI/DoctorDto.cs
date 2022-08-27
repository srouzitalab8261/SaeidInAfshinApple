using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_PersonASP_NETCoreWebAPI
{
    public class DoctorDto
    {
        public string Speciality { get; }
        public string Name { get; set; }
        public string Family { get; set; }
        public int Age { get; set; }
        public long  Id { get; set; }
    }
}
