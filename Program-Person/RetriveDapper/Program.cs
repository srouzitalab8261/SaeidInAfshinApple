using System;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Retrive_data_from_sql_with_dapper
{
    class Program
    {
        static void Main(string[] args)
        {

            var cs = @"Server=AFSHINTEYMO7572\SQLEXPRESS;Database=HealthDB;Trusted_Connection=True;";

            using var con = new SqlConnection(cs);
            con.Open();

            var Doctors = con.Query<Doctor>("SELECT * FROM Doctor1").ToList();

            Doctors.ForEach(Doctor => Console.WriteLine(Doctor));

        }
    }
}
