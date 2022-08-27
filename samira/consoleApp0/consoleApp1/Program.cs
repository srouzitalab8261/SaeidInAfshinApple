using System;

namespace consoleApp1
{
   
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("please enter Num 1");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("please enter Num 2");
            int b = Convert.ToInt32(Console.ReadLine());
           
            var sum = new Sum();
            sum.a = a;
            sum.b = b;
            var c=sum.Add();
            Console.WriteLine(" The sum of these numbers is:"+ +c);

            var ListInt = new GenericList<int>();
            ListInt.Addd(1);
            var ListDecimal = new GenericList<decimal>();
            ListDecimal.Addd(1.1m);

            var ListDoctor= new GenericList<Doctor>();
            var d1 = new Doctor();
            var d2 = new Doctor();
            d1.Name = "Sam";
            d2.Name = "Saeid";
            ListDoctor.Addd(d1);
            ListDoctor.Mylist.Add(d2);
            Console.WriteLine(d1.Name);

            foreach (var item in ListDoctor.Mylist)
            {
                Console.WriteLine(item.Name);
            }


            var ListDoctorNew = new GenericListNew<Doctor>();
            
            ListDoctorNew.Add(new Doctor { Name="Afshin"});

            foreach (var item in ListDoctorNew)
            {
                Console.WriteLine(item.Name);
            }

            var mList = new MyList<int>(3);
            mList.Add(5); mList.Add(9); mList.Add(11);
            foreach (var item in mList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
