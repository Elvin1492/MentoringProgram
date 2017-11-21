namespace MessageQueues.UI.Web.Models
{
	public class ResizedImageWithPreview
	{
		public int Id { get; set; }
		public string FilePath { get; set; }
		public string FileName { get; set; }
		public string PreviewPath { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public string StartTime { get; set; }
		public string FinishTime { get; set; }
		public ResizedImageWithPreview(ResizedImage r)
		{
			Id = r.Id;
			FilePath = r.FilePath;
			FileName = r.FileName;
			PreviewPath = r.PreviewPath;
			Width = r.Width;
			Height = r.Height;
			StartTime = r.StartTime.GetValueOrDefault().ToString("D");
			FinishTime = r.FinishTime.GetValueOrDefault().ToString("D");
		}
	}
}