using System;
using System.Threading;

// Блокировка потоков, бесконечно ожидающих доступ к объекту ядра - не рациональная трата памяти.
// Пул потока предлагает механизм вызова метода.

namespace RegistredWaitHandleNs
{
    class Program
    {
        static void Main()
        {
            AutoResetEvent auto = new AutoResetEvent(false);
            WaitOrTimerCallback callback = CallbackMethod;

            // auto - от кого ждать сингнал
            // callback - что выполнять
            // null - 1-й аргумент Callback метода
            // 1000 - интервал между вызовами Callback метода
            // если true - вызвать Callback метод один раз. Если false - вызывать Callback метод с интервалом.
            ThreadPool.RegisterWaitForSingleObject(auto, callback, null, 1000, false);

            //ThreadPool.RegisterWaitForSingleObject(auto, callback, null, 2000, true); // Попробовать этот вариант.

            Console.WriteLine("S - сигнал, Q - выход");

            while (true)
            {
                string operation = Console.ReadKey(true).KeyChar.ToString().ToUpper();

                if (operation == "S")
                {
                   auto.Set();
                }
                if (operation == "Q")
                {
                    break;
                }
            }
        }

        static void CallbackMethod(object state, bool timedOut)
        {
            Console.WriteLine("Signal");
        }
    }
}
