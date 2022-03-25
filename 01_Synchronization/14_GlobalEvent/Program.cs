using System;
using System.Threading;

// Для тестирования данного примера, запустите несколько экземпляров данного приложения.

namespace GlobalEventNs
{
    class Program
    {
        static EventWaitHandle manual = null;

        static void Main()
        {
            // Если объект ядра с именем GlobalEvent уже существует будет получена ссылка на него.
            // false - несигнальное состояние.
            // ManualReset - тип событиия.
            // GlobalEvent - имя по которому все приложения будут слушать событие.
            manual = new EventWaitHandle(false, EventResetMode.ManualReset, "GlobalEvent");

            Thread thread = new Thread(Function);
            thread.IsBackground = true;
            thread.Start();

            Console.WriteLine("Нажмите любую клавишу для начала работы потока.");
            Console.ReadKey();

            // Перевод события в сигнальное состояние.
            // Все приложения, которые используют событие с именем GlobalEvent, получат оповещение о переходе в сигнальное состояние.
            manual.Set();

            // Delay.
            Console.ReadKey();
        }

        static void Function()
        {
            manual.WaitOne();

            while (true)
            {
                Console.WriteLine("Hello world!");
                Thread.Sleep(300);
            }
        }
    }
}
