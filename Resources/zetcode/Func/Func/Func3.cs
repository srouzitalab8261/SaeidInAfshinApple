public class Func3
{
    public static Func<int, int, int> randInt = (n1, n2) => new Random().Next(n1, n2);
}