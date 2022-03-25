using System;
using System.Threading;

namespace SimpleLock
{
	class Program
	{
		static HybridLock block = new HybridLock();

		static void Main()
		{
			Thread[] threads = { new Thread(Function), new Thread(Function) };

			for (int i = 0; i < threads.Length; i++)
			{
				threads[i].Name = i.ToString();
				threads[i].Start();
			}

			// Delay.
			Console.ReadKey();
		}

		static void Function()
		{
			block.Enter();
			Console.WriteLine($"Поток {Thread.CurrentThread.Name} выполнил операцию.");
			block.Leave();
		}
	}

	class HybridLock : IDisposable
	{
		private int count = 0; // для использования примитивной конструкцией пользовательского режима.
		private AutoResetEvent auto = new AutoResetEvent(false);

		public void Enter()
		{
			if (Interlocked.Increment(ref count) == 1)
				return;

			auto.WaitOne();
		}

		public void Leave()
		{
			if (Interlocked.Decrement(ref count) == 0)
				return;

			auto.Set();
		}

		public void Dispose()
		{
			auto.Dispose();
		}
	}
}
