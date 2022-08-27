public class Func2
{
    static int Sum(int x, int y)
    {
        return x + y;
    }

    //static Func<int, int, int> add = Sum;
    public static Func<int, int, int> add = Sum;

    public int res = add(150, 10);

    //delegate definition
    public delegate int Add(int x, int y);
    public Add Add1 = Sum;
    public static Add Add2 = new Add(Sum);
    
    
}