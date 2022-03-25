using System;
using System.Threading;

// ManualResetEvent

namespace Event
{
    class Work
    {
        readonly ManualResetEvent manual;
        readonly Thread thread;

        public Work(string name, ManualResetEvent manual)
        {
            this.thread = new Thread(this.Run) {Name = name};
            this.manual = manual;
            this.thread.Start();
        }

        void Run()
        {
            Console.WriteLine("Запущен поток " + thread.Name);

            for (int i = 0; i < 10; i++)
            {
                Console.Write(". ");
                Thread.Sleep(200);
            }

            Console.WriteLine("\nПоток " + thread.Name + " завершен");

            // Уведомление о событии происходит при помощи вызова метода Set()
            manual.Set();
        }
    }

    class ManualEventDemo
    {
        static void Main()
        {
            // false - установка несигнального состояния.
            var manual = new ManualResetEvent(false);

            var thread = new Work("1", manual);
            Console.WriteLine("Основной поток ожидает событие.\n");

            manual.WaitOne(); // Ожидать уведомления о событии. 
            Console.WriteLine("\nОсновной поток получил уведомление о событии от первого потока.\n");

            manual.Reset(); // Сбрасывает в несигнальное состояние.

            thread = new Work("2", manual);
            Console.WriteLine("Основной поток ожидает событие.\n");

            manual.WaitOne(); // Ожидать уведомления о событии. 
            Console.WriteLine("\nОсновной поток получил уведомление о событии от второго потока.");

            // Delay.
            Console.ReadKey();
        }
    }
}