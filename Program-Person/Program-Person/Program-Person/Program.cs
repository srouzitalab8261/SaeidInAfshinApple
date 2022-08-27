using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main1(string[] args)
        {
            //var person1 = new Person("samira","abbasi",34);
            var person1 = new Person();
            {

                Console.WriteLine("Hi");
                Console.Write("Your Name:");
                person1.Name = Console.ReadLine();

                Console.Write("Your Family:");
                person1.Family = Console.ReadLine();

                Console.Write("Your Age:");
                person1.Age = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Your Name is {0}", person1.Name);

                Console.WriteLine("Your Family is" + " " + person1.Family);

                Console.WriteLine($"Your Age is {person1.Age}");

                Console.ReadKey();


            }
        }
    }
}
