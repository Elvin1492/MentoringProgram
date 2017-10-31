using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree2.Core
{
	public class To
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }

		public override string ToString()
		{
			return $"Id:{Id} Name:{Name} LastName:{LastName}";
		}
	}
}
