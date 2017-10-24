using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskMulth5
{
	class Program
	{
		static readonly object locker = new object();
		private static int number = 10;

		static void Main(string[] args)
		{
			Semaphore semaphore = new Semaphore(0, 10);
			ThreadPool.QueueUserWorkItem((x) => { Continue(10, semaphore); });
			WaitHandle.WaitAll(new WaitHandle[]{semaphore});
		}

		static void Continue(int num, Semaphore semaphore)
		{
			if (num > 0)
			{
				//semaphore.WaitOne();
				Console.WriteLine(num);
				num--;
				ThreadPool.QueueUserWorkItem((x) => { Continue(num, semaphore); });
				//semaphore.Release();
			}
		}
	}
}
