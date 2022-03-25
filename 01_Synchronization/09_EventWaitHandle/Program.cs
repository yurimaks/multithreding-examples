using System;
using System.Threading;

namespace EventWaitHandleNs
{
    class Program
    {
        // AutoResetEvent - Уведомляет ожидающий поток о том, что произошло событие. 
        static readonly AutoResetEvent auto = new AutoResetEvent(false);

        static void Main()
        {
            Console.WriteLine("Нажмите на любую клавишу для перевода AutoResetEvent в сигнальное состояние.\n");

            Thread[] threads = { new Thread(Function1), new Thread(Function2) };

            foreach (Thread thread in threads)
                thread.Start();

            Console.ReadKey();
            auto.Set(); // Послать сигнал первому потоку

            Console.ReadKey();
            auto.Set(); // Послать сигнал второму потоку

            // Delay.
            Console.ReadKey();
        }

        static void Function1()
        {
            Console.WriteLine("Поток 1 запущен и ожидает сигнала.");
            auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
            Console.WriteLine("Поток 1 завершается.");
        }

        static void Function2()
        {
            Console.WriteLine("Поток 2 запущен и ожидает сигнала.");
            auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
            Console.WriteLine("Поток 2 завершается.");
        }
    }
}
