using System.Collections.Generic;
using System.Linq;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using MessageQueues.UI.Web.Models;

namespace MessageQueues.UI.Web.Controllers
{
	public class ImagesController : ODataController
	{
		private ImageContext db = new ImageContext();


		// GET: odata/Images
		public PageResult<Image> GetImages(ODataQueryOptions<Image> options)
		{
			ODataQuerySettings settings = new ODataQuerySettings()
			{
				PageSize = CollectionOfWorkers.SizeOfPage
			};

			IQueryable results = options.ApplyTo(db.Images.AsQueryable(), settings);

			return new PageResult<Image>(
				results as IEnumerable<Image>,
				null,
				db.Images.Count());
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool ImageExists(int key)
		{
			return db.Images.Count(e => e.Id == key) > 0;
		}
	}
}