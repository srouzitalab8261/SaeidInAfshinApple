public class Func8
{
    List<Person> person = new List<Person>()
    {
        new Person() { Name = "ANNA", Occupation = "Teacher" },
        new Person() { Name = "Abbas", Occupation = "Oil Engineer" }
    
    };
    public List<Person> data = new List<Person>
    {
        new ("John Doe", "gardener"),
        new Person("s","f"),
        new ("Robert Brown", "programmer"),
        new ("Lucia Smith", "teacher"),
        new ("Thomas Neuwirth", "teacher")
    };


// ShowOutput(data, r => r.Occupation == "teacher");
//bool Condition(Person r) => r.Occupation == "Teacher"; //To local function form of below line
    public Func<Person, bool> Condition = r => r.Occupation == "Teacher";

    public void ShowOutput(List<Person> list, Func<Person, bool> condition)
    {
        var data = list.Where(condition);

        foreach (var person in data)
        {
            Console.WriteLine("{0}, {1}", person.Name, person.Occupation);
        }
    }
//record Person(string Name, string Occupation);
    public class Person
    {
        public string Name { get; set; }
        public string Occupation { get; set; }

        public Person()
        {
        
        }
        public Person(string name,string occupation)
        {
            Name = name;
            Occupation = occupation;
        }
    
    }
}