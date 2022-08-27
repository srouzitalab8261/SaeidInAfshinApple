
namespace Program_Person
{
    public class Nurse : Person
    {
        public Nurse(string Name, string Family, int Age) : base(Name, Family, Age)
        {

        }

        public Nurse(string Name, string Family) : base(Name, Family, 18)
        {

        }

        public Nurse() : base()
        {

        }

    }
}