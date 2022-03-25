using System;
using System.Threading;

// Альтернативные операции VolatileWrite() и VolatileRead() ключевому слову volatile.

namespace VolatileSample2
{
    class Program
    {
        // static volatile int stop = 0;
        static int stop;

        static void Main()
        {
            Console.WriteLine("Main: запускается поток на 2 секунды.");
            var t = new Thread(Worker);
            t.Start();

            Thread.Sleep(2000);

            Thread.VolatileWrite(ref stop, 1);

            Console.WriteLine("Main: ожидание завершения потока.");
            t.Join();

			Console.ReadKey();
        }

        private static void Worker(Object o)
        {
            int x = 0;

            while (Thread.VolatileRead(ref stop) != 1)
            {
                x++;
            }

            Console.WriteLine($"Worker: остановлен при x = {x}.");
        }
    }
}
