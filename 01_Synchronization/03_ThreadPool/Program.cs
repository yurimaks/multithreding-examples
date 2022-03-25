using System;
using System.Threading;

// Пул потоков.

namespace ThreadPoolNs
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Начало работы программы");
            ShowThreadInfo();
            Console.WriteLine("Запускаем Task1 в потоке из пула потоков");
            ThreadPool.QueueUserWorkItem(new WaitCallback(Task1));
            ShowThreadInfo();
            Console.WriteLine("Запускаем Task2 в потоке из пула потоков");
            ThreadPool.QueueUserWorkItem(Task2);
            ShowThreadInfo();
            Console.WriteLine("Главный поток.");

            Thread.Sleep(1000);

            Console.WriteLine("Главный поток завершен.\n");

            // Delay.
            Console.WriteLine("Task1 и Task2 закончили работу");
            ShowThreadInfo();
            Console.ReadKey();
        }

        static void Task1(Object state)
        {
            Thread.CurrentThread.Name = "1";
            Console.WriteLine($"Поток {Thread.CurrentThread.Name}");
            Thread.Sleep(200);
        }

        static void Task2(Object state)
        {
            Thread.CurrentThread.Name = "2";
            Console.WriteLine($"Поток {Thread.CurrentThread.Name}");
            Thread.Sleep(200);
        }

        //Когда метод GetAvailableThreads возвращает значение, переменная, указанная параметром workerThreads, 
        //содержит число дополнительных рабочих потоков, которые могут быть запущены, а переменная, указанная параметром 
        //completionPortThreads, содержит число дополнительных потоков асинхронного ввода/вывода, которые могут быть запущены.

        //Если доступные потоки отсутствуют, дополнительные запросы к пулу потоков будут оставаться в очереди до тех пор, 
        //пока в пуле потоков не появятся доступные потоки.
        static void ShowThreadInfo()
        {
            int availableWorkThreads, availableIOThreads, maxWorkThreads, maxIOThreads;
            ThreadPool.GetAvailableThreads(out availableWorkThreads, out availableIOThreads);
            ThreadPool.GetMaxThreads(out maxWorkThreads, out maxIOThreads);
            Console.WriteLine($"-------------Доступно рабочих потоков в пуле:{availableWorkThreads} из {maxWorkThreads}");
            Console.WriteLine($"-------------Доступно потоков ввода-вывода в пуле:{availableIOThreads} из {maxIOThreads}\n");
        }
    }
}
