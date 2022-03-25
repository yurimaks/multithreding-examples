using System;
using System.Threading;

// Класс Semaphore - используется для управления доступом к пулу ресурсов. 
// Потоки занимают слот семафора, вызывая метод WaitOne(), и освобождают занятый слот вызовом метода Release().

namespace MyNamespace
{
    public class Program
    {
        private static Semaphore pool;

        private static void Work(object number)
        {
            pool.WaitOne();

            Console.WriteLine($"Поток {number} занял слот семафора.");
            Thread.Sleep(1000);
            Console.WriteLine($"Поток {number} -----> освободил слот.");

            pool.Release();
        }

        public static void Main()
        {
            // Первый аргумент:
            // Задаем количество слотов для использования в данный момент (не более максимального клоличества).
            // Второй аргумент:
            // Задаем максимальное количество слотов для данного семафора.
            pool = new Semaphore(2, 4);

            for (int i = 1; i <= 8; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(Work));
                thread.Start(i);
            }
            
            // Задержка.
            Console.ReadKey();
        }
    }
}