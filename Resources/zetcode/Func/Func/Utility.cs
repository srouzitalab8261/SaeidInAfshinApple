using System.Reflection.Emit;

public static class Utility
{
    public static void HowToContinue(string ClassName)
    {
        Label:
        Console.Write("\nPress 'Y' to continue, 'N' to exit the process...\n");

        // while (Console.ReadKey().Key != ConsoleKey.Y) 
        // {
        //     if (Console.ReadKey().Key == ConsoleKey.N)
        //     {
        //         break;
        //         return;//It works in program class(stop program)
        //     }
        // }

        //string? key = Console.ReadKey().ToString(); //Read what is being pressed
        // if (key == "y" || key == "Y")
        
        ConsoleKey key = Console.ReadKey().Key;
        Console.WriteLine();
        if (key != ConsoleKey.Y)
        {
            if (key == ConsoleKey.N)
            {
                //return; //stop further execution
                Environment.Exit(1);
            }

            Console.Clear();
            goto Label;
        }
        Console.WriteLine(ClassName);


        //     Console.WriteLine("Enter a value: ");
        //     string? str = Console.ReadLine();
        //
        //     if (string.IsNullOrWhiteSpace(str))
        //         // ReSharper disable once RedundantJumpStatement
        //         return;
        //     Console.WriteLine(string.Format("you have entered: '{0}'", str));
        //     Console.Read();
    }
}