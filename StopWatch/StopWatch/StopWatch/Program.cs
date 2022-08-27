using System;
//using System.Diagnostics;
using System.Threading;

namespace StopWatch
{
    public class Stopwatch
    {
        //double t;
        public DateTime start()
        {
            var StartTime = DateTime.Now;
            return StartTime;
        }
        //public DateTime stop(double t)
        public DateTime stop()
        {

            //var StopTime = DateTime.Now.AddSeconds(t);
            var StopTime = DateTime.Now;
            return StopTime;
        }
    }

    class Program
    {
        //static void Main0(string[] args)
        //{
        //    Console.WriteLine("Hello World!");
        //    var stopwatch = new Stopwatch();
        //    TimeSpan duration = stopwatch.stop(6) - stopwatch.start();
        //    Console.WriteLine(duration);
            
        //}
        static void Main(string[] args)
        {
            Stopwatch stopw = new Stopwatch();
            stopw.start();
            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(2);
            }
            stopw.stop();
            // Console.WriteLine(" Time elapsed: {0} ", stopw.Elapsed);
            var Duration = new TimeSpan();
            Duration=stopw.stop() - stopw.start();
            Console.WriteLine(" Time elapsed: {0} ", Duration);
        }
    }
    
}
