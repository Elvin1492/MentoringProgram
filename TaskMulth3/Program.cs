using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMulth3
{
	class Program
	{
		static void Main(string[] args)
		{
			var arrayA = new int[4, 4];
			var arrayB = new int[4, 4];
			var arrayC = new int[4, 4];

			var random = new Random();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					arrayA[i, j] = random.Next(400);
					arrayB[i, j] = random.Next(400);
				}
			}

			

			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					Console.WriteLine($"arrayA[{i},{j}] = {arrayA[i, j]}");
				}
			}

			Console.WriteLine();
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					Console.WriteLine($"arrayB[{i},{j}] = {arrayB[i, j]}");
				}
			}

			Console.WriteLine();

			Parallel.For(0, arrayA.GetLength(0), (i) =>
			{
				for (int j = 0; j < arrayA.GetLength(0); j++)
				{
					int v = 0;

					for (int k = 0; k < arrayA.GetLength(0); k++)
					{
						v += arrayA[i, k] * arrayB[k, j];
					}

					arrayC[i, j] = v;
				}
			});

			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					Console.WriteLine($"arrayC[{i},{j}] = {arrayC[i, j]}");
				}
			}
		}
	}
}
