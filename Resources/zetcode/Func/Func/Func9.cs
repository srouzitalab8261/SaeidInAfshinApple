public class Func9
{
    int[] vals = new int[] { 1, 2, 3, 4, 5 };

    Func<int, int> inc = e => e + 1;
    Func<int, int> cube = e => e * e * e;

    IEnumerable<int> res;


    public Func9()
    {
        res = vals.Select(inc).Select(cube);
    }

    public void PrnCalculation()
    {
        foreach (var e in res)
        {
            Console.WriteLine(e);
        }
    }
}