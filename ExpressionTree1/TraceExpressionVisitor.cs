using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTree1
{
	public class TraceExpressionVisitor : ExpressionVisitor
	{
		public int Indent = 0;

		public Dictionary<string, int> Replacers;

		public Expression Visit(Expression node, Dictionary<string, int> dict)
		{
			if (node == null)
				return base.Visit((Expression) null);

			Replacers = dict;
			var result = base.Visit(node);

			return result;
		}

		public override Expression Visit(Expression node)
		{
			if (node == null)
				return base.Visit((Expression) null);

			Indent++;
			var result = base.Visit(node);
			//Console.WriteLine("{0}{1} - {2}", new String(' ', Indent * 4), result.NodeType, result.GetType());

			Indent--;

			return result;
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			if (node.Right.NodeType == ExpressionType.Constant && node.NodeType == ExpressionType.Add && node.Right.ToString() == "1")
			{
				Expression newNode = Expression.Increment(node.Left);
				return base.Visit(newNode);
			}

			if (node.Right.NodeType == ExpressionType.Constant && node.NodeType == ExpressionType.Subtract && node.Right.ToString() == "1")
			{
				Expression newNode = Expression.Decrement(node.Left);
				return base.Visit(newNode);
			}

			if (node.Right.NodeType == ExpressionType.Parameter || node.Left.NodeType == ExpressionType.Parameter)
			{
				var newLeft = ReplaceParameter(node.Left);
				var newRight = ReplaceParameter(node.Right);
				if (newLeft != node.Left || newRight != node.Right)
				{
					var newNode = Expression.MakeBinary(node.NodeType, newRight, newLeft);
					return base.VisitBinary(newNode);
				}
			}

			return base.VisitBinary(node);
		}

		public Expression ReplaceParameter(Expression parameter)
		{
			if (Replacers == null) return parameter;
			var par = parameter as ParameterExpression;
			var val = Replacers[par.Name];

			return val != 0 ? Expression.Constant(val) : parameter;
		}
	}
}