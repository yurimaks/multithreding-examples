using System;
using System.Threading;

namespace WaitHandleNs
{
    class Program
    {
        static WaitHandle[] events = new WaitHandle[] { new AutoResetEvent(false), new AutoResetEvent(false) };

        static Random random = new Random();

        static void Main()
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine("Главный поток ожидает завершения ОБЕИХ задач.\n");

            // Очередь для двух задач в двух разных потоках. 
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task1), events[0]);
            ThreadPool.QueueUserWorkItem(Task2, events[1]);

            // Ожидание пока все задачи завершаться.
            WaitHandle.WaitAll(events);

            // Время отображаемое ниже, должно совпадать с продолжительностью выполнения самой длинной задачи.
            Console.WriteLine($"Обе задачи завершены (время ожидания = {(DateTime.Now - dateTime).TotalMilliseconds})");

            dateTime = DateTime.Now;

            Console.WriteLine("\nОжидание завершения одной из задач.");
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task1), events[0]);
            ThreadPool.QueueUserWorkItem(Task2, events[1]);

            // Ожидание пока одна из задач не завершится.
            int index = WaitHandle.WaitAny(events);

            // Время отображаемое ниже, должно совпадать с продолжительностью выполнения самой короткой задачи.
            Console.WriteLine($"Задача {index + 1} завершилась первой (время ожидания = {(DateTime.Now - dateTime).TotalMilliseconds}).");

            // Задержка.
            Console.ReadKey();
        }

        static void Task1(Object state)
        {
            var auto = (AutoResetEvent)state;
            int time = 1000 * random.Next(2, 10);
            Thread.Sleep(time);
            Console.WriteLine($"Задача 1 выполнена за {time} миллисенунд.");
            auto.Set();
        }

        static void Task2(Object state)
        {
            var auto = (AutoResetEvent)state;
            int time = 1000 * random.Next(2, 10);
            Thread.Sleep(time);
            Console.WriteLine($"Задача 2 выполнена за {time} миллисенунд.");
            auto.Set();
        }
    }
}
