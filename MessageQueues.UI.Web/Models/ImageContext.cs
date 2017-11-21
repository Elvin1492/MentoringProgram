using System.Data.Entity;

namespace MessageQueues.UI.Web.Models
{
	public class ImageContext : DbContext
	{
		public ImageContext()
			: base("name=DataBaseImages")
		{
			Database.SetInitializer<ImageContext>(new CreateDatabaseIfNotExists<ImageContext>());
		}
		public DbSet<Image> Images { get; set; }
		public DbSet<ResizedImage> ResizedImages { get; set; }
	}
}