
using System;
using System.Collections.Generic;

namespace Program_Person
{
    public class Constants
    {
        public const int Today = 256;
    }
    public enum Role
    {
        Doctor,
        Nurse,
        Staff,
        Admin
    }
    public interface IEmployment
    {
        public decimal Calculate();
        string FullName { get; }
        Role Role { get; set; }

    }

    public class ContractEmployment : IEmployment
    {
        public long EmployeeId { get; set; }
        public int StartDate { get; set; }
        public decimal Rate { get; set; }
        public Doctor Doctor { get; set; }
        public Nurse Nurse { get; set; }
        public Role Role { get; set; }
        public string FullName
        {
            get
            {
                switch (Role)
                {
                    case Role.Doctor:
                        {
                            return "Contract employee: " + Role.ToString() + ", " + Doctor.Name + " " + Doctor.Family;
                        }
                    case Role.Nurse:
                        {
                            return "Contract employee: " + Role.ToString() + ", " + Nurse.Name + " " + Nurse.Family;
                        }
                    default:
                        {
                            throw new System.Exception("The role is not valid");
                        }
                }
            }
        }

        public decimal Calculate()
        {
            var days = Constants.Today - StartDate;
            return (days * Rate) * 1.07m;
        }

    }
    public class PermanentEmployment : IEmployment
    {
        public long EmployeeId { get; set; }
        public int StartDate { get; set; }
        public decimal Rate { get; set; }
        public Doctor Doctor { get; set; }
        public Nurse Nurse { get; set; }

        public string FullName
        {
            get
            {
                switch (Role)
                {
                    case Role.Doctor:
                        {
                            return "Permanet employee: " + Role.ToString() + ", " + Doctor.Name + " " + Doctor.Family;
                        }
                    case Role.Nurse:
                        {
                            return "Permanet employee: " + Role.ToString() + ", " + Nurse.Name + " " + Nurse.Family;
                        }
                    default:
                        {
                            throw new System.Exception("The role is not valid");
                        }
                }
            }
        }


        public Role Role { get; set; }

        public decimal Calculate()
        {
            var days = Constants.Today - StartDate;
            return days * Rate;
        }

    }

    public class CasualEmployment : IEmployment
    {
        public long EmployeeId { get; set; }
        public int StartDate { get; set; }
        public decimal Rate { get; set; }
        public Doctor Doctor { get; set; }
        public Nurse Nurse { get; set; }


        public string FullName
        {
            get
            {
                switch (Role)
                {
                    case Role.Doctor:
                        {
                            return "Casual employee: " + Role.ToString() + ", " + Doctor.Name + " " + Doctor.Family;
                        }
                    case Role.Nurse:
                        {
                            return "Casual employee: " + Role.ToString() + ", " + Nurse.Name + " " + Nurse.Family;
                        }
                    default:
                        {
                            throw new System.Exception("The role is not valid");
                        }
                }
            }
        }


        public Role Role { get; set; }

        public decimal Calculate()
        {
            var days = Constants.Today - StartDate;
            return days * Rate;
        }

    }

    public class SalaryCalculator
    {
        public const decimal Bounes = 1.1m;
        public decimal CalculateAll(IEmployment employee)
        {
            return Bounes * employee.Calculate();
        }
        public string PrintSalary(IEmployment employment)
        {
            return employment.FullName + " Salary: " + CalculateAll(employment).ToString();
        }
    }

    public class RoleCall
    {
        public static void PrintStaff(List<IEmployment> employees)
        {
          
           
            foreach (var item in employees)
            {
                   Console.WriteLine(item.FullName);
            }
        }
        public static void PrintStaff(List<IEmployment> employees, int i)
        {

                Console.WriteLine(employees[i].FullName);
        }
        public static void PrintStaff(IEmployment employment)
        {

            Console.WriteLine(employment.FullName);
        }
    }
}
           




        
    

    


