namespace ExpressionTree2.Core
{
	public class From
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string FullName { get; set; }

		public override string ToString()
		{
			return $"Id:{Id} Name:{Name} FullName:{FullName}";
		}
	}
}
