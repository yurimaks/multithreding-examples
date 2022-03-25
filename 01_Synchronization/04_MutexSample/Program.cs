using System;
using System.Threading;

// Ииспользование Mutex для синхронизации доступа к защищенным ресурсам.

// Mutex - Примитив синхронизации, который также может использоваться в межпроцессной и междоменной синхронизации.
// MutEx - Mutual Exclusion (Взаимное Исключение).

namespace MutexSample
{
    class Program
    {
        // Mutex - Примитив синхронизации, который также может использоваться в межпроцессорной синхронизации.
        // функционирует аналогично AutoResetEvent но снабжен дополнительной логикой:
        // 1. Запоминает какой поток им владеет. ReleaseMutex не может вызвать поток, который не владеет мьютексом.
        // 2. Управляет рекурсивным счетчиком, указывающим, сколько раз поток-владелец уже владел объектом.
        static Mutex mutex = new Mutex();

        static void Main()
        {
            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(Function));
                threads[i].Name = i.ToString();
                threads[i].Start();
            }

            // Delay.
            Console.ReadKey();
        }

        static void Function()
        {
            mutex.WaitOne();
            Console.WriteLine($"Поток {Thread.CurrentThread.Name} зашел в защищенную область.");
            Thread.Sleep(100);
            Console.WriteLine($"Поток {Thread.CurrentThread.Name} покинул защищенную область.\n");
            mutex.ReleaseMutex();
        }
    }
}
