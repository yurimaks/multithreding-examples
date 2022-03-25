using System;
using System.Threading;

// Ииспользование Mutex для синхронизации доступа к защищенным ресурсам.

// Mutex - Примитив синхронизации, который также может использоваться в межпроцессной и междоменной синхронизации.
// MutEx - Mutual Exclusion (Взаимное Исключение).

namespace MyNamespace
{
    class Program
    {
        private static Mutex mutex = new Mutex();

        static void Main()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(new ThreadStart(Function));
                thread.Name = $"Поток {i + 1}";
                thread.Start();
            }

            // Задержка.
            Console.ReadKey();
        }

        private static void Function()
        {
            for (int i = 0; i < 2; i++)
            {
                UseResource();
            }
        }

        // Данный метод представляет собой ресурс, который должен быть синхронизирован так,
        // что только один поток может его выполнять в одно время.
        private static void UseResource()
        {
            // Метод WaitOne используется для запроса на владение мьютексом.
            mutex.WaitOne(); // Блокирует текущий поток.

            Console.WriteLine($"{Thread.CurrentThread.Name} вошел в защищенную область.");
            Thread.Sleep(1000); // Выполнение некоторой работы...
            Console.WriteLine($"{Thread.CurrentThread.Name} покидает защищенную область.\r\n");
            
            mutex.ReleaseMutex();  // Освобождение Mutex.

            Thread.Sleep(1000); // Выполнение некоторой работы...
        }
    }
}