using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskMulth4
{
	class Program
	{
		static readonly object locker = new object();
		private static int number = 10;

		static void Main(string[] args)
		{
			Thread t = new Thread(() => { Continue(10); });
			t.Start();
			t.Join();
		}

		static void Continue(int num)
		{
			if (num > 0)
			{
				Console.WriteLine(num);
				num--;
				Thread t = new Thread(() => { Continue(num); });
				t.Start();
				t.Join();
			}
		}
	}



}
