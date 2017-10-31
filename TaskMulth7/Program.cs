using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskMulth7
{
	class Program
	{
		static void Main(string[] args)
		{
			Task taskA = Task.Factory.StartNew(() => Console.WriteLine(DateTime.Today.DayOfWeek));

			//a
			Task continuationRegardless = taskA.ContinueWith(x => Console.WriteLine("SYNC"), TaskContinuationOptions.ExecuteSynchronously);

			taskA = Task.Factory.StartNew(() =>
			{
				try
				{
					Thread.CurrentThread.Abort();
				}
				finally 
				{
					
				}

			});

			

			Task continuationCancelled = taskA.ContinueWith(x => Console.WriteLine("Cancelled"), TaskContinuationOptions.OnlyOnFaulted);

			Task continuationWithoutSuccess = taskA.ContinueWith(x => Console.WriteLine("WithoutSuccess"), TaskContinuationOptions.OnlyOnCanceled);

			Thread.Sleep(1000);
			Task.WaitAll(taskA);
		}
	}
}
