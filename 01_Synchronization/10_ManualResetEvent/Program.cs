using System;
using System.Threading;

/*
	ManualResetEvent
	
	ManualResetEvent позволяет потокам взаимодействовать друг с другом путем передачи сигналов. 
	Обычно это взаимодействие касается задачи, которую один поток должен завершить до того, как другой продолжит работу. 
	Когда поток начинает работу, которая должна быть завершена до продолжения работы других потоков, 
	он вызывает метод Reset для того, чтобы поместить ManualResetEvent в несигнальное состояние. 
	Этот поток можно понимать как контролирующий ManualResetEvent. 
	Потоки, которые вызывают метод WaitOne в ManualResetEvent, будут заблокированы, ожидая сигнала. 
	Когда контролирующий поток завершит работу, он вызовет метод Set для сообщения о том, что ожидающие потоки могут продолжить работу. 
	Все ожидающие потоки освобождаются.
*/

namespace ManualResetEventNs
{
	class Program
	{
		// ManualResetEvent - Уведомляет один или более ожидающих потоков о том, что произошло событие.
	    private static ManualResetEvent manual = new ManualResetEvent(false);

		static void Main()
		{
			Console.WriteLine("Нажмите на любую клавишу для перевода ManualResetEvent в сигнальное состояние.\n");

			Thread[] threads = { new Thread(Function1), new Thread(Function2) };

			foreach (Thread thread in threads)
				thread.Start();

			Console.ReadKey();
			manual.Set(); // Просылает сигнал всем потокам.

			// Delay.
			Console.ReadKey();
		}

		static void Function1()
		{
			Console.WriteLine("Поток 1 запущен и ожидает сигнала.");
			manual.WaitOne(); // после завершения WaitOne() ManualResetEvent остаеться в сигнальном сотоянии.
			Console.WriteLine("Поток 1 завершается.");
		}

		static void Function2()
		{
			Console.WriteLine("Поток 2 запущен и ожидает сигнала.");
			manual.WaitOne(); // после завершения WaitOne() ManualResetEvent остаеться в сигнальном сотоянии.
			Console.WriteLine("Поток 2 завершается.");
		}
	}
}
