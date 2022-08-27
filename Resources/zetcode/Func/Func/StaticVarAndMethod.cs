public class StaticVarAndMethod
{
    private static int _a,_b;
    public static string B;
    private string _c;

    public int PublicSum(int a,int b)
    {
        _a = 3;
        //_b = 5;
        return a + b*(_a+_b);
    }
    
    int PrivateSum(int a,int b)
    {
        _a = 3;
        //_b = 5;
        return a + b*(_a+_b);
    }

    public static void StaticMethod()
    {
        Console.WriteLine("No need to initialize the class for accessing to StaticMethod");
    }
    

}