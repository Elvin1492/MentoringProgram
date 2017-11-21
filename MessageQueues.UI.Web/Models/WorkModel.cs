using System;
using System.Collections.Generic;

namespace MessageQueues.UI.Web.Models
{
	[Serializable]
	public class WorkModel
	{
		public ImageModel OriginalFile { get; set; }
		public ImageModel PreviewFile { get; set; }
		public List<ImageModel> Files { get; set; }
		public WorkModel()
		{
			Files = new List<ImageModel>();
		}
	}
}