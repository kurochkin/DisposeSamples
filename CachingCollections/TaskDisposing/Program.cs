using System;
using System.Threading;

namespace ConsoleApplication19
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var classWithThread = new ClassWithTread())
            {
                Thread.Sleep(3000);
            }
            Console.WriteLine("Thread and class might be disposed.");

            Console.ReadKey();

        }
    }
}
