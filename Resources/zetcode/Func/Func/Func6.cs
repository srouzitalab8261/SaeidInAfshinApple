public class Func6
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
    Func<User, bool> livesIn;
    private IEnumerable<User> res;
    
    int age = 60;
    Func<User, bool> olderThan;
    IEnumerable<User> ageusers;
    public Func6()
    {
        livesIn = e => e.City == city;
         res = users.Where(livesIn);
         
         olderThan = a => GetAge(a) > age;
         ageusers = users.Where(olderThan);
    }

    public void PrnBratislava()
    {
        foreach (var e in new Func6().res)
        {
            Console.WriteLine(e);
        }
        Console.WriteLine("---------------------");

    }

    public void PrnAge()
    {
        foreach (var e in new Func6().ageusers)
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