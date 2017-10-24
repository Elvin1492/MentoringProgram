using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskMulth1
{
	class Program
	{
		static void Main(string[] args)
		{
			var taskArray = new Task[10];

			for (var i = 0; i <= taskArray.Length; i++)
			{
				taskArray[i] = Task.Factory.StartNew((Object obj) =>
				{
					var data = obj as KeepedData;
					if (data == null)
						return;

					data.ThreadNum = Thread.CurrentThread.ManagedThreadId;

					Console.WriteLine("Task #{0} created at {1} on thread #{2}.",
									 data.Name, data.CreationTime, data.ThreadNum);
					for (int j = 0; j <= 1000; j++)
					{
						Console.WriteLine("Task #{0} - iteration number{1}",
									 data.Name, j);
					}
				},
				new KeepedData() { Name = i, CreationTime = DateTime.Now.Ticks });
			}
			Task.WaitAll(taskArray);
		}
	}

	class KeepedData
	{
		public long CreationTime;
		public int Name;
		public int ThreadNum;
	}
}
