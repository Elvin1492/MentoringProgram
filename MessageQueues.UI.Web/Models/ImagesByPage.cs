using System;
using System.Collections.Generic;

namespace MessageQueues.UI.Web.Models
{
	public class ImagesByPage
	{
		public int TotalPages { get; set; }
		public List<ResizedImageWithPreview> ListOfImages { get; set; }
		public ImagesByPage(int totalPages, List<ResizedImage> images)
		{
			TotalPages = Convert.ToInt32(Math.Ceiling((decimal)totalPages / (decimal)CollectionOfWorkers.SizeOfPage));
			ListOfImages = new List<ResizedImageWithPreview>();
			images.ForEach(i => ListOfImages.Add(new ResizedImageWithPreview(i)));
		}
	}
}