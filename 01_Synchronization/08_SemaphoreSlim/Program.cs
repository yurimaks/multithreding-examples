using System;
using System.Threading;

namespace SemaphoreSlimSample
{
    class Program
    {
        // SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.
        static readonly SemaphoreSlim slim = new SemaphoreSlim(1, 2);

        static void Main()
        {
            Thread[] threads = { new Thread(Function), new Thread(Function), new Thread(Function) };

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Name = i.ToString();
                threads[i].Start();
            }

            Thread.Sleep(1000);
            slim.Release();  // Возможен принудительный сброс из потока владельца семафора.

            // Delay.
            Console.ReadKey();
        }

        static void Function()
        {
            slim.Wait();

            Console.WriteLine($"Поток {Thread.CurrentThread.Name} начал работу.");
            Thread.Sleep(1000);
            Console.WriteLine($"Поток {Thread.CurrentThread.Name} закончил работу.\n");

            slim.Release();
        }
    }
}
