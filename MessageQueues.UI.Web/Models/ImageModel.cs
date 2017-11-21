using System;

namespace MessageQueues.UI.Web.Models
{
	[Serializable]
	public class ImageModel
	{
		public string filePath { get; set; }
		public string fileName { get; set; }
		public string StartTime { get; }
		public string FinishTime { get; }
		public ImageModel()
		{

		}
		public ImageModel(StatusOfImage status)
		{
			StartTime = status.StartTime.ToString("F");
			FinishTime = status.FinishTime.ToString("F");
		}
	}
}