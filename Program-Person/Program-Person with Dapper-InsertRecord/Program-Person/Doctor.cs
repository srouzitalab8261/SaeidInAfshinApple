using System;

namespace Program_Person
{
    public class Doctor
    {
        Person Person { get; }
        public Specialities Speciality { get;}

        public string Name { get { return Person.Name; } }
        public string Family { get { return Person.Family; } }
        public int Age { get { return Person.Age; } }


        public Doctor(Person p)
        {
            Person = p;
        }
        public Doctor(string name, string familyName, int age) : this(new Person { Name = name, Family = familyName, Age = age }) { }
        

        public Doctor(string name1, string familyName1)
        {
            Person = new Person { Name = name1, Family = familyName1, Age = 40 };
        }

        public Doctor(string name1, string familyName1,int age, string speciality)
        {
            Person = new Person { Name = name1, Family = familyName1, Age = age };
            Speciality = (Specialities) Enum.Parse(typeof(Specialities), speciality);
        }
        public Doctor(string name1, string familyName1, int age, Specialities spec)
        {
            Person = new Person { Name = name1, Family = familyName1, Age = age };
            Speciality = spec;
        }

        

        public void ChangeName(string newName)
        {
            if (newName.Length < 50)
                Person.Name = newName;
        }

        public override string ToString()
        {
            //return base.ToString();
            return "I am doctor " + Person.Name + " " + Person.Family + " and " + Person.Age.ToString() + " years old, my specilality is " + Speciality;
        }

       
        
   }

}




