using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int a1 = 10;
            int a2 = 20;

            //a1 and a2 are valu types so a copy will be passed to the Sum Method
            Sum(a1, a2);

            Console.WriteLine(a1);
            Console.WriteLine("Hello World!");
            Console.ReadLine();

            //Reference Type
            var p1 = new Person("Saeid");
            var result = ValidatePerson(p1);
            Console.WriteLine($"The validation is {result}");
            Console.WriteLine(p1.Name);


        }

        private static bool ValidatePerson(Person p)
        {
            if (p.Name == "Saeid")
            {
                p.Name = "Afshin";
                return true;

            }
                
            return false;
        }

        static void Sum(int a, int b) {
            a += 30;
            int result = a + b;
            Console.WriteLine(result);
        }


    }
}
