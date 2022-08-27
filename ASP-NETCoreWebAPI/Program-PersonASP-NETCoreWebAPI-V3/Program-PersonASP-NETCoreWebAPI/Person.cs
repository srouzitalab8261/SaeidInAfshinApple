using System;

namespace Program_PersonASP_NETCoreWebAPI
{
    public class Person
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        public string Family { get; set; }
        public int Age { get; set; }

        public Person(string name, String family, int age)
        {
            Name = name;
            Family = family;
            Age = age;

        }
        public Person()
        {

        }
        public override string ToString()
        {
            //return base.ToString();
            return  "I am " + Name +" "+Family+" and " + Age.ToString()+" years old." ;
        }
    }
    
}
