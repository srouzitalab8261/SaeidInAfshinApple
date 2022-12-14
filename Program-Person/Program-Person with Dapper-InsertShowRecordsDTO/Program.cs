using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Program_Person
{
    class Program
    {
        static void Main(string[] args)
        {

            var Dr1 = new Doctor("ALi", "Tim", 36, "Dermatologist");

            var Dr2 = new Doctor("Abbas", "Talari", 46, Specialities.Dentist);


            //==============

            var cs = @"Server=AFSHINTEYMO7572\SQLEXPRESS;Database=HealthDB;Trusted_Connection=True;";

            using var con = new SqlConnection(cs);
            con.Open();

            //var query = "INSERT INTO cars(name, price) VALUES(@name, @price)";
            var query = "INSERT INTO Doctor1(name, family, age, speciality) VALUES(@name, @family, @age, @speciality)";

            var dp = new DynamicParameters();
            dp.Add("@name", Dr2.Name, DbType.AnsiString, ParameterDirection.Input, 255);
            dp.Add("@family", Dr2.Family, DbType.AnsiString, ParameterDirection.Input, 255);
            dp.Add("@age", Dr2.Age);
            dp.Add("@speciality", Dr2.Speciality, DbType.AnsiString, ParameterDirection.Input, 255);

            //dp.Add("@name", Dr1.Name, DbType.AnsiString, ParameterDirection.Input, 255);
            //dp.Add("@family", Dr1.Family, DbType.AnsiString, ParameterDirection.Input, 255);
            //dp.Add("@age", Dr1.Age);
            //dp.Add("@speciality", Dr1.Speciality.ToString(), DbType.AnsiString, ParameterDirection.Input, 255);

            //dp.Add("@name", "Saeid", DbType.AnsiString, ParameterDirection.Input, 255);
            //dp.Add("@family", "Rouzitalab", DbType.AnsiString, ParameterDirection.Input, 255);
            //dp.Add("@age", 39);
            //dp.Add("@speciality", "Dentist", DbType.AnsiString, ParameterDirection.Input, 255);
            int res = con.Execute(query, dp);

            if (res > 0)
            {
                Console.WriteLine("row inserted");
            }


            // Show all records of Doctor1 table

            var DoctorsDto = con.Query<DoctorDto>("SELECT * FROM Doctor1").ToList();

            //DoctorsDto.ForEach(Doctor => Console.WriteLine(Doctor));
            //var doctors = new List<Doctor>() {
            //    new Doctor(){ Id = 1, Name="Bill"},
            //    new Doctor(){ Id = 2, Name="Steve"},
            //    new Doctor(){ Id = 3, Name="Ram"},
            //    new Doctor(){ Id = 4, Name="Abdul"}
            //};

            var doctors = new List<Doctor>();

            foreach (var item in DoctorsDto)
            {
                var d = new Doctor();
                d.Name = item.Name;
                d.Family = item.Family;
                d.Age = item.Age;
                d.Speciality = Enum.Parse<Specialities>(item.Speciality, true);
                d.Id = item.Id;
                doctors.Add(d);
            }
            doctors.ForEach((i) => { Console.WriteLine(i.ToString()); });

            Console.ReadLine();
        }

    }
}


