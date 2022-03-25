using System;
using System.Threading;

namespace MutexSampleNs
{
    class Program
    {
        static void Main()
        {
            var instance = new SomeClass();

            for (int i = 0; i < 5; i++)
            {
                var thread = new Thread(instance.Method1);
                thread.Start();
            }

            // Delay.
            Console.ReadKey();
        }
    }

    // Рекурсивное запирание.
    public class SomeClass : IDisposable
    {
        private Mutex mutex = new Mutex();

        public void Method1()
        {
            mutex.WaitOne();
            Console.WriteLine("Method1 Start");
            Method2();
            Console.WriteLine("Method1 End");
            mutex.ReleaseMutex();
        }

        public void Method2()
        {
            mutex.WaitOne();
            Console.WriteLine("Method2 Start");
            Thread.Sleep(1000);
            Console.WriteLine("Method2 End");
            mutex.ReleaseMutex();
        }

        public void Dispose()
        {
            mutex.Dispose();
        }
    }
}
 