public class Func7
{
    //var users=new List<User>
    //User[] users =
    User[] users = new User[]
    {
        new(1, "John", "London", "2001-04-01"),
        new(2, "Lenny", "New York", "1997-12-11"),
        new(3, "Andrew", "Boston", "1987-02-22"),
        new(4, "Peter", "Prague", "1936-03-24"),
        new(5, "Anna", "Bratislava", "1973-11-18"),
        new(6, "Albert", "Bratislava", "1940-12-11"),
        new(7, "Adam", "Trnava", "1983-12-01"),
        new(8, "Robert", "Bratislava", "1935-05-15"),
        new(9, "Robert", "Prague", "1998-03-14"),
    };

    string city = "Bratislava";
    //Func<User, bool> livesIn;
    //Using predicate:1-No need to set output as bool because it is defined as a delegate with bool output.
    Predicate<User> livesIn;
    private IEnumerable<User> res;
    
    int age = 60;
    //Func<User, bool> olderThan;
    Predicate<User> olderThan;
    IEnumerable<User> ageusers;
    public Func7()
    {
        livesIn = e => e.City == city;
        //Using predicate:2-must use array class instead of the list directly.
        res = Array.FindAll(users, livesIn);
         
         olderThan = a => GetAge(a) > age;
         ageusers = Array.FindAll(users,olderThan);
    }

    public void PrnBratislava()
    {
        foreach (var e in new Func7().res)
        {
            Console.WriteLine(e);
        }
        Console.WriteLine("---------------------");

    }

    public void PrnAge()
    {
        foreach (var e in new Func7().ageusers)
        {
            Console.WriteLine(e);
        }
    }

    
    
    int GetAge(User user)
    {    
        var dob = DateTime.Parse(user.DateOfBirth);
        return (int) Math.Floor((DateTime.Now - dob).TotalDays / 365.25D);
    }

    record User(int id, string Name, string City, string DateOfBirth);
}