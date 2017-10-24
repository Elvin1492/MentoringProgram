using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskMulth6
{
	class Program
	{
		static void Main(string[] args)
		{
			ConcurrentBag<int> bag = new ConcurrentBag<int>();

			Task t1 = Task.Factory.StartNew(() =>
			{
				for (int i = 1; i <= 10; ++i)
				{
					bag.Add(i);
					Task t2 = Task.Factory.StartNew(() =>
					{

						foreach (var item in bag)
						{
							Console.WriteLine(item);
						}


						Console.WriteLine();
					});
					Task.WaitAll(t2);
				}
			});

			Task.WaitAll(t1);
		}
	}
}
