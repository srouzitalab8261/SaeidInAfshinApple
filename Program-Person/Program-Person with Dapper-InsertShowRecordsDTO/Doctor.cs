using System;

namespace Program_Person
{
    public class Doctor
    {
        Person Person { get; }

        public Specialities Speciality { get; set; }

        public string Name { get { return Person.Name; } set { Person.Name = value; } }
        public string Family { get { return Person.Family; } set { Person.Family = value; } }
        public int Age { get { return Person.Age; } set { Person.Age = value; } }

        // Override constructor for showing data from SQL , Select
        public long Id { get; set; }
        public Doctor(string name1, string familyName1, int age, string Especiality, int id)
        {
            
        }
        public Doctor()
        {
            Person = new Person();
        }



        public Doctor(Person p):this()
        {
            Person = p;
        }
        public Doctor(string name, string familyName, int age) : this(new Person { Name = name, Family = familyName, Age = age }) { }
        

        public Doctor(string name1, string familyName1)
        {
            Person = new Person { Name = name1, Family = familyName1, Age = 40 };
        }

        public Doctor(string name1, string familyName1,int age, string Especiality)
        {
            Person = new Person { Name = name1, Family = familyName1, Age = age };
            Speciality = (Specialities) Enum.Parse(typeof(Specialities), Especiality,true);
        }
        public Doctor(string name1, string familyName1, int age, Specialities spec)
        {
            Person = new Person { Name = name1, Family = familyName1, Age = age };
            Speciality = spec;
        }

        // Override constructor for showing data from SQL , Select

        public override string ToString()
        {
            return $"{Id} {Name} {Family} {Age} {Speciality}";
        }

        public void ChangeName(string newName)
        {
            if (newName.Length < 50)
                Person.Name = newName;
        }

        //public override string ToString()
        //{
        //    //return base.ToString();
        //    return "I am doctor " + Person.Name + " " + Person.Family + " and " + Person.Age.ToString() + " years old, my specilality is " + Speciality;
        //}

       
        
   }

}




