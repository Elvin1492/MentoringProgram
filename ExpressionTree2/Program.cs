using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionTree2.Core;

namespace ExpressionTree2
{
	class Program
	{
		private static void Main(string[] args)
		{
			var mapGenerator = new MappingGenerator();
			var mapper = mapGenerator.Generate<From, To>();

			var foo = new From() { Id = 42, Name = "From foo", FullName = "Full foo name" };
			var bar = mapper.Map(foo);

			Console.WriteLine("foo: {0}", foo);
			Console.WriteLine("bar: {0}", bar);
			Console.ReadKey();
		}
	}
}
