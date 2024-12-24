namespace ForoWebApp.Models
{
	public class CreateMessageData
	{
		public int ThreadId { get; set; }
		public int UserId { get; set; }
		public string? MessageContent { get; set; }
	}
}
