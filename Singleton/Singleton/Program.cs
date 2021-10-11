using System;
using System.Threading;
namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            // use a naive Schaf

            NaiveSchaf schaf1 = NaiveSchaf.getInstance("brown");
            NaiveSchaf schaf2 = NaiveSchaf.getInstance("white");

            if (schaf1 == schaf2)
            {
                Console.WriteLine("We are the same sheep with {0} fur", schaf1.Color);
            }
            else
            {
                Console.WriteLine("We are not the same sheep");
            }

            // use a Thread safe sheep

            Console.WriteLine(
                "{0}\n{1}\n\n{2}\n",
                "If you see the same value, then singleton was reused (yay!)",
                "If you see different values, then 2 singletons were created (booo!!)",
                "RESULT:"
            );

            Thread thread1 = new Thread(() => {

                TestSingleton("brown");
            });

            Thread thread2 = new Thread(() =>
            {

                TestSingleton("white");
            });

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();


            Console.ReadKey(true);
        }

        public static void TestSingleton(string color)
        {
            TSSchaf schaf = TSSchaf.getInstance(color);
            Console.WriteLine(schaf.Color);

        }
    }
}
