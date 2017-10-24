using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMulth2
{
	class Program
	{
		private static int[] array;

		static void Main(string[] args)
		{
			var taskArray = new Task[4];

			taskArray[0] = Task.Factory.StartNew(CreateArray);

			taskArray[1] = taskArray[0].ContinueWith((x) =>
			{
				MultipleArray();
			});

			taskArray[2] = taskArray[1].ContinueWith((x) =>
			{
				OrderByAscending();
			});

			taskArray[3] = taskArray[2].ContinueWith((x) =>
			{
				PrintAverage();
			});

			Console.ReadKey();
		}

		static void CreateArray()
		{
			Console.WriteLine("-- TASK 0 --");
			Random random = new Random();
			array = new int[10];

			for (int i = 0; i < 10; i++)
			{
				array[i] = random.Next(1000);
				Console.WriteLine($"array[{i}]={array[i]}");
			}
		}

		static void MultipleArray()
		{
			Random random = new Random();
			int number = random.Next(1000);
			Console.WriteLine($"-- TASK 1 -- MultipleArray [Random number is {number}]");
			for (var i = 0; i < 10; i++)
			{
				array[i] = array[i] * number;
				Console.WriteLine($"array[{i}]={array[i]}");
			}
		}

		static void OrderByAscending()
		{
			Console.WriteLine("-- TASK 2 -- Ascending");
			array = (from i in array orderby i ascending select i).ToArray();

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine($"array[{i}]={array[i]}");
			}
		}

		static void PrintAverage()
		{
			Console.WriteLine("-- TASK 3 -- Average");
			var sum = array.Sum();
			Console.WriteLine(sum / array.Length);
		}
	}
}
