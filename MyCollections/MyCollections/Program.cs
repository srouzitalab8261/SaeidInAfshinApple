using System;

namespace MyCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var myenumerator = new MyEnumerator(11);
            while (myenumerator.MoveNext())
            {
                Console.WriteLine(myenumerator.Current);
            }

            var oddNumbers = new OddNumbers(11);
            foreach (var item in oddNumbers)
            {
                Console.WriteLine(item);

            }
            var l = 10;
            var fancy = new FancyCollection<Int32>(l);

            for (int i = 0; i < l; i++)
            {
                fancy[i] = 3 * i;
            }
            var j = 0;

            foreach (var item in fancy)
            {
                Console.WriteLine(fancy[j++]);
                //Console.WriteLine(item);
            }

            var fancy2 = new FancyCollection<decimal>(l);
            for (int i = 0; i < l; i++)
            {
                fancy2[i] = 3.14m*i;
            }
            foreach (var item in fancy2)
            {
                Console.WriteLine(item);
            }

            var fancy3 = new FancyCollection<Doctor>(10);
            for (int i = 0; i < l; i++)
            {
                fancy3[i] = new Doctor() { Name = $"Doctor{i}" };
            }

            foreach (var item in fancy3)
            {
                Console.WriteLine(((Doctor)item).Name);
            }
        }
    }
}
