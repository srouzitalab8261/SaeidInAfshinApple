using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retrive_data_from_sql_with_dapper
{
    class Doctor
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public int Age  { get; set; }
        public string Speciality { get; set; }


        public override string ToString()
        {
            return $"{Id} {Name} {Family} {Age} {Speciality}";
        }

    }
}
