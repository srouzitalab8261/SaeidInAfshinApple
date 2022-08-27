
public class Func5
{
    // var vals = new int[] { 1, 2, 3, 4, 5 };
    //
    // Func<int, int> square = x => x * x;
    // Func<int, int> cube = x => x * x * x;
    // Func<int, int> inc = x => x + 1;
    //
    // var fns = new List<Func<int, int>> 
    //     {
    //         inc, square, cube
    //     };
    //
    //     foreach (var fn in fns)
    // {
    //     var res = vals.Select(fn);
    //
    //     Console.WriteLine(string.Join(", ", res));
    // } 

    public int[] vals = new int[] { 1, 2, 3, 4, 5 };

    static Func<int, int> square = x => x * x;
    static Func<int, int> cube = x => x * x * x;
    static Func<int, int> inc = x => x + 1;

    public List<Func<int, int>> fns = new()
        {
            inc, square, cube
        };

    public static void PrnOperations()
    {
        foreach (var fn in new Func5().fns)
        {
            var res = new Func5().vals.Select(fn);
    
            Console.WriteLine(string.Join(", ", res));
        } 
        Console.WriteLine("---------------------");
    }
    
    public static void PrnOperations1(List<Func<int, int>> fns,int[] vals)
    {
        foreach (var fn in fns)
        {
            var res = vals.Select(fn);
    
            Console.WriteLine(string.Join(", ", res));
        } 
        Console.WriteLine("---------------------");

    }
      
    
    public static void PrnOperation()
    {
        int[] vals = { 1, 2, 3, 4, 5 };
        //int[] vals =new int[5] { 1, 2, 3, 4, 5 };
        (int, int)[] Nums ;
        Nums=new (int, int)[] {(1,4),(2,3),(3,7)};
        
        
        
        
        
        


        Func<int, int> square = x => x * x;
         Func<int, int> cube = x => x * x * x;
         Func<int, int> inc = x => x + 1;

    List<Func<int, int>> fns = new()
        {
            inc, square, cube
        };
        foreach (var fn in fns)
        {
            var res = vals.Select(fn);
    
            Console.WriteLine(string.Join(", ", res));
        } 
        Console.WriteLine("---------------------");
    }
       
}