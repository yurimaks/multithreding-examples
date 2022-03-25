using System;
using System.Threading;

namespace EventSlim
{
    class Program
    {
        // ManualResetEventSlim - изначально используется SpinWait блокировка на 1000 итераций, 
        // после чего происходит синхронизация с помощью объекта ядра.
        static ManualResetEventSlim slim = new ManualResetEventSlim(false, 1000);

        static void Main()
        {
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start();
            }

            Console.ReadKey();
            slim.Set();

            // Delay.
            Console.ReadKey();
        }

        static void Function()
        {
            slim.Wait();
            Console.WriteLine($"Поток {Thread.CurrentThread.Name} начал работу.");
        }
    }
}