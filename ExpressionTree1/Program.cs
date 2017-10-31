using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree1
{
	class Program
	{
		static void Main(string[] args)
		{
			Expression<Func<int, int>> exp = (a) => (a + 1) * (4 + 1) - 1 + 1;
			Expression<Func<int, int>> changedExpression = (Expression<Func<int, int>>)new TraceExpressionVisitor().Visit(exp);

			Console.WriteLine("{0} : ", changedExpression);
			Console.WriteLine("{0} : ", exp);
			Console.WriteLine("{0} : ", changedExpression.Compile().Invoke(3));
			Console.WriteLine("{0} : ", exp.Compile().Invoke(3));

			Expression<Func<int, int, int>> replace = (a, b) => b * a - 5;
			Console.WriteLine("{0} : ", replace.Compile().Invoke(3, 3));

			var dict = new Dictionary<string, int>();
			dict.Add("a", 5); dict.Add("b", 4);

			Expression<Func<int, int, int>> replaced =
				(Expression<Func<int, int, int>>)new TraceExpressionVisitor().Visit(replace, dict);

			Console.WriteLine("{0} : ", replaced.Compile().Invoke(3, 3));
			Console.ReadKey();
		}
	}
}
