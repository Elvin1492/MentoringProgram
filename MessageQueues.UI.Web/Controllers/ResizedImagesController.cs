using System.Linq;
using System.Web.Http;
using MessageQueues.UI.Web.Models;

namespace MessageQueues.UI.Web.Controllers
{
	public class ResizedImagesController : ApiController
	{
		private readonly ImageContext _db = new ImageContext();

		// GET: api/ResizedImage/1/namedesc
		public ImagesByPage GetResizedImage(int page, string sort)
		{
			return new ImagesByPage(_db.ResizedImages.Count(), GetImagesByPage(page, sort).ToList());
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_db.Dispose();
			}
			base.Dispose(disposing);
		}

		private IQueryable<ResizedImage> GetImagesByPage(int page, string sort)
		{
			var result = _db.ResizedImages.OrderBy(i => i.Id);
			switch (sort)
			{
				case "idasc":
					result = _db.ResizedImages.OrderBy(i => i.Id);
					break;
				case "iddesc":
					result = _db.ResizedImages.OrderByDescending(i => i.Id);
					break;
				case "nameasc":
					result = _db.ResizedImages.OrderBy(i => i.FileName);
					break;
				case "namedesc":
					result = _db.ResizedImages.OrderByDescending(i => i.FileName);
					break;
				case "parentasc":
					result = _db.ResizedImages.OrderBy(i => i.ParentId);
					break;
				case "parentdesc":
					result = _db.ResizedImages.OrderByDescending(i => i.ParentId);
					break;
				case "startasc":
					result = _db.ResizedImages.OrderBy(i => i.StartTime);
					break;
				case "startdesc":
					result = _db.ResizedImages.OrderByDescending(i => i.StartTime);
					break;
				case "finishasc":
					result = _db.ResizedImages.OrderBy(i => i.FinishTime);
					break;
				case "finishdesc":
					result = _db.ResizedImages.OrderByDescending(i => i.FinishTime);
					break;
				case "widthtasc":
					result = _db.ResizedImages.OrderBy(i => i.Width);
					break;
				case "widthdesc":
					result = _db.ResizedImages.OrderByDescending(i => i.Width);
					break;
				case "heightasc":
					result = _db.ResizedImages.OrderBy(i => i.Height);
					break;
				case "heightdesc":
					result = _db.ResizedImages.OrderByDescending(i => i.Height);
					break;
				default:
					break;
			}

			return result.Skip(page < 2 ? 0 : CollectionOfWorkers.SizeOfPage * (page - 1)).
				Take(CollectionOfWorkers.SizeOfPage);
		}

	}
}