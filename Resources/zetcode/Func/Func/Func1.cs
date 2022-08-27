public class Func1
{
    public static string GetMessage()
    {
        return "Hello there!";
    }

   public Func<string> sayHello  = GetMessage;
    public string s;
}