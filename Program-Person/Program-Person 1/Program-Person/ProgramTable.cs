using System;

namespace ConsoleApp1
{
    class ProgramTable
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter Number of Persons:");
            int Number = Convert.ToInt32(Console.ReadLine());
            PersonTable[] people = new PersonTable[Number];

            Console.WriteLine("Hi");

            for (int i=0;i<Number;i++)

            {
                Console.Write($"Enter Person[{i+1}] name:");
                string Name=Console.ReadLine();
                
                Console.Write($"Enter Person[{i + 1}] family:");
                string Family = Console.ReadLine();
               
                Console.Write($"Enter Person[{i + 1}] age:");
                int Age = Convert.ToInt32(Console.ReadLine());

                PersonTable p = new PersonTable(Name, Family, Age);
                people[i] = p;

               
               

            }

            Console.ForegroundColor = ConsoleColor.Green;

            foreach (PersonTable personTable in people)
            {
                Console.WriteLine($"Name:{personTable.Name} Family:{personTable.Family} Age:{personTable.Age}");

            }

            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
